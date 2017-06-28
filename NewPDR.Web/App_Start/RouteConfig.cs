using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewPDR.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            "pdrDemo1",
            "PDR/All/{id}",
            new
            {
                controller = "PDReview",
                action = "ShowAllPDR",
                id = UrlParameter.Optional
            });

              routes.MapRoute(
            "pdrDemo2",
            "PDR/Details/{revieweeID}/{reviewPeriod}",
            new
            {
                controller = "PDReview",
                action="ShowPDRDetails",
                revieweeID = UrlParameter.Optional,
                reviewPeriod = UrlParameter.Optional ,
                refresh = UrlParameter.Optional 
            });

           


            routes.MapRoute(
            "pdrDemo3",
            "PDR/{action}/{id}",
            new
            {
                controller = "PDReview",
                id = UrlParameter.Optional
            });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PDReview", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
