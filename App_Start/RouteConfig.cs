using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Travel_Agency_2nd_Semester_Project
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
               name: "Default3",
               url: "Bookings/{action}/{id}",
               defaults: new { controller = "Bookings", action = "Create", id = UrlParameter.Optional }
       );

            routes.MapRoute(
               name: "Default2",
               url: "Gallery/{action}/{id}",
               defaults: new { controller = "Gallery", action = "Gallery", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default1",
                url: "Tours/{action}/{id}",
                defaults: new { controller = "Tours", action = "WicklowMountains", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomePage", action = "HomePage", id = UrlParameter.Optional }
            );
        }
    }
}
