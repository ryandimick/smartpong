﻿using System;
using System.Linq;
using SmartPong.Helpers;
using SmartPong.Models;
using SmartPong.Models.View;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

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

        public ActionResult Singles([DataSourceRequest] DataSourceRequest request)
        {
            var userRatings = Global.Repository.RetrieveUserRatings(UserRatingType.TrueskillSingles);
            var viewModels = UserRankingViewModel.Generate(userRatings).OrderByDescending(o => o.Rating);
            return Json(viewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Doubles([DataSourceRequest] DataSourceRequest request)
        {
            var userRatings = Global.Repository.RetrieveUserRatings(UserRatingType.TrueskillDoubles);
            var viewModels = UserRankingViewModel.Generate(userRatings).OrderByDescending(o => o.Rating);
            return Json(viewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            //string name = User.Identity.Name;
            //User activeDirectoryUser = name != null ? ActiveDirectoryServices.FetchUser(name): new User();
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username, GivenName, Surname, Nickname, Email, Notifications")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var username = string.Format(@"GLOBALSMA\{0}", user.Username);
                    Global.Repository.CreateUser(username, user.GivenName, user.Surname, user.Email, user.Nickname);

                    return RedirectToAction("Index", "Rankings");
                }
                return View(user);
            }
            catch (Exception e)
            {
                return Json(new {message = e.Message});
            }

        }

        public ActionResult Profiles(int? userId)
        {
            return View();
        }
    }
}