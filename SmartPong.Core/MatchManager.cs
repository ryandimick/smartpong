using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Serialization;
using Moserware.Skills;
using SmartPong.Models;
using Team = Moserware.Skills.Team;

namespace SmartPong
{
    internal class MatchManager
    {
        private readonly SmartPongContext _context;

        internal MatchManager(SmartPongContext context)
        {
            _context = context;
        }

        internal Match ConfirmMatch(int matchId, int userId)
        {
            var match = _context.Matches.Include(x => x.MatchParticipants).First(m => m.MatchId == matchId);
            match.ConfirmDate = DateTime.Now;
            match.ConfirmUser = userId;

            bool dirty = false;

            foreach (var lastMatch in match.MatchParticipants.Select(participant => FindLastMatch(participant.UserId, match.MatchDate.Value)))
            {
                if (lastMatch != null && lastMatch.Status < MatchStatus.Posted)
                {
                    dirty = true;
                    break;
                }
            }

            if (dirty)
            {
                match.Status = MatchStatus.Pending;
            }
            else
            {
                match.Status = MatchStatus.Posted;
            }

            var firstUnconfirmedMatch =
                _context.Matches.Where(m => m.Status == MatchStatus.Submitted)
                    .Include(x => x.MatchParticipants)
                    .OrderBy(m => m.MatchDate)
                    .First();

            var dirtyUsers = new List<User>();
            DateTime updateDate;

            if (firstUnconfirmedMatch.MatchDate.Value < match.MatchDate.Value)
            {
                updateDate = firstUnconfirmedMatch.MatchDate.Value;
                foreach (var participant in firstUnconfirmedMatch.MatchParticipants.Where(participant => dirtyUsers.All(u => u.UserId != participant.UserId)))
                {
                    dirtyUsers.Add(_context.Users.First(u => u.UserId == participant.UserId));
                }
            }
            else
            {
                updateDate = match.MatchDate.Value;
            }

            _context.Matches.Attach(match);
            _context.Entry(match).State = EntityState.Modified;
            _context.SaveChanges();

            UpdateNewerMatches(match, match.MatchTypeId, updateDate, dirtyUsers);

            return match;
        }

        internal Match CreateMatch(Match newMatch)
        {
            newMatch.ValidateInput();

            newMatch.CreateDate = DateTime.Now;
            newMatch.Status = MatchStatus.Submitted;

            ProcessMatch(newMatch);

            _context.Matches.Add(newMatch);
            _context.SaveChanges();

            var participants = from participant in newMatch.MatchParticipants
                               join user in _context.Users.ToList() on participant.UserId equals user.UserId
                               select user;

            UpdateNewerMatches(newMatch, newMatch.MatchTypeId, newMatch.MatchDate.Value, participants.ToList());

            return newMatch;
        }

        internal void DeleteMatch(int matchId)
        {
            var match = _context.Matches.First(m => m.MatchId == matchId);
            _context.Matches.Remove(match);
            _context.SaveChanges();

            var firstUnconfirmedMatch =
                    _context.Matches.Where(m => m.Status == MatchStatus.Submitted)
                        .Include(x => x.MatchParticipants)
                        .OrderBy(m => m.MatchDate)
                        .First();

            var dirtyUsers = new List<User>();
            DateTime updateDate;

            if (firstUnconfirmedMatch.MatchDate.Value < match.MatchDate.Value)
            {
                updateDate = firstUnconfirmedMatch.MatchDate.Value;
                foreach (var participant in firstUnconfirmedMatch.MatchParticipants.Where(participant => dirtyUsers.All(u => u.UserId != participant.UserId)))
                {
                    dirtyUsers.Add(_context.Users.First(u => u.UserId == participant.UserId));
                }
            }
            else
            {
                updateDate = match.MatchDate.Value;
            }

            UpdateNewerMatches(match, match.MatchTypeId, updateDate, dirtyUsers);
        }

        private Match FindLastMatch(int userId, DateTime matchDate)
        {
            return
                _context.Matches.Where(m => m.MatchDate < matchDate)
                    .Where(m => m.MatchParticipants.Any(mp => mp.UserId == userId))
                    .OrderByDescending(m => m.MatchDate)
                    .FirstOrDefault();
        }

        private void ProcessMatch(Match match)
        {
            var serializer = new JavaScriptSerializer();
            var ratings = new Dictionary<int, TrueskillRatingChange>();

            foreach (var participant in match.MatchParticipants.OrderBy(mp => mp.MatchTeamId).ThenBy(mp => mp.UserId))
            {
                var lastMatch = FindLastMatch(participant.UserId, match.MatchDate.Value);
                TrueskillRatingChange oldRating;
                if (lastMatch == null)
                {
                    oldRating = new TrueskillRatingChange
                    {
                        OldSkill = GameInfo.DefaultGameInfo.DefaultRating.Mean,
                        NewSkill = GameInfo.DefaultGameInfo.DefaultRating.Mean,
                        OldVariance = GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation,
                        NewVariance = GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation
                    };
                }
                else
                {
                    var rating = _context.MatchUserRatings.First(mur => mur.MatchId == lastMatch.MatchId && mur.UserId == participant.UserId);
                    //if (rating.RatingTypeId == 1)
                    oldRating = serializer.Deserialize<TrueskillRatingChange>(rating.RatingData);
                }
                ratings.Add(participant.UserId, oldRating);
            }

            var firstDbTeam = new Team();
            var secondDbTeam = new Team();

            foreach (var participant in match.MatchParticipants.Where(w => w.MatchTeamId == 1))
            {
                firstDbTeam.AddPlayer(new Player(participant.UserId),
                    new Rating(ratings[participant.UserId].NewSkill, ratings[participant.UserId].NewVariance));
            }

            foreach (var participant in match.MatchParticipants.Where(w => w.MatchTeamId == 2))
            {
                secondDbTeam.AddPlayer(new Player(participant.UserId),
                    new Rating(ratings[participant.UserId].NewSkill, ratings[participant.UserId].NewVariance));
            }

            var dbTeam = Teams.Concat(firstDbTeam, secondDbTeam);

            var newRatings = match.WinningTeam == 1
                ? TrueSkillCalculator.CalculateNewRatings(GameInfo.DefaultGameInfo, dbTeam, 1, 2)
                : TrueSkillCalculator.CalculateNewRatings(GameInfo.DefaultGameInfo, dbTeam, 2, 1);

            match.MatchUserRatings = new List<MatchUserRating>();
            for (int i = 0; i < match.MatchParticipants.Count; i++)
            {
                var participant = match.MatchParticipants.ElementAt(i);
                var oldRating = ratings[participant.UserId];
                var newRating = newRatings.ElementAt(i).Value;
                var trueskillRatingChange = new TrueskillRatingChange
                {
                    OldSkill = oldRating.NewSkill,
                    OldVariance = oldRating.NewVariance,
                    NewSkill = newRating.Mean,
                    NewVariance = newRating.StandardDeviation
                };

                var matchUserRating = new MatchUserRating
                {
                    RatingTypeId = match.MatchParticipants.Count == 4 ? 2 : 1,
                    UserId = participant.UserId,
                    RatingData = serializer.Serialize(trueskillRatingChange)
                };
                match.MatchUserRatings.Add(matchUserRating);
            }
        }

        internal IEnumerable<Match> RetrieveMatches()
        {
            return _context.Matches.Include(x => x.MatchParticipants);
        }

        internal IEnumerable<Match> RetrieveMatches(Func<Match, bool> predicate)
        {
            return RetrieveMatches().Where(predicate);
        }

        private void UpdateNewerMatches(Match newMatch, int matchType, DateTime matchDate, ICollection<User> dirtyUsers)
        {
            var matches = _context.Matches.Where(m => m.MatchDate > matchDate && m.MatchTypeId == matchType).OrderBy(m => m.MatchDate).Include(x => x.MatchParticipants).ToList();
            foreach (var match in matches)
            {
                var userRatings = _context.MatchUserRatings.Where(mur => mur.MatchId == match.MatchId);
                _context.MatchUserRatings.RemoveRange(userRatings);
                ProcessMatch(match);
                if (match.ConfirmDate == null || match.MatchParticipants.Any(participant => dirtyUsers.Any(u => u.UserId == participant.UserId)))
                {
                    foreach (var participant in match.MatchParticipants.Where(participant => dirtyUsers.All(u => u.UserId != participant.UserId)))
                    {
                        dirtyUsers.Add(_context.Users.First(u => u.UserId == participant.UserId));
                    }

                    if (match.ConfirmDate == null)
                    {
                        match.Status = MatchStatus.Submitted;
                    }
                    else
                    {
                        match.Status = MatchStatus.Pending;
                    }
                }
                else
                {
                    match.Status = MatchStatus.Posted;
                }
                var oldMatch = _context.Matches.First(m => m.MatchId == match.MatchId);
                _context.Entry(oldMatch).CurrentValues.SetValues(match);
                _context.SaveChanges();
            }

            UpdateUserRatings(newMatch);
        }

        private void UpdateUserRatings(Match newMatch)
        {
            var serializer = new JavaScriptSerializer();
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                var rateTypeId = newMatch.MatchUserRatings.First().RatingTypeId;
                var userRatings = _context.UserRatings.Where(ur => ur.UserId == user.UserId && ur.RatingTypeId == rateTypeId);

                //var userRatings = _context.UserRatings.FirstOrDefault(ur =>
                //  ur.UserId == user.UserId);
                //todo retrieve userRatings with ratingType filtered?

                _context.UserRatings.RemoveRange(userRatings);

                var lastPostedMatch =
                    _context.Matches
                        .Include(x => x.MatchUserRatings)
                        .Where(
                            m =>
                                m.Status == MatchStatus.Posted &&
                                m.MatchParticipants.Any(mp => mp.UserId == user.UserId))
                        .OrderByDescending(m => m.MatchDate)
                        .FirstOrDefault();

                TrueskillRating trueskillRating;

                if (lastPostedMatch != null)
                {
                    var lastRatingChange = lastPostedMatch.MatchUserRatings.First(mur => mur.UserId == user.UserId);
                    var trueskillRatingChange =
                        serializer.Deserialize<TrueskillRatingChange>(lastRatingChange.RatingData);
                    trueskillRating = new TrueskillRating
                    {
                        Skill = trueskillRatingChange.NewSkill,
                        Variance = trueskillRatingChange.NewVariance
                    };
                }
                else
                {
                    trueskillRating = new TrueskillRating
                    {
                        Skill = GameInfo.DefaultGameInfo.DefaultRating.Mean,
                        Variance = GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation
                    };
                }

                var userRating = new UserRating
                {//todo leave the ratetypeId
                    RatingTypeId = rateTypeId,//userRatings.First().RatingTypeId == (int) UserRatingType.TrueskillDoubles ? 2: 1,
                    UserId = user.UserId,
                    RatingData = serializer.Serialize(trueskillRating)
                };

                _context.UserRatings.Add(userRating);
            }
            _context.SaveChanges();
        }
    }
}
