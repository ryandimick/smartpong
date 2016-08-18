//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace SmaPong.Business
//{
 
//    public class Match
//    {
//        public int Id { get; set; }
//        public int Teamid1 { get; set; }
//        public int Teamid2 { get; set; }
//        public int Roundnumber { get; set; }
//        public int Winner { get; set; }

//        public Match(int id, int teamid1, int teamid2, int roundnumber, int winner)
//        {
//            Id = id;
//            Teamid1 = teamid1;
//            Teamid2 = teamid2;
//            Roundnumber = roundnumber;
//            Winner = winner;
//        }
//    }

//    public class Tournament
//    {
//        public SortedList<int, SortedList<int, Match>> TournamentRoundMatches { get; private set; }
//        public Match ThirdPlaceMatch { get; private set; }

//        public Tournament(int rounds)
//        {
//            TournamentRoundMatches = new SortedList<int, SortedList<int, Match>>();
//            GenerateTournamentResults(rounds);
//            if (rounds > 1)
//            {
//                GenerateThirdPlaceResult(rounds);
//            }
//        }

//        public void AddMatch(Match m)
//        {
//            if (TournamentRoundMatches.ContainsKey(m.Roundnumber))
//            {
//                if (!TournamentRoundMatches[m.Roundnumber].ContainsKey(m.Id))
//                {
//                    TournamentRoundMatches[m.Roundnumber].Add(m.Id, m);
//                }
//            }
//            else
//            {
//                TournamentRoundMatches.Add(m.Roundnumber, new SortedList<int, Match>());
//                TournamentRoundMatches[m.Roundnumber].Add(m.Id, m);
//            }
//        }

//        private void GenerateTournamentResults(int rounds)
//        {
//            Random winnerRandomizer = new Random();

//            for (int round = 1, matchId = 1; round <= rounds; round++)
//            {
//                int matchesInRound = 1 << (rounds - round);
//                for (int roundMatch = 1; roundMatch <= matchesInRound; roundMatch++, matchId++)
//                {
//                    int team1Id;
//                    int team2Id;
//                    int winner;
//                    if (round == 1)
//                    {
//                        team1Id = (matchId * 2) - 1;
//                        team2Id = (matchId * 2);
//                    }
//                    else
//                    {
//                        int match1 = (matchId - (matchesInRound * 2) + (roundMatch - 1));
//                        int match2 = match1 + 1;
//                        team1Id = TournamentRoundMatches[round - 1][match1].Winner;
//                        team2Id = TournamentRoundMatches[round - 1][match2].Winner;
//                    }
//                    winner = (winnerRandomizer.Next(1, 3) == 1) ? team1Id : team2Id;
//                    AddMatch(new Match(matchId, team1Id, team2Id, round, winner));
//                }
//            }
//        }

//        private void GenerateThirdPlaceResult(int rounds)
//        {
//            Random winnerRandomizer = new Random();
//            int semifinalMatchid1 = TournamentRoundMatches[rounds - 1].Keys.ElementAt(0);
//            int semifinalMatchid2 = TournamentRoundMatches[rounds - 1].Keys.ElementAt(1);
//            Match semifinal1 = TournamentRoundMatches[rounds - 1][semifinalMatchid1];
//            Match semifinal2 = TournamentRoundMatches[rounds - 1][semifinalMatchid2];
//            int semifinalLoser1 = (semifinal1.Winner == semifinal1.Teamid1) ? semifinal1.Teamid2 : semifinal1.Teamid1;
//            int semifinalLoser2 = (semifinal2.Winner == semifinal2.Teamid1) ? semifinal2.Teamid2 : semifinal2.Teamid1;
//            int thirdPlaceWinner = (winnerRandomizer.Next(1, 3) == 1) ? semifinalLoser1 : semifinalLoser2;
//            ThirdPlaceMatch = new Match((1 << rounds) + 1, semifinalLoser1, semifinalLoser2, 1, thirdPlaceWinner);
//        }
//    }
//}