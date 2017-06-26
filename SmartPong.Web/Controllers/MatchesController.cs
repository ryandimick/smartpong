using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SmartPong.Models;

namespace SmartPong.Controllers
{
    public sealed class MatchesController : BaseController
    {
        public ActionResult Index()
        {
            var matches = Global.Repository.RetrieveMatches(m => m.Status >= 2).OrderByDescending(m => m.MatchDate);
            return View(matches);
        }

        public PartialViewResult Confirm()
        {
            return PendingMatches();
        }

        public ActionResult Create()
        {
            var opponents =
                Global.Repository.RetrieveUsers(
                    u =>
                        u.Enabled &&
                        !string.Equals(u.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(u => u.DisplayName);
            return View(opponents);
        }

        [HttpPost]
        public ActionResult Create(DateTime matchDate, int opponentId, int yourscore, int theirscore)
        {
            bool result;
            try
            {
                var submitter = Global.Repository.RetrieveUser(User.Identity.Name);
                var opponent = Global.Repository.RetrieveUser(opponentId);
                var match = new Match(MatchType.Type.Singles, matchDate);
                match.AddTeam(1, new List<User> { submitter });
                match.AddTeam(2, new List<User> { opponent });
                match.SetOutcome(yourscore > theirscore ? 1 : 2);
                Global.Repository.CreateMatch(match);
                result = true;
            }
            catch(Exception e)
            {
                result = false;
            }
            return Json(new { success = result });
        }

        public PartialViewResult Delete()
        {
            return PendingMatches();
        }

        public ActionResult Pending()
        {
            return View();
        }

        private PartialViewResult PendingMatches()
        {
            IEnumerable<Match> pendingMatches = new List<Match>();
            var userId = UserId;

            if (userId != null)
            {
                pendingMatches = Repository.RetrieveMatches(m => m.Status == (int)MatchStatus.Type.Submitted);
                if (!IsAdmin())
                {
                    pendingMatches =
                        pendingMatches.Where(
                            m => m.MatchParticipants.Any(mp => mp.UserId == userId && mp.MatchTeamId > 1));
                }
            }
            return PartialView("_pendingMatches", pendingMatches);
        }
    }
}