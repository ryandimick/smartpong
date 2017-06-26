using System;
using System.Linq;
using System.Web.Mvc;
using SmartPong.Models;

namespace SmartPong.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ISmartPongRepository Repository = Global.Repository;

        private User GetUser()
        {
            User user = Repository.RetrieveUsers(u => String.Equals(u.Username, User.Identity.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return user;
        }

        protected bool IsAdmin()
        {
            var user = GetUser();
            return user != null && user.Admin;
        }

        protected bool IsUser(int userId)
        {
            var user = GetUser();
            return user != null && user.UserId == userId;
        }

        protected int? UserId
        {
            get
            {
                var user = GetUser();
                return user?.UserId;
            }
        }
    }
}