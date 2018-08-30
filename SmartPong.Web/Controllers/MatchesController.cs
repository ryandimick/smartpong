using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SmartPong.Models;
using SmartPong.Models.View;
using WebGrease.Css.Extensions;

namespace SmartPong.Controllers
{
    public sealed class MatchesController : BaseController
    {
        private static readonly IEnumerable<SingleRakingsChartViewModels> _chartViewModes = GetRankingViews();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read_Matches([DataSourceRequest] DataSourceRequest request)
        {
            var matches = Global.Repository.RetrieveMatches(m => m.Status >= MatchStatus.Submitted).ToGridViewModel()
                .OrderByDescending(m => m.MatchDate);

            return Json(matches.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Matches_Modal()
        {
            var opponentList = Global.Repository.RetrieveUsers(user => user.Enabled && !String.Equals(user.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));

            var matchViewModel = new MatchCreateViewModel();
            matchViewModel.DoubleOpponents =
                 opponentList.Select(x => new SelectListItem { Value = x.UserId.ToString(), Text = x.DisplayName });
            matchViewModel.SingleOpponent = opponentList.Select(x =>
                new SelectListItem { Value = x.UserId.ToString(), Text = x.DisplayName });

            return PartialView("_matchesModal", matchViewModel);
        }

        public ActionResult Matches_Save(MatchCreateViewModel match)
        {
            try
            {
                var users = Global.Repository.RetrieveUsers(w => w.Enabled);

                if (match.MatchType == MatchType.Type.Singles)
                {
                    var submitter = GetUserInfo();
                    var opponent = users.First(f => f.UserId == Convert.ToInt32(match.SelectedOpponents));
                    var matchToSubmit = new Match(match.MatchType, match.MatchTime);
                    matchToSubmit.AddTeam(1, new List<User> { submitter });
                    matchToSubmit.AddTeam(2, new List<User> { opponent });
                    SubmitMatch(match, matchToSubmit);
                }
                else
                {
                    var submitter = GetUserInfo();
                    var teammate = users.First(f => f.UserId == Convert.ToInt32(match.Teammate));
                    var opponents = match.SelectedOpponents.Split(',')
                        .Select(user => users.First(f => f.UserId == Convert.ToInt32(user))).ToList();
                    var matchToSubmit = new Match(match.MatchType, match.MatchTime);
                    matchToSubmit.AddTeam(1, new List<User> { submitter, teammate });
                    matchToSubmit.AddTeam(2, new List<User> { opponents.First(), opponents.Last() });
                    SubmitMatch(match, matchToSubmit);
                }
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Invalid Operation!" });
            }
        }

        private static void SubmitMatch(MatchCreateViewModel match, Match matchToSubmit)
        {
            matchToSubmit.SetOutcome(match.YourScore > match.OpponentScore ? 1 : 2);
            Global.Repository.CreateMatch(matchToSubmit);
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

        public ActionResult Pending()
        {
            return View("_pendingMatches");
        }

        public ActionResult Read_Pending_Matches([DataSourceRequest] DataSourceRequest request)
        {
            var user = Global.Repository.RetrieveUser(User.Identity.Name);

            var matches = !user.Admin
                ? Global.Repository.RetrieveMatches(m =>
                        m.Status < MatchStatus.Submitted &&
                        m.MatchParticipants.Any(a => a.MatchTeamId != 1 && a.UserId == user.UserId)).ToGridViewModel()
                    .OrderByDescending(m => m.MatchDate)
                : Global.Repository.RetrieveMatches(m => m.Status < MatchStatus.Posted).ToGridViewModel();
            return Json(matches.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Submit_Pending_Matches(IEnumerable<int> updateRows, string action)
        {
            try
            {
                updateRows.ForEach(x => Global.Repository.ConfirmMatch(x, GetUserInfo().UserId, action));

                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Invalid Operation!" });
            }
        }

        //public ActionResult Match_User_Selected_Graph(UserRankingViewModel viewModel)
        //{
        //todo work on selectio
        //}

        private User GetUserInfo()
        {
            IEnumerable<User> users = Global.Repository.RetrieveUsers(w => w.Enabled);
            var loggedInUser = users.First(f =>
                String.Equals(f.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
            return loggedInUser;
        }

        public ActionResult Match_Group_Doubles_Ratings_Graph()//todo group dates together create cluster of data points.. 
        {
            var matchUserRatings = _chartViewModes.Where(w => w.RatingTypeId == (int) UserRatingType.TrueskillDoubles);

            return Json(matchUserRatings);
        }

        public ActionResult Match_Group_Single_Ratings_Graph()
        {
            var matchUserRatings = _chartViewModes.Where(w => w.RatingTypeId == (int) UserRatingType.TrueskillSingles);
            
            return Json(matchUserRatings);
        }

        private static IEnumerable<SingleRakingsChartViewModels> GetRankingViews()
        {
            var matches = Global.Repository.RetrieveMatches();
            var matchUserRatings = Global.Repository
                .RetrieveMatchUserRatings()
                .ToChartFormat(matches).OrderByDescending(o => o.MatchId);
            return matchUserRatings;
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