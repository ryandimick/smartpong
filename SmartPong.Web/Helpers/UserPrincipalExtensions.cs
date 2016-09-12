using SmartPong.Models;
using System.DirectoryServices.AccountManagement;

namespace SmartPong.Helpers
{
    public static class UserPrincipalExtensions
    {
        public static User ToUser(this UserPrincipal userPrincipal, string username)
        {
            User user = new User
            {
                Username = username,
                GivenName = userPrincipal.GivenName,
                Surname = userPrincipal.Surname,
                Email = userPrincipal.EmailAddress,
                Notifications = !string.IsNullOrWhiteSpace(userPrincipal.EmailAddress)
            };

            return user;
        }
    }
}