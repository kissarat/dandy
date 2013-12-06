using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Johnny.Models;
using Johnny.Logic;
using Johnny.Data;

namespace Johnny
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApp : System.Web.HttpApplication
    {
        public static readonly string SecrectCookie = "secret";

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            new EntityContext().Database.Initialize(false);
            //new EntityContext().Seed();
            ServerObject.Log(this, "Started");
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Manager.Current = Manager.Unregistered;
            DataContext.Current = new DataContext();
            //return;
            //var secretCookie = Request.Cookies[SecrectCookie];
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            DataContext.DisposeContext();
        }
    }
}