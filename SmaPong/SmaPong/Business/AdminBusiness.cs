using System;
using System.Linq;

namespace SmaPong.Business
{
    public class AdminBusiness
    {
        public static bool IsAdmin(string userName)
        {
            return Global.Admins.Any(a => string.Equals(a.Username, userName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}