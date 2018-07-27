using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SmartPong.Models;
using SmartPong.Models.View;

namespace SmartPong.Controllers
{
    public sealed class MatchesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read_Matches([DataSourceRequest] DataSourceRequest request)
        {
            var matches = Global.Repository.RetrieveMatches(m => m.Status >=  MatchStatus.Submitted).ToGridViewModel()
                .OrderByDescending(m => m.MatchDate);
            return Json(matches.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Matches_Modal()
        {
            var opponentList = Global.Repository.RetrieveUsers(user => user.Enabled && !String.Equals(user.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));

            var matchViewModel = new MatchCreateViewModel();
            matchViewModel.DoubleOpponents =
                 opponentList.Select(x => new SelectListItem {Value = x.UserId.ToString(), Text = x.DisplayName});
            matchViewModel.SingleOpponent = opponentList.Select(x =>
                new SelectListItem {Value = x.UserId.ToString(), Text = x.DisplayName});
            
            return PartialView("_matchesModal", matchViewModel);
        }
        
        public ActionResult Matches_Save(MatchCreateViewModel match)
        {
            try
            {
                var users = Global.Repository.RetrieveUsers(w => w.Enabled);

                if (match.MatchType == MatchType.Type.Singles)
                {
                    var submitter = users.First(f => String.Equals(f.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
                    var opponent = users.First(f => f.UserId == Convert.ToInt32(match.SelectedOpponents));
                    var matchToSubmit = new Match(match.MatchType, match.MatchTime);
                    matchToSubmit.AddTeam(1, new List<User> { submitter });
                    matchToSubmit.AddTeam(2, new List<User> { opponent });
                    SubmitMatch(match, matchToSubmit);
                }
                else
                {
                    var submitter = users.First(f => String.Equals(f.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
                    var teammate = users.First(f => f.UserId == Convert.ToInt32(match.Teammate));
                    var opponents = match.SelectedOpponents.Split(',')
                        .Select(user => users.First(f => f.UserId == Convert.ToInt32(user))).ToList();
                    var matchToSubmit = new Match(match.MatchType, match.MatchTime);
                    matchToSubmit.AddTeam(1, new List<User> { submitter, teammate });
                    matchToSubmit.AddTeam(2, new List<User> { opponents.First(), opponents.Last() });
                    SubmitMatch(match, matchToSubmit);
                }
                return Json(new{success = true});
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = "Invalid Operation!"});
            }
        }

        private static void SubmitMatch(MatchCreateViewModel match, Match matchToSubmit)
        {
            matchToSubmit.SetOutcome(match.YourScore > match.OpponentScore ? 1 : 2);
            Global.Repository.CreateMatch(matchToSubmit);
        }

        //public PartialViewResult Confirm()
        //{
        //    return PendingMatches();
        //}

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

        public ActionResult Pending()
        {
            return View();
        }

        public ActionResult Read_Pending_Matches([DataSourceRequest] DataSourceRequest request)
        {
            var user = Global.Repository.RetrieveUser(User.Identity.Name);

            var matches = user != null
                ? Global.Repository.RetrieveMatches(m => m.Status == MatchStatus.Submitted && m.MatchParticipants.Any(a => a.MatchTeamId != 1 && a.UserId == user.UserId)).ToGridViewModel()
                    .OrderByDescending(m => m.MatchDate)
                : new List<MatchGridViewModel>().OrderByDescending( m => m.MatchDate);
            return Json(matches.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Submit_Pending_Matches(int matchId)
        {
            try
            {
                return Json(new {success = true});
            }
            catch (Exception)
            {
                return Json(new {success = false, message = "Invalid Operation!"});
            }
        }

        //private PartialViewResult PendingMatches()
        //{
        //    IEnumerable<Match> pendingMatches = new List<Match>();
        //    var userId = UserId;

        //    if (userId != null)
        //    {
        //        pendingMatches = Repository.RetrieveMatches(m => m.Status == (int)MatchStatus.Type.Submitted);
        //        if (!IsAdmin())
        //        {
        //            pendingMatches =
        //                pendingMatches.Where(
        //                    m => m.MatchParticipants.Any(mp => mp.UserId == userId && mp.MatchTeamId > 1));
        //        }
        //    }
        //    return PartialView("_pendingMatches", pendingMatches);
        //}
    }
}