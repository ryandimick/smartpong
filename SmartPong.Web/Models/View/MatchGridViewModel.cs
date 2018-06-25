using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPong.Models.View
{
    public class MatchGridViewModel
    {
        public int MatchId { get; set; }
        public DateTime? MatchDate { get; set; }
        public MatchUserViewModel Winner { get; set; }
        public MatchUserViewModel Loser { get; set; }
        public int Status { get; set; }
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
                var winner = mt.MatchParticipants.First(mp => mp.MatchTeamId == mt.WinningTeam);
                var loser = mt.MatchParticipants.First(mp => mp.MatchTeamId != mt.WinningTeam);
                var status = Enum.GetName(typeof(MatchStatus.Type), mt.Status);
                var matchType = Enum.GetName(typeof(MatchType.Type), mt.MatchTypeId);
                
                string ratingsChange = "";

                var match = new MatchGridViewModel
                {
                    MatchDate = mt.MatchDate,
                    StatusText = status,
                    Status = mt.Status,
                    MatchId = mt.MatchId,
                    Loser = new MatchUserViewModel(mt.MatchId, loser.UserId, loser.User.DisplayName, ratingsChange),
                    Winner = new MatchUserViewModel(mt.MatchId, winner.UserId, winner.User.DisplayName, ratingsChange),
                    LoserOne = loser.User.DisplayName,
                    WinnerOne = winner.User.DisplayName,
                    MatchType = matchType
                };
                list.Add(match);
            }

            return list;
        }
    }
}