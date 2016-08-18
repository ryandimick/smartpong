using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moserware.Skills;
using SmaPong.DataAccess;
using SmaPong.Models;

namespace SmaPong.Business
{
    public static class MatchBusiness
    {
        public class PlayerRating
        {
            public int Id { get; set; }
            public double FinalMu { get; set; }
            public double FinalSigma { get; set; }
            public bool Locked { get; set; }
            public DateTime MatchDate { get; set; }
            public double NewMu { get; set; }
            public double NewSigma { get; set; }
            public double OldMu { get; set; }
            public double OldSigma { get; set; }
        }

        private static void CalculateRating(PlayerRating playerOne, PlayerRating playerTwo, int winner)
        {
            var gameInfo = GameInfo.DefaultGameInfo;

            var p1 = new Moserware.Skills.Player(playerOne.Id);
            var p2 = new Moserware.Skills.Player(playerTwo.Id);

            var t1 = new Team(p1, new Rating(playerOne.NewMu, playerOne.NewSigma));
            var t2 = new Team(p2, new Rating(playerTwo.NewMu, playerTwo.NewSigma));

            var teams = Teams.Concat(t1, t2);

            IDictionary<Moserware.Skills.Player, Rating> newRatings;
            switch (winner)
            {
                case 1:
                    newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 1, 2);
                    break;
                case 2:
                    newRatings = TrueSkillCalculator.CalculateNewRatings(gameInfo, teams, 2, 1);
                    break;
                default:
                    throw new NotImplementedException();
            }

            playerOne.OldMu = playerOne.NewMu;
            playerOne.OldSigma = playerOne.NewSigma;
            playerTwo.OldMu = playerTwo.NewMu;
            playerTwo.OldSigma = playerTwo.NewSigma;
            playerOne.NewMu = newRatings[p1].Mean;
            playerOne.NewSigma = newRatings[p1].StandardDeviation;
            playerTwo.NewMu = newRatings[p2].Mean;
            playerTwo.NewSigma = newRatings[p2].StandardDeviation;
        }

        private static bool CheckForOutstandingMatches(DateTime matchDate, IEnumerable<PlayerMatch> playerOneMatches,
            IEnumerable<PlayerMatch> playerTwoMatches)
        {
            return playerOneMatches.Any(m => m.MatchDate < matchDate && m.ConfirmationDate == null) ||
                   playerTwoMatches.Any(m => m.MatchDate < matchDate && m.ConfirmationDate == null);
        }

        public static void Confirm(int matchId)
        {
            var dtNow = DateTime.Now;

            using (var connection = ConnectionFactory.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var match = Global.AllMatches.Single(m => m.Id == matchId);
                    var playerOne = Global.Players.Single(p => p.Id == match.PlayerOneId);
                    var playerOneRating = FindLastGoodRating(playerOne, match.MatchDate);
                    var playerTwo = Global.Players.Single(p => p.Id == match.PlayerTwoId);

                    var playerTwoRating = FindLastGoodRating(playerTwo, match.MatchDate);

                    if (!CheckForOutstandingMatches(match.MatchDate, playerOne.Matches, playerTwo.Matches))
                    {
                        var liableMatches = Global.AllMatches.Where(m => m.MatchDate >= match.MatchDate);
                        PostUpdates(connection, transaction, liableMatches, playerOneRating, playerTwoRating, dtNow, matchId);
                    }
                    MatchData.Confirm(connection, transaction, matchId, dtNow);
                    transaction.Commit();
                }
            }

            Global.LoadAll();
        }

        public static void CreateMatch(NewMatch newMatch)
        {

            var playerOne = Global.Players.Single(p => p.Id == newMatch.PlayerOneId);
            var playerOneRating = FindLastGoodRating(playerOne, newMatch.MatchDate);
            var playerTwo = Global.Players.Single(p => p.Id == newMatch.PlayerTwoId);
            var playerTwoRating = FindLastGoodRating(playerTwo, newMatch.MatchDate);

            CalculateRating(playerOneRating, playerTwoRating, newMatch.Placement);

            var match = new Match
            {
                MatchDate = newMatch.MatchDate,
                PlayerOneId = playerOneRating.Id,
                PlayerOneOldMu = playerOneRating.OldMu,
                PlayerOneOldSigma = playerOneRating.OldSigma,
                PlayerTwoId = playerTwoRating.Id,
                PlayerTwoOldMu = playerTwoRating.OldMu,
                PlayerTwoOldSigma = playerTwoRating.OldSigma,
                PlayerOneNewMu = playerOneRating.NewMu,
                PlayerOneNewSigma = playerOneRating.NewSigma,
                PlayerTwoNewMu = playerTwoRating.NewMu,
                PlayerTwoNewSigma = playerTwoRating.NewSigma,
                WinningPlayerId = newMatch.Placement == 1 ? playerOneRating.Id : playerTwoRating.Id
            };

            lock (Global.Lock)
            {
                MatchData.Create(match);
            }
            Global.LoadAll();

            var to = (playerTwo.Notifications && !string.IsNullOrWhiteSpace(playerTwo.Email)) ? playerTwo.Email : null;

            if (to == null)
                return;

            var subject = string.Format("{0} has submitted a match pending your approval, {1}", playerOne.Name,
                "http://smartpong/Matches/Pending");
            var body = string.Format("Match Date: {0}, Your Result: {1}", newMatch.MatchDate,
                newMatch.Placement == 1 ? "Loss" : "Win");

            NotificationBusiness.Send(to, subject, body);
        }

        public static void Delete(int matchId)
        {
            var dtNow = DateTime.Now;
            var match = Global.AllMatches.Single(m => m.Id == matchId);
            var playerOne = Global.Players.Single(p => p.Id == match.PlayerOneId);
            var playerTwo = Global.Players.Single(p => p.Id == match.PlayerTwoId);

            lock (Global.Lock)
            {
                using (var connection = ConnectionFactory.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {

                        var playerOneRating = FindLastGoodRating(playerOne, match.MatchDate);
                        var playerTwoRating = FindLastGoodRating(playerTwo, match.MatchDate);
                        var liableMatches =
                            Global.AllMatches.Where(m => m.MatchDate >= match.MatchDate && m.Id != match.Id);

                        PostUpdates(connection, transaction, liableMatches, playerOneRating, playerTwoRating, dtNow);

                        MatchData.Delete(connection, transaction, matchId);
                        transaction.Commit();
                    }
                }
            }

            Global.LoadAll();


            var to = (playerTwo.Notifications && !string.IsNullOrWhiteSpace(playerTwo.Email)) ? playerTwo.Email : null;

            if (to == null)
                return;

            var subject = string.Format("{0} has contested and deleted a match against you!", playerTwo.Name);
            var body = string.Empty;
            NotificationBusiness.Send(to, subject, body);
        }

        private static PlayerRating FindLastGoodRating(PlayerDetail player, DateTime matchDate)
        {
            double mu;
            double sigma;
            DateTime lastMatchDate;

            var goodMatches = player.Matches.Where(m => m.MatchDate < matchDate).OrderByDescending(m => m.MatchDate).ToList();
            if (!goodMatches.Any())
            {
                mu = GameInfo.DefaultGameInfo.DefaultRating.Mean;
                sigma = GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation;
                lastMatchDate = new DateTime(1900, 1, 1);
            }
            else
            {
                var lastMatch = goodMatches[0];
                mu = lastMatch.Mu;
                sigma = lastMatch.Sigma;
                lastMatchDate = lastMatch.MatchDate;
            }

            return new PlayerRating
            {
                Id = player.Id,
                NewMu = mu,
                NewSigma = sigma,
                Locked = false,
                MatchDate = lastMatchDate,
                FinalMu = mu,
                FinalSigma = sigma
            };
        }

        private static void PostUpdates(IDbConnection connection, IDbTransaction transaction, IEnumerable<Match> suspectMatches,
            PlayerRating playerOne, PlayerRating playerTwo, DateTime now, int forcedMatchId = 0)
        {
            suspectMatches = suspectMatches.OrderBy(m => m.MatchDate);
            var affectedPlayers = new List<PlayerRating> {playerOne, playerTwo};
            var affectedMatches = new List<Match>();

            foreach (var suspectMatch in suspectMatches.Where(suspectMatch => affectedPlayers.Any(p => p.Id == suspectMatch.PlayerOneId || p.Id == suspectMatch.PlayerTwoId)))
            {
                if (affectedPlayers.All(p => p.Id != suspectMatch.PlayerOneId))
                {
                    var player = Global.Players.Single(p => p.Id == suspectMatch.PlayerOneId);
                    var playerRating = FindLastGoodRating(player, suspectMatch.MatchDate);
                    affectedPlayers.Add(playerRating);
                }

                if (affectedPlayers.All(p => p.Id != suspectMatch.PlayerTwoId))
                {
                    var player = Global.Players.Single(p => p.Id == suspectMatch.PlayerTwoId);
                    var playerRating = FindLastGoodRating(player, suspectMatch.MatchDate);
                    affectedPlayers.Add(playerRating);
                }

                var firstPlayer = affectedPlayers.Single(p => p.Id == suspectMatch.PlayerOneId);
                var secondPlayer = affectedPlayers.Single(p => p.Id == suspectMatch.PlayerTwoId);

                CalculateRating(firstPlayer, secondPlayer,
                    suspectMatch.WinningPlayerId == suspectMatch.PlayerOneId ? 1 : 2);

                firstPlayer.MatchDate = suspectMatch.MatchDate;
                secondPlayer.MatchDate = suspectMatch.MatchDate;

                if (suspectMatch.ConfirmationDate == null && suspectMatch.Id != forcedMatchId)
                {
                    firstPlayer.Locked = true;
                    secondPlayer.Locked = true;
                }
                else
                {
                    if (firstPlayer.Locked || secondPlayer.Locked)
                        continue;
                    firstPlayer.FinalMu = firstPlayer.NewMu;
                    firstPlayer.FinalSigma = firstPlayer.NewSigma;
                    secondPlayer.FinalMu = secondPlayer.NewMu;
                    secondPlayer.FinalSigma = secondPlayer.NewSigma;
                }

                affectedMatches.Add(new Match
                {
                    Id = suspectMatch.Id,
                    PostDate = !firstPlayer.Locked && !secondPlayer.Locked ? now : (DateTime?) null,
                    PlayerOneOldMu = firstPlayer.OldMu,
                    PlayerOneNewMu = firstPlayer.NewMu,
                    PlayerOneOldSigma = firstPlayer.OldSigma,
                    PlayerOneNewSigma = firstPlayer.NewSigma,
                    PlayerTwoOldMu = secondPlayer.OldMu,
                    PlayerTwoNewMu = secondPlayer.NewMu,
                    PlayerTwoOldSigma = secondPlayer.OldSigma,
                    PlayerTwoNewSigma = secondPlayer.NewSigma

                });
            }

            MatchData.Update(connection, transaction, affectedMatches);
            PlayerData.UpdateRating(connection, transaction, affectedPlayers);
        }
    }
}