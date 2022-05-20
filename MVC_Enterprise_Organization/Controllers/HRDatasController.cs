using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Enterprise_Organization.Models;

namespace MVC_Enterprise_Organization.Controllers
{
    public class HRDatasController : Controller
    {
        private EnterpriseOrganizationEntities db = new EnterpriseOrganizationEntities();

        // GET: HRDatas
        public ActionResult Index()
        {
            var hRDatas = db.HRDatas.Include(h => h.Employee);

            return View(hRDatas.ToList());
        }

        [HttpPost]
        public ActionResult SearchResult(string searchText)
        {
            IEnumerable<Employee> employee = from e in db.Employees select e;

            if (!String.IsNullOrEmpty(searchText))
            {
                employee = employee.Where(e => e.Email.ToLower().Contains(searchText.ToLower()));
            }

            return View(employee);
        }

        public ActionResult SendMessage(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(int id, string SendMessage)
        {
            Employee employee = db.Employees.Find(id);
            employee.Letter = SendMessage;
            db.SaveChanges();

            return View();
        }

        // GET: HRDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRData hRData = db.HRDatas.Find(id);
            if (hRData == null)
            {
                return HttpNotFound();
            }
            return View(hRData);
        }

        // GET: HRDatas/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: HRDatas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SSN,EmployeeId,Salary")] HRData hRData)
        {
            if (ModelState.IsValid)
            {
                db.HRDatas.Add(hRData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hRData.EmployeeId);
            return View(hRData);
        }

        // GET: HRDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRData hRData = db.HRDatas.Find(id);
            if (hRData == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hRData.EmployeeId);
            return View(hRData);
        }

        // POST: HRDatas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SSN,EmployeeId,Salary")] HRData hRData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hRData.EmployeeId);
            return View(hRData);
        }

        // GET: HRDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRData hRData = db.HRDatas.Find(id);
            if (hRData == null)
            {
                return HttpNotFound();
            }
            return View(hRData);
        }

        // POST: HRDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HRData hRData = db.HRDatas.Find(id);
            db.HRDatas.Remove(hRData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
