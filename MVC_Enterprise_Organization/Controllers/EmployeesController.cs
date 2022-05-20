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
    public class EmployeesController : Controller
    {
        private EnterpriseOrganizationEntities db = new EnterpriseOrganizationEntities();
        private static List<Employee> DeletedItems;

        public EmployeesController()
        {
            if (DeletedItems == null)
            {
                DeletedItems = new List<Employee>();
            }
        }
        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult DeletedEmployees()
        {
            if (DeletedItems != null)
            {
                return View(DeletedItems);
            }
            return HttpNotFound();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,BirthDate,Gender,PhoneNumber,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee emp = db.Employees.FirstOrDefault(x => x.Email == employee.Email);

                if (emp != null)
                {
                    return HttpNotFound("This Email already exists!");
                }

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,BirthDate,Gender,PhoneNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult SoftDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SoftDeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            DeletedItems.Add(employee);

            DeleteConfirmed(id);
            return RedirectToAction("Index");
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                employee = DeletedItems.FirstOrDefault(x => x.Id == id);
            }

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                if (employee.HRDatas.Count != 0)
                {
                    ViewBag.ErrorMessage = "Email not found or matched";
                }
                var item = db.HRDatas.FirstOrDefault(x => x.EmployeeId == id);
                if (item != null)
                {
                    return HttpNotFound("First you must delete related record in HRDatas!");
                }
                db.Employees.Remove(employee);
                db.SaveChanges();
            }
            else
            {
                employee = DeletedItems.FirstOrDefault(x => x.Id == id);
                DeletedItems.Remove(employee);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RetrieveRecord(int id)
        {
            Employee employee = DeletedItems.FirstOrDefault(x => x.Id == id);

            db.Employees.Add(employee);
            db.SaveChanges();

            DeletedItems.Remove(employee);

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
