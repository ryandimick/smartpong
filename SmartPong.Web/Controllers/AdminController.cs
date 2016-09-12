using System.Web.Mvc;

namespace SmartPong.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Confirm()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Confirm()
        //{
        //    return RedirectToAction("Index", "Rankings");
        //}

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Reload()
        {
            // reload all
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }
    }
}