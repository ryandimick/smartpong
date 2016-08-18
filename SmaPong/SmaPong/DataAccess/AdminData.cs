using System.Collections.Generic;
using System.Linq;
using Dapper;
using SmaPong.Models;

namespace SmaPong.DataAccess
{
    public class AdminData
    {
        public static ICollection<Admin> Retrieve()
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Query<Admin>(RetrieveAllSql).ToList();
            }
        }

        private const string RetrieveAllSql = "select * from dbo.Admins";
    }
}