using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using SmaPong.Business;
using SmaPong.Models;
using SmaPong.Security;

namespace SmaPong.Controllers
{
    public class MatchesController : Controller
    {
        [MyAuthorize]
        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player =
                Global.Players.SingleOrDefault(
                    p => string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
            if (player == null)
            {
                return HttpNotFound();
            }
            var match = player.Matches.SingleOrDefault(m => m.Id == id);
            if (match == null)
            {
                if (!AdminBusiness.IsAdmin(User.Identity.Name))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var matchDetail = Global.Matches.SingleOrDefault(m => m.Id == id);
                if (matchDetail == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                player = Global.Players.SingleOrDefault(p => p.Id == matchDetail.PlayerTwoId);

                if (player == null)
                {
                    return HttpNotFound();
                }

                match = player.Matches.SingleOrDefault(m => m.Id == id);
                if (match == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "Id")] int id)
        {
            if (ModelState.IsValid)
            {
                MatchBusiness.Confirm(id);
                return RedirectToAction("Pending");
            }

            return View();
        }

        [MyAuthorize]
        public ActionResult Create()
        {
            var opponents =
                Global.Players.Where(
                    p => !string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(p => p.Name);
            var match = new NewMatch
            {
                Opponents = new SelectList(opponents, "Id", "Name"),
                PossibleOutcomes = new SelectList(MatchOutcome.PossibleOutcomes, "Id", "Description")
            };
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatchDate,PlayerTwoId,Placement")] NewMatch newMatch)
        {
            var player =
                Global.Players.SingleOrDefault(
                    p => string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
            if (player == null)
                return HttpNotFound();

            newMatch.PlayerOneId = player.Id;
            MatchBusiness.CreateMatch(newMatch);
            return RedirectToAction("Index");
        }

        [MyAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player =
                Global.Players.SingleOrDefault(
                    p => string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
            if (player == null)
            {
                return HttpNotFound();
            }
            var match = player.Matches.SingleOrDefault(m => m.Id == id);
            if (match == null)
            {
                if (!AdminBusiness.IsAdmin(User.Identity.Name))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var matchDetail = Global.Matches.SingleOrDefault(m => m.Id == id);
                if (matchDetail == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                player = Global.Players.SingleOrDefault(p => p.Id == matchDetail.PlayerTwoId);

                if (player == null)
                {
                    return HttpNotFound();
                }

                match = player.Matches.SingleOrDefault(m => m.Id == id);
                if (match == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id")] int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MatchBusiness.Delete(id);
                    return RedirectToAction("Pending");
                }
            }
            catch (Exception e)
            {
                var hei = new HandleErrorInfo(e, "Matches", "Delete");
                return RedirectToAction("Error", hei);
            }
            return View();
        }

        public ActionResult Error(Exception e)
        {
            ViewBag.Message = e.ToString();
            return View();
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var player =
                Global.Players.SingleOrDefault(
                    p => string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
            ViewBag.PlayerId = player == null ? 0 : player.Id;

            return View(Global.Matches.OrderByDescending(m => m.MatchDate).ToPagedList(pageNumber, pageSize));
        }

        [MyAuthorize]
        public ActionResult Pending()
        {
            var player =
                Global.Players.SingleOrDefault(
                    p => string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
            if (player == null)
                return HttpNotFound();

            return
                View(
                    player.Matches.Where(m => m.Position == 2 && m.Status == MatchStatus.PendingConfirmation)
                        .OrderBy(m => m.MatchDate));
        }

        public ActionResult Player(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var player = Global.Players.SingleOrDefault(p => p.Id == id);
            if (player == null)
            {
                return HttpNotFound();
            }

            ViewBag.Title = string.Format("{0}'s Matches", player.Name);
            ViewBag.PlayerId = id;
            return View(player.Matches.OrderByDescending(m => m.MatchDate));
        }
    }
}
