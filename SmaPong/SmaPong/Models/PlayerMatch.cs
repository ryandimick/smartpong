using System;

namespace SmaPong.Models
{
    public class PlayerMatch
    {
        private readonly MatchDetail _match;

        public string Confidence { get; private set; }

        public DateTime? ConfirmationDate
        {
            get { return _match.ConfirmationDate; }
        }

        public int Id { get; set; }

        public DateTime MatchDate { get; set; }

        public double Mu { get; private set; }

        public string Opponent { get; set; }

        public int OpponentId { get; private set; }

        public PlayerMatch()
        {

        }

        public PlayerMatch(int playerId, MatchDetail match)
        {
            _match = match;
            Id = _match.Id;
            MatchDate = _match.MatchDate;
            Status = _match.Status;
            if (match.PlayerOneId == playerId)
            {
                Opponent = match.PlayerTwoName;
                OpponentId = match.PlayerTwoId;
                Position = 1;
                Mu = match.PlayerOneNewMu;
                Sigma = match.PlayerOneNewSigma;
                Rating = string.Format("{0:0.0000} ({1:0.0000})", match.PlayerOneNewMu, match.PlayerOneMuDelta);
                Confidence = string.Format("{0:0.0000} ({1:0.0000})", match.PlayerOneNewSigma, match.PlayerOneSigmaDelta);
            }
            else if (match.PlayerTwoId == playerId)
            {
                Opponent = match.PlayerOneName;
                OpponentId = match.PlayerOneId;
                Position = 2;
                Mu = match.PlayerTwoNewMu;
                Sigma = match.PlayerTwoNewSigma;
                Rating = string.Format("{0:0.0000} ({1:0.0000})", match.PlayerTwoNewMu, match.PlayerTwoMuDelta);
                Confidence = string.Format("{0:0.0000} ({1:0.0000})", match.PlayerTwoNewSigma, match.PlayerTwoSigmaDelta);
            }
            Result = match.WinningPlayerId == playerId ? "Win" : "Loss";
        }

        public int Position { get; private set; }

        public DateTime? PostDate
        {
            get { return _match.PostDate; }
        }

        public string Rating { get; private set; }

        public string Result { get; set; }

        public double Sigma { get; private set; }

        public MatchStatus Status { get; set; }

        public int WinningPlayerId
        {
            get { return _match.WinningPlayerId; }
        }
    }
}