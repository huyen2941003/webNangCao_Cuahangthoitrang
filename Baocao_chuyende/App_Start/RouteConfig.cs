using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Baocao_chuyende
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] {"Baocao_chuyende.Controllers"}
            );

            //routes.MapRoute(
            //name: "Admin_default",
            //url: "Admin/{controller}/{action}/{id}",
            //defaults: new { area = "Admin", controller = "Home", action = "Index", id = UrlParameter.Optional },
            //namespaces: new string[] { "Baocao_chuyende.Areas.Admin.Controllers" }
            //);
        }
        //public override string AreaName
        //{
        //    get
        //    {
        //        return "Admin";
        //    }
        //}

        //public override void RegisterArea(AreaRegistrationContext context)
        //{
        //    context.MapRoute(
        //        "Admin_default",
        //        "Admin/{controller}/{action}/{id}",
        //        new { action = "Index", Controller = "Home", id = UrlParameter.Optional }, 
        //        new[] { "Baocao_chuyende.Areas.Admin.Controllers" }
        //    );
        //}
    }
}
