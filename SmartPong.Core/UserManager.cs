using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moserware.Skills;
using SmartPong.Models;
using System.Web.Script.Serialization;
using SmartPong.Exceptions;

namespace SmartPong
{
    internal class UserManager
    {
        private readonly SmartPongContext _context;

        internal UserManager(SmartPongContext context)
        {
            _context = context;
        }

        /* TODO: this needs to be redone to do a true deep clone */
        private User Clone(User user)
        {
            var clonedUser = new User
            {
                UserId = user.UserId,
                Username = user.Username,
                GivenName = user.GivenName,
                Surname = user.Surname,
                Nickname = user.Nickname,
                Email = user.Email,
                Notifications = user.Notifications,
                CreateDate = user.CreateDate,
                ActivityDate = user.ActivityDate,
                Admin = user.Admin,
                Enabled = user.Enabled
            };
            return clonedUser;
        }

        internal User CreateUser(string username, string givenName, string surname, string email, string nickname)
        {
            if ((RetrieveUsers(u => string.Equals(u.Username, username, StringComparison.CurrentCultureIgnoreCase)).Any()))//u.Username.Equals(username , u.Username)) != null)
                throw new Exception("User Already Created!");

            var newUser = new User
            {
                Username = username,
                GivenName = givenName,
                Surname = surname,
                Email = email,
                Nickname = nickname,
                Enabled = true,
                CreateDate = DateTime.Now
            };

            newUser.UserRatings = DefaultUserRatings.Generate();

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        internal User DisableUser(int userId)
        {
            var user = FindUser(userId);
            return DisableUser(user);
        }

        internal User DisableUser(string username)
        {
            var user = FindUser(username);
            return DisableUser(user);
        }

        private User DisableUser(User user)
        {
            var updatedUser = Clone(user);
            updatedUser.Enabled = false;
            return Update(user, updatedUser);
        }

        internal User EnableUser(int userId)
        {
            var user = FindUser(userId);
            return EnableUser(user);
        }

        internal User EnableUser(string username)
        {
            var user = FindUser(username);
            return EnableUser(user);
        }

        private User EnableUser(User user)
        {
            var updatedUser = Clone(user);
            updatedUser.Enabled = true;
            return Update(user, updatedUser);
        }

        private User FindUser(int userId)
        {
            var user = _context.Users.First(u => u.UserId == userId);
            return user;
        }

        private User FindUser(string username)
        {
            var user = _context.Users.First(u => u.Username == username);
            return user;
        }

        internal IEnumerable<UserRating> RetrieveRatings(UserRatingType type)
        {
            return _context.UserRatings.Where(ur => ur.RatingTypeId == (int) type).Include(x => x.User);
        } 

        internal User RetrieveUser(int userId)
        {
            return FindUser(userId);
        }

        internal User RetrieveUser(string username)
        {
            return FindUser(username);
        }

        internal IEnumerable<User> RetrieveUsers(Func<User, bool> predicate)
        {
            return RetrieveUsers().Where(predicate);
        }

        private IEnumerable<User> RetrieveUsers()
        {
            return _context.Users;
        } 

        private User Update(User oldUser, User newUser)
        {
            _context.Entry(oldUser).CurrentValues.SetValues(newUser);
            _context.SaveChanges();
            return newUser;
        }

        internal User UpdateUser(User updatedUser)
        {
            var user = FindUser(updatedUser.UserId);
            return Update(user, updatedUser);
        }
    }
}
