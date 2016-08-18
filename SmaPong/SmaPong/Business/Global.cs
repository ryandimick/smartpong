using System.Collections.Generic;
using System.Linq;
using SmaPong.DataAccess;
using SmaPong.Models;

namespace SmaPong.Business
{
    public static class Global
    {

        private static ICollection<Admin> _admins;
        private static IEnumerable<MatchDetail> _matches;
        private static IEnumerable<PlayerDetail> _players;

        public static ICollection<Admin> Admins
        {
            get
            {
                CheckForLoad();
                return _admins;
            }
        }

        public static IEnumerable<Match> AllMatches;
        public static IEnumerable<Player> AllPlayers;

        private static void CheckForLoad()
        {
            if (AllPlayers == null)
            {
                LoadAll();
            }
        }

        public static void LoadAll()
        {
            lock (Lock)
            {
                AllPlayers = PlayerData.Retrieve();
                AllMatches = MatchData.Retrieve();
                _admins = AdminData.Retrieve();

                _matches = AllMatches.Select(m => new MatchDetail(m)).ToList();
                LoadPlayers();
            }
        }

        private static void LoadPlayers()
        {
            _players =
                AllPlayers.Select(
                    player =>
                        new PlayerDetail(player)
                        {
                            //Matches =
                            //    _matches.Where(m => m.PlayerOneId == player.Id || m.PlayerTwoId == player.Id)
                            //        .Select(m => new PlayerMatch(player.Id, m))
                            //        .ToList()
                        }).ToList();
        }

        public static void LoadPlayersOnly()
        {
            lock (Lock)
            {
                AllPlayers = PlayerData.Retrieve();
                LoadPlayers();
            }
        }

        public static object Lock = new object();

        public static IEnumerable<MatchDetail> Matches
        {
            get
            {
                CheckForLoad();
                return _matches;
            }
        }

        public static IEnumerable<PlayerDetail> Players
        {
            get
            {
                CheckForLoad();
                return _players;
            }
        }
    }
}
