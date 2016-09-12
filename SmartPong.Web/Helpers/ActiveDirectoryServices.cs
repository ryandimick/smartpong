using SmartPong.Models;
using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;

namespace SmartPong.Helpers
{
    public class ActiveDirectoryServices
    {
        public static User FetchUser(string username)
        {
            UserPrincipal userPrincipal;

            string domainName = ConfigurationManager.AppSettings["DomainName"];
            using (var pc = new PrincipalContext(ContextType.Domain, domainName))
            {
                userPrincipal = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, username);
            }

            if (userPrincipal == null)
            {
                throw new Exception("Active Directory user not found!");
            }

            User user = userPrincipal.ToUser(username);
            return user;
        }
    }
}