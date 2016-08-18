using System;
using System.Collections.Generic;
using System.Linq;
using SmaPong.Business;

namespace SmaPong.Models
{
    public class PlayerDetail
    {
        private readonly Player _player;

        public bool Active
        {
            get { return _player.Active; }
        }

        public DateTime ActivityDate
        {
            get { return _player.ActivityDate; }
        }

        public string Confidence
        {
            get { return string.Format("{0:0.0000}", _player.Sigma); }
        }

        public string Email
        {
            get { return _player.Email; }
        }

        public string FirstName
        {
            get { return _player.FirstName; }
        }

        public string FullName { get; private set; }

        public int Id
        {
            get { return _player.Id; }
        }

        public List<PlayerMatch> Matches { get; set; }

        public double Mu
        {
            get { return _player.Mu; }
        }

        public string Name { get; private set; }

        public string Nickname
        {
            get { return _player.Nickname; }
        }

        public bool Notifications
        {
            get { return _player.Notifications; }
        }

        public Record OverallRecord { get; set; }

        public string Pending { get; private set; }

        public PlayerDetail(Player player)
        {
            _player = player;
            Name = string.Format("{0} {1}", player.FirstName, player.Surname);
            FullName = string.IsNullOrWhiteSpace(player.Nickname)
                ? string.Format("{0} {1}", player.FirstName, player.Surname)
                : string.Format("{0} \"{1}\" {2}", player.FirstName, player.Nickname, player.Surname);
            
            var matches = new List<PlayerMatch>();
            Records = new List<PlayerRecord>();
            foreach (var match in Global.Matches.Where(m => m.PlayerOneId == player.Id || m.PlayerTwoId == player.Id))
            {
                var playerMatch = new PlayerMatch(player.Id, match);
                matches.Add(playerMatch);
                var r = Records.SingleOrDefault(record => record.OpponentId == playerMatch.OpponentId);
                if (r != null)
                {
                    if (playerMatch.WinningPlayerId == player.Id)
                    {
                        r.Wins++;
                    }
                    else
                    {
                        r.Losses++;   
                    }
                    r.Games++;
                    r.CalculatePercentage();
                }
                else
                {
                    r = playerMatch.WinningPlayerId == player.Id
                        ? new PlayerRecord(playerMatch.OpponentId, playerMatch.Opponent, 1, 0)
                        : new PlayerRecord(playerMatch.OpponentId, playerMatch.Opponent, 0, 1);
                    Records.Add(r);
                }
            }
            Matches = matches.OrderByDescending(m => m.MatchDate).ToList();
            OverallRecord = new Record(Matches.Count(m => m.WinningPlayerId == player.Id),
                Matches.Count(m => m.WinningPlayerId != player.Id));
            Records = Records.OrderByDescending(r => r.Games).ToList();
            var lastMatch = Matches.FirstOrDefault(match => match.MatchDate == Matches.Max(m => m.MatchDate));
            Pending = (lastMatch != null && lastMatch.PostDate == null)
                ? string.Format("{1}{0:0.0000}", lastMatch.Mu - player.Mu,
                    (lastMatch.Mu - player.Mu) > 0 ? "+" : string.Empty)
                : string.Empty;
        }

        public string Rating
        {
            get { return string.Format("{0:0.0000}", _player.Mu); }
        }

        public ICollection<PlayerRecord> Records { get; private set; } 

        public double Sigma
        {
            get { return _player.Sigma; }
        }

        public string Surname
        {
            get { return _player.Surname; }
        }

        public string Username
        {
            get { return _player.Username; }
        }
    }
}