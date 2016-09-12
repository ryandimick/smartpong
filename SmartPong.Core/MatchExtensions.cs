using System;
using System.Collections.Generic;
using SmartPong.Models;

namespace SmartPong
{
    public static class MatchExtensions
    {
        public static void AddTeam(this Match match, int teamId, IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                var newParticipant = new MatchParticipant
                {
                    MatchId = match.MatchId,
                    MatchTeamId = teamId,
                    UserId = user.UserId
                };
                match.MatchParticipants.Add(newParticipant);
            }
        }

        public static void AddUser(this Match match, User user, int teamId)
        {
            throw new NotImplementedException();
        }

        public static void SetOutcome(this Match match, int winningTeamId)
        {
            match.WinningTeam = winningTeamId;
        }
    }
}
