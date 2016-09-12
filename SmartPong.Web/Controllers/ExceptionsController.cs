using StackExchange.Exceptional;
using System.Web.Mvc;

namespace SmartPong.Controllers
{
    public class ExceptionsController : Controller
    {
        public ActionResult Index()
        {
            // check for admin

            var context = System.Web.HttpContext.Current;
            var page = new HandlerFactory().GetHandler(context, Request.RequestType, Request.Url.ToString(), Request.PathInfo);
            page.ProcessRequest(context);

            return null;
        }
    }
}