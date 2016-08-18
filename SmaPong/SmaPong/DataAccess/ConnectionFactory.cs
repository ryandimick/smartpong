using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SmaPong.DataAccess
{
    public static class ConnectionFactory
    {
        /* TODO Make this an actual factory later */
        public static IDbConnection GetConnection()
        {
            IDbConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Testing"].ConnectionString;
            return connection;
        }
    }
}