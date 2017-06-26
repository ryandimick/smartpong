using SmartPong.Helpers;
using SmartPong.Models;
using SmartPong.Models.View;
using System.Web.Mvc;

namespace SmartPong.Controllers
{
    public class UsersController : Controller
    {
        /* TODO: 
         * Removed this until the implementation is further along */
        //public PartialViewResult Doubles()
        //{
        //    var ratingType = Global.DbContext.RatingTypes.FirstOrDefault(rt => rt.Description == "Doubles Trueskill");
        //    var userRatings = ratingType.UserRatings;
        //    var viewModels = UserRankingViewModel.Generate(userRatings);
        //    return PartialView("_doublesRankings", viewModels);
        //}

        public PartialViewResult Singles()
        { 
            var userRatings = Global.Repository.RetrieveUserRatings(UserRatingType.TrueskillSingles);
            var viewModels = UserRankingViewModel.Generate(userRatings);
            return PartialView("_singlesRankings", viewModels);
        }

        public ActionResult Register()
        {
            string name = User.Identity.Name;
            User activeDirectoryUser = ActiveDirectoryServices.FetchUser(name);
            return View(activeDirectoryUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username, GivenName, Surname, Nickname, Email, Notifications")] User user)
        {
            if (ModelState.IsValid)
            {
                Global.Repository.CreateUser(user.Username, user.GivenName, user.Surname, user.Email, user.Nickname);

                return RedirectToAction("Index", "Rankings");
            }
            return View(user);
        }

        public ActionResult Profiles(int? userId)
        {
            return View();
        }
    }
}