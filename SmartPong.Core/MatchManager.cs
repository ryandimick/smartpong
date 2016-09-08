using System;
using SmartPong.Exceptions;
using SmartPong.Models;

namespace SmartPong
{
    internal class MatchManager
    {
        private readonly SmartPongContext _context;

        internal MatchManager(SmartPongContext context)
        {
            _context = context;
        }

        internal Match ConfirmMatch()
        {
            throw new NotImplementedException();
        }

        internal Match CreateMatch(MatchType.Type matchType)
        {
            var newMatch = new Match
            {
                MatchTypeId = (int) matchType,
                Status = (int) MatchStatus.Type.Scheduled
            };

            return CreateNewMatch(newMatch);
        }

        internal Match CreateMatch(Match newMatch)
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

            newMatch.CreateDate = DateTime.Now;
            newMatch.Status = (int) MatchStatus.Type.Submitted;

            // create match
            
            /* do i need to do this with EF? */
            newMatch.UpdateParticipantsMatchInfo();

            // create match participants

            // begin ratings

            // create ratings

            // save ratings

            throw new NotImplementedException();
        }

        private Match CreateNewMatch(Match newMatch)
        {
            newMatch.CreateDate = DateTime.Now;

            _context.Matches.Add(newMatch);
            _context.SaveChanges();

            return newMatch;
        }

        internal void DeleteMatch(int matchId)
        {
            throw new NotImplementedException();
        }

        internal Match UpdateMatch(Match updatedMatch)
        {
            /* TODO
             * Stubbed out for future tournament functionality 
             */
            throw new NotImplementedException();
        }
    }
}
