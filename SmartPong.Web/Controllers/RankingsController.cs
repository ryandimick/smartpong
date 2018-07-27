using System.Web.Mvc;

namespace SmartPong.Controllers
{
    public class RankingsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Doubles()
        {
            return PartialView("_doublesGrid");
        }
    }
}