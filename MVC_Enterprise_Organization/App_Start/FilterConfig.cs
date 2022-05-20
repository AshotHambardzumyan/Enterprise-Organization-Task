using System.Web;
using System.Web.Mvc;

namespace MVC_Enterprise_Organization
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
