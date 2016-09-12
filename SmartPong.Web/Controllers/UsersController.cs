using SmartPong.Core;
using SmartPong.Helpers;
using SmartPong.Models;
using SmartPong.Models.View;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SmartPong.Controllers
{
    public class UsersController : Controller
    {
        public PartialViewResult Doubles()
        {
            var ratingType = Global.DbContext.RatingTypes.FirstOrDefault(rt => rt.Description == "Doubles Trueskill");
            var userRatings = ratingType.UserRatings;
            var viewModels = UserRankingViewModel.Generate(userRatings);
            return PartialView("_doublesRankings", viewModels);
        }

        public PartialViewResult Singles()
        {
            var test = Global.DbContext.Users.ToList();
            var test2 = Global.DbContext.UserRatings.ToList();
            var ratingType = Global.DbContext.RatingTypes.FirstOrDefault(rt => rt.Description == "Singles Trueskill");
            var userRatings = ratingType.UserRatings;
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
                user.CreateDate = DateTime.Now;
                user.Enabled = true;

                Global.DbContext.Users.Add(user);
                Global.DbContext.SaveChanges();
                user = Global.DbContext.Users.FirstOrDefault(u => u.Username == user.Username);
                if (user == null)
                {
                    throw new Exception("User was not added successfully");
                }

                var ratings = RatingsCalculator.GenerateNewUserRatings(user.UserId);
                Global.DbContext.UserRatings.AddRange(ratings);
                Global.DbContext.SaveChanges();

                return RedirectToAction("Index", "Rankings");
            }
            return View(user);
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}