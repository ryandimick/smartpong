using System;
using System.Collections.Generic;
using SmartPong.Models;
using SmartPong.Exceptions;

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

        public static void ValidateInput(this Match newMatch)
        {
            if (newMatch.MatchDate == null)
            {
                throw new InvalidMatchException("Match Date cannot be null!");
            }

            if (newMatch.MatchParticipants == null || newMatch.MatchParticipants.Count < 2)
            {
                throw new InvalidMatchException("Insufficient match participants provided!");
            }

            if (newMatch.WinningTeam == null)
            {
                throw new InvalidMatchException("The match winning team cannot be null!");
            }
        }
    }
}
