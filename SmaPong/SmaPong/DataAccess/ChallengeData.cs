using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Dapper;
using SmaPong.Models;

namespace SmaPong.DataAccess
{
    internal class ChallengeData
    {
        internal static int Create(Challenge challenge)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Execute(CreateSql, challenge);
            }
        }

        private const string CreateSql =
            "insert into dbo.Challenges values(@ChallengerId, @ChallengeeId, @Timestamp, @Message, @Response, @Status);";

        //internal static IEnumerable<ChallengeViewModel> RetrieveUpcoming()
        //{
            
        //}

        internal static int Update(Challenge challenge)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                return connection.Execute(UpdateSql, challenge);
            }
        }

        private const string UpdateSql =
            "update dbo.Challenges set response = @Response, status = @Status where id = @Id;";
    }
}