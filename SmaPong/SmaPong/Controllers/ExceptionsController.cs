using System.Web.Mvc;
using SmaPong.Business;
using StackExchange.Exceptional;

namespace SmaPong.Controllers
{
    public class ExceptionsController : Controller
    {
        public ActionResult Index()
        {
            if (!AdminBusiness.IsAdmin(User.Identity.Name))
            {
                return RedirectToAction("Rankings", "Players");
            }

            var context = System.Web.HttpContext.Current;
            var page = new HandlerFactory().GetHandler(context, Request.RequestType, Request.Url.ToString(), Request.PathInfo);
            page.ProcessRequest(context);

            return null;
        }
    }
}
