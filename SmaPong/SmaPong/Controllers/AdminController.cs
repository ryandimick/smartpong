using System.Web.Mvc;
using SmaPong.Business;

namespace SmaPong.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (!AdminBusiness.IsAdmin(User.Identity.Name))
            {
                return RedirectToAction("Rankings", "Players");
            }

            return View();
        }

        public ActionResult Reload()
        {
            if (!AdminBusiness.IsAdmin(User.Identity.Name))
            {
                return RedirectToAction("Rankings", "Players");
            }

            Global.LoadAll();
            return RedirectToAction("Index");
        }
    }
}
