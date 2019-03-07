using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartPong.Controllers
{
    public sealed class MatchHistoryController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}