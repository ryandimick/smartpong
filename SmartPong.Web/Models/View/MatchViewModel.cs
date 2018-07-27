using System;
using System.Linq;

namespace SmartPong.Models.View
{
    public class MatchViewModel
    {
        private readonly Match _match;

        public MatchViewModel(Match match)
        {
            _match = match;

            if (match.MatchTypeId == 1)
            {
                var winner = match.MatchParticipants.First(mp => mp.MatchTeamId == match.WinningTeam);
                var rating = match.MatchUserRatings.First(r => r.UserId == winner.UserId);
                string ratingsChange = "";
                WinnerOne = new MatchUserViewModel(match.MatchId, winner.UserId, winner.User.DisplayName, ratingsChange);

                var loser = match.MatchParticipants.First(mp => mp.MatchTeamId == match.WinningTeam);
                rating = match.MatchUserRatings.First(r => r.UserId == loser.UserId);
                ratingsChange = "";
                LoserOne = new MatchUserViewModel(match.MatchId, loser.UserId, loser.User.DisplayName, ratingsChange);
            }
            else
            {

            }
        }

        public int MatchId => _match.MatchId;

        public DateTime MatchDate => _match.MatchDate.Value;

        public MatchStatus Status => _match.Status;

        public MatchUserViewModel WinnerOne { get; private set; }

        public MatchUserViewModel WinnerTwo { get; private set; }

        public MatchUserViewModel LoserOne { get; private set; }

        public MatchUserViewModel LoserTwo { get; private set; }
    }
}