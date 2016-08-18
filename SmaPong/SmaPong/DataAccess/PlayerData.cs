using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SmaPong.Business;
using SmaPong.Models;

namespace SmaPong.DataAccess
{
    public static class PlayerData
    {
        public static int Create(Player player)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Execute(CreateSql, player);
            }
        }

        private const string CreateSql =
            "insert into dbo.Players (Username, FirstName, Surname, CreateDate, ActivityDate, Mu, Sigma, Nickname, Email, Notifications, Active) values (@Username, @FirstName, @Surname, @CreateDate, @ActivityDate, @Mu, @Sigma, @Nickname, @Email, @Notifications, @Active)";

        public static List<Player> Retrieve()
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Query<Player>(RetrieveAllSql).ToList();
            }
        }

        public static Player Retrieve(string username)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Query<Player>(RetrieveSql, new {username}).SingleOrDefault();
            }
        }

        private const string RetrieveAllSql = "select * from dbo.Players";

        private const string RetrieveSql = "select * from dbo.Players where username = @username";

        public static int UpdateDetails(Player player)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Execute(UpdateDetailsSql, player);
            }
        }

        private const string UpdateDetailsSql =
            "update dbo.Players set firstname = @FirstName, surname = @Surname, nickname = @Nickname, email = @Email, notifications = @Notifications where id = @Id;";

        public static int UpdateImage(Player player)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Execute(UpdateImageSql, player);
            }
        }

        private const string UpdateImageSql = "update dbo.Players set image = @Image where id = @Id;";

        public static int UpdateRating(IDbConnection connection, IDbTransaction transaction,
            IEnumerable<MatchBusiness.PlayerRating> players)
        {
            return connection.Execute(UpdateRatingSql, players, transaction);
        }

        private const string UpdateRatingSql =
            "update dbo.Players set mu = @FinalMu, sigma = @FinalSigma, activitydate = @MatchDate where id = @Id";
    }
}