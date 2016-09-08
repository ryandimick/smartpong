using System.Collections.Generic;
using SmartPong.Models;

namespace SmartPong
{
    public static class MatchExtensions
    {
        public static void AddTeam(this Match match, int teamId, IEnumerable<User> users)
        {
            
        }

        public static void AddUser(this Match match, User user, int teamId)
        {
            
        }

        internal static void PostRatings(this Match match)
        {
            foreach (var participant in match.MatchParticipants)
            {
                // find last good match 
                // foreach rating type calculate based on last good rating
            }
        }

        public static void SetOutcome(this Match match, int winningTeamId)
        {
            match.WinningTeam = winningTeamId;
        }

        internal static void UpdateParticipantsMatchInfo(this Match match)
        {
            foreach (MatchParticipant participant in match.MatchParticipants)
            {
                participant.MatchId = match.MatchId;
            }
        }

        public static void UpdateStatus(this Match match)
        {
            if (match.WinningTeam == null)
            {
                match.Status = (int) MatchStatus.Type.Scheduled;
            }
            else if (match.ConfirmDate == null)
            {
                match.Status = (int) MatchStatus.Type.Submitted;
            }
            else if (match.PostDate == null)
            {
                match.Status = (int) MatchStatus.Type.Pending;
            }
            else
            {
                match.Status = (int) MatchStatus.Type.Posted;
            }
        }
    }
}
