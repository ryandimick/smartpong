using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Exceptional;

namespace SmaPong
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public void Application_Error(object sender, EventArgs e)
        {
            LogException(Server.GetLastError());
        }

        protected void Application_Start()
        {
            // Setting the jQuery URL, in case you need this to be an internally hosted jQuery for example
            // By default, this will pull from the google CDN
            ErrorStore.jQueryURL = "//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js";

            ErrorStore.OnBeforeLog += (sender, args) => args.Error.Message += " - This was appended in the OnBeforeLog handler.";
            ErrorStore.OnAfterLog += (sender, args) => Trace.WriteLine("The logged exception GUID was: " + args.ErrorGuid);

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Example method to log an exception to the log...that' not shown to the user.
        /// </summary>
        /// <param name="e">The exception to log</param>
        public static void LogException(Exception e)
        {
            // Note: When dealing with non-web applications, or logging from background threads, 
            // you would pass, null in instead of a HttpContext object.
            ErrorStore.LogException(e, HttpContext.Current);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute(), 2); //by default added


            filters.Add(new HandleErrorAttribute
            {
                View = "Error"
            }, 1);
        }
    }
}