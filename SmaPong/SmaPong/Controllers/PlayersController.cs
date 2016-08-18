using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Mvc;
using Moserware.Skills;
using PagedList;
using SmaPong.Business;
using SmaPong.DataAccess;
using SmaPong.Models;
using SmaPong.Security;
using Player = SmaPong.Models.Player;

namespace SmaPong.Controllers
{
    public class PlayersController : Controller
    {
        public ActionResult Create()
        {
            var name = User.Identity.Name;
            UserPrincipal user;

            using (var pc = new PrincipalContext(ContextType.Domain, "smausa"))
            {
                user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, name);
            }

            if (user == null)
            {
                throw new Exception("Active Directory user not found!");
            }

            var player = new Player
            {
                Username = name,
                FirstName = user.GivenName,
                Surname = user.Surname,
                Email = user.EmailAddress,
                Notifications = !string.IsNullOrWhiteSpace(user.EmailAddress)
            };

            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nickname")] Player player)
        {
            if (ModelState.IsValid)
            {
                var name = User.Identity.Name;

                UserPrincipal user;

                using (var pc = new PrincipalContext(ContextType.Domain, "smausa"))
                {
                    user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, name);
                }

                if (user == null)
                {
                    throw new Exception("User not found!");
                }
                player.Username = name;
                player.FirstName = user.GivenName;
                player.Surname = user.Surname;
                player.Email = user.EmailAddress;
                player.CreateDate = DateTime.Now;
                player.ActivityDate = new DateTime(1900, 1, 1);
                player.Mu = GameInfo.DefaultGameInfo.DefaultRating.Mean;
                player.Sigma = GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation;
                player.Active = true;
                PlayerData.Create(player);
                Global.LoadPlayersOnly();
                return RedirectToAction("Rankings");

            }
            return View(player);
        }

        [MyAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var player = Global.Players.SingleOrDefault(p => p.Id == id);
            if (player == null || !string.Equals(player.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                return HttpNotFound();
            }

            return View(player);
        }

        [HttpPost]
        [MyAuthorize]
        public ActionResult Edit([Bind(Include = "Id, FirstName, Surname, Nickname, Email, Notifications")] Player player)
        {
            PlayerData.UpdateDetails(player);
            Global.LoadPlayersOnly();

            var p = Global.Players.SingleOrDefault(pl => pl.Id == player.Id);

            if (p == null)
            {
                return HttpNotFound();
            }

            return View("Profile", p);
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            return View(Global.Players.OrderBy(p => p.Name).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Profile(int? id)
        {
            PlayerDetail player;
            if (id == null)
            {
                player =
                   Global.Players.SingleOrDefault(
                       p => string.Equals(p.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
                if (player == null)
                {
                    return RedirectToAction("Create", "Players");
                }
                return View("Profile", player);   
            }

            player = Global.Players.SingleOrDefault(p => p.Id == id);
            if (player == null)
            {
                return HttpNotFound();
            }

            ViewBag.Title = string.Format("{0}'s Profile", player.Name);
            ViewBag.PlayerId = id;

            return View(player);
        }

        public ActionResult Rankings(int? page)
        {
            ViewBag.Message = "Official Ratings Utilizing \"TrueSkill\"";

            int pageSize = 20;
            int pageNumber = page ?? 1;

            return
                View(
                    Global.Players.Where(
                        p =>
                            p.Active & p.Mu != GameInfo.DefaultGameInfo.DefaultRating.Mean &
                            p.Sigma != GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation)
                        .OrderByDescending(p => p.Mu).ToPagedList(pageNumber, pageSize));
        }
    }
}
