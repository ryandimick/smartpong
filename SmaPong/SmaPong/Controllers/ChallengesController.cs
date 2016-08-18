using System;
using System.Linq;
using System.Web.Mvc;
using SmaPong.Business;
using SmaPong.Models;
using SmaPong.Security;

namespace SmaPong.Controllers
{
    public class ChallengesController : Controller
    {
       // [MyAuthorize]
       // public ActionResult Create()
       // {
       //     var player =
       //Global.Players.SingleOrDefault(
       //    p => string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));

       //     if (player == null)
       //     {
       //         return HttpNotFound();
       //     }

       //     var opponents =
       //         Global.Players.Where(
       //             p =>
       //                 !string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase) &
       //                 p.Notifications & !string.IsNullOrWhiteSpace(p.Email)).OrderBy(p => p.Name);

       //     var challenge = new Challenge
       //     {
       //         Opponents = new SelectList(opponents, "Id", "Name"),
       //         ChallengerName = player.Name,
       //         Timestamp = DateTime.Now
       //     };

       //     return View(challenge);
       // }

       // [HttpPost]
       // [ValidateAntiForgeryToken]
       // public ActionResult Create(
       //     [Bind(Include = "ChallengedId, ChallengerName, Timestamp, Message")] Challenge challenge)
       // {

       // }


        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Challenges/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Challenges/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Challenges/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Challenges/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Challenges/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Challenges/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Challenges/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
