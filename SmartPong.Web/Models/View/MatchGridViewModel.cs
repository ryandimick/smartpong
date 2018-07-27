using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebGrease.Css.Extensions;

namespace SmartPong.Models.View
{
    public class MatchGridViewModel
    {
        public int MatchId { get; set; }
        public DateTime? MatchDate { get; set; }
        public MatchUserViewModel Winner { get; set; }
        public MatchUserViewModel Loser { get; set; }
        public MatchStatus Status { get; set; }
        public string WinnerOne { get; set; }
        public string LoserOne { get; set; }
        public string StatusText { get; set; }
        public string MatchType { get; set; }
    }

    public static class MatchExtensions
    {
        public static IEnumerable<MatchGridViewModel> ToGridViewModel(this IEnumerable<Match> matches)
        {
            var list = new List<MatchGridViewModel>();
            foreach (var mt in matches)
            {
                var winner = mt.MatchParticipants.Where(mp => mp.MatchTeamId == mt.WinningTeam);
                var loser = mt.MatchParticipants.Where(mp => mp.MatchTeamId != mt.WinningTeam);
                var status = Enum.GetName(typeof(MatchStatus), mt.Status);
                var matchType = Enum.GetName(typeof(MatchType.Type), mt.MatchTypeId);
                
                string ratingsChange = "";
                List<StringBuilder> db = null;

                if (matchType == "Doubles")
                {
                    db = DoublesParticipants(winner, loser);
                }
                
                var match = new MatchGridViewModel
                {
                    MatchDate = mt.MatchDate,
                    StatusText = status,
                    Status = mt.Status,
                    MatchId = mt.MatchId,
                    Loser =  new MatchUserViewModel(mt.MatchId, loser.First().UserId, loser.First().User.DisplayName, ratingsChange),
                    Winner = new MatchUserViewModel(mt.MatchId, winner.First().UserId, winner.First().User.DisplayName, ratingsChange),
                    LoserOne = matchType == "Doubles" ? db.First().ToString() : loser.First().User.DisplayName,
                    WinnerOne = matchType== "Doubles" ? db.Last().ToString() : winner.First().User.DisplayName,
                    MatchType = matchType
                };
                list.Add(match);
            }

            return list;
        }

        private static List<StringBuilder> DoublesParticipants(IEnumerable<MatchParticipant> winner, IEnumerable<MatchParticipant> loser)
        {
            var teams = new List<StringBuilder>();

            var wTeam = new StringBuilder();
            var lTeam = new StringBuilder();

            winner.ForEach(x => wTeam.Append(x.User.DisplayName).Append("   "));
            loser.ForEach(x => lTeam.Append(x.User.DisplayName).Append("   "));
            
            teams.Add(lTeam);
            teams.Add(wTeam);

            return teams;
        }
    }
}