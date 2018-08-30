using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SmaPong.Models;
using SmartPong.Models;

namespace SmaPong.DataAccess
{
    public class MatchData
    {
        public static int Confirm(IDbConnection connection, IDbTransaction transaction, int Id, DateTime? ConfirmationDate)
        {
            return connection.Execute(UpdateConfirmationDateSql, new {Id, ConfirmationDate}, transaction);
        }

        public static int Create(Match match)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Execute(CreateSql, match);
            }
        }

        private const string CreateSql = "insert into dbo.Matches (MatchDate, PlayerOneId, PlayerOneOldMu, PlayerOneNewMu, PlayerOneNewSigma, PlayerOneOldSigma, PlayerTwoId, PlayerTwoOldMu, PlayerTwoNewMu, PlayerTwoOldSigma, PlayerTwoNewSigma, WinningPlayerId) values (@MatchDate, @PlayerOneId, @PlayerOneOldMu, @PlayerOneNewMu, @PlayerOneNewSigma, @PlayerOneOldSigma, @PlayerTwoId, @PlayerTwoOldMu, @PlayerTwoNewMu, @PlayerTwoOldSigma, @PlayerTwoNewSigma, @WinningPlayerId)";

        public static int Delete(IDbConnection connection, IDbTransaction transaction, int id)
        {
            return connection.Execute(DeleteSql, new {id}, transaction);
        }

        private const string DeleteSql = "delete from dbo.Matches where id = @id";

        public static List<Match> Retrieve()
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Query<Match>(RetrieveAllSql).ToList();
            }
        }

        private const string RetrieveAllSql = "select * from dbo.Matches";

        public List<MatchUserRating> RetrieveMatchUserRatings()
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Query<MatchUserRating>(RetrieveAllUserRatings).ToList();
            }
        }

        private const string RetrieveAllUserRatings = "select * from dbo.MatchUserRatings";

        public static int Update(IDbConnection connection, IDbTransaction transaction, IEnumerable<Match> matches)
        {
            return connection.Execute(UpdateSql, matches, transaction);
        }

        private const string UpdateSql =
            "update dbo.Matches set playeroneoldmu = @PlayerOneOldMu, playeronenewmu = @PlayerOneNewMu, playeroneoldsigma = @PlayerOneOldSigma, playeronenewsigma = @PlayerOneNewSigma, playertwooldmu = @PlayerTwoOldMu, playertwonewmu = @PlayerTwoNewMu, playertwooldsigma = @PlayerTwoOldSigma, playertwonewsigma = @PlayerTwoNewSigma, postdate = @PostDate where id = @Id";
        
        private const string UpdateConfirmationDateSql =
            "update dbo.Matches set confirmationdate = @ConfirmationDate where id = @Id";
    }
}