﻿using System;
using System.Web;
using System.Web.Mvc;
using SmaPong.DataAccess;

namespace SmaPong.Security
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = false;
            var username = httpContext.User.Identity.Name;
            // Some code to find the user in the database...
            var user = PlayerData.Retrieve(username);
            if (user != null)
            {
                isAuthorized = true;
            }


            return isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                SetCachePolicy(filterContext);
            }
            else
            {
                // If not authorized, redirect to the Login action 
                // of the Account controller... 
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                        {"controller", "Players"}, {"action", "Create"}
                    }
                    );
            }
        }

        protected void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, 
            // the authorization code runs after the output caching module. 
            // In the worst case this could allow an authorized user 
            // to cause the page to be cached, then an unauthorized user would later 
            // be served the cached page. We work around this by telling proxies not to 
            // cache the sensitive page, then we hook our custom authorization code into 
            // the caching mechanism so that we have the final say on whether a page 
            // should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidationHandler, null /* data */);
        }

        public void CacheValidationHandler(HttpContext context,
            object data,
            ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }
}