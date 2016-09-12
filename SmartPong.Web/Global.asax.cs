using StackExchange.Exceptional;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SmartPong
{
    public class MvcApplication : HttpApplication
    {
        public void Application_Error(object sender, EventArgs e)
        {
            LogException(Server.GetLastError());
        }

        protected void Application_Start()
        {
            ErrorStore.jQueryURL = "//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js";

            ErrorStore.OnBeforeLog += (sender, args) => args.Error.Message += " - This was appended in the OnBeforeLog handler.";
            ErrorStore.OnAfterLog += (sender, args) => Trace.WriteLine("The logged exception GUID was: " + args.ErrorGuid);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
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
    }
}
