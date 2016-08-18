using System;
using System.Linq;
using SmaPong.Business;

namespace SmaPong.Models
{
    /// <summary>
    /// An immutable wrapper class around a Match object with calculated fields
    /// </summary>
    public class MatchDetail
    {
        private readonly Match _match;

        public DateTime? ConfirmationDate
        {
            get { return _match.ConfirmationDate; }
        }

        public int Id
        {
            get { return _match.Id; }
        }

        public DateTime MatchDate
        {
            get { return _match.MatchDate; }
        }

        public MatchDetail(Match match)
        {
            _match = match;
            var playerOne = Global.AllPlayers.Single(p => p.Id == match.PlayerOneId);
            PlayerOneName = string.Format("{0} {1}", playerOne.FirstName, playerOne.Surname);
            var delta = match.PlayerOneNewMu - match.PlayerOneOldMu;
            PlayerOneMuDelta = delta;
            PlayerOneSigmaDelta = match.PlayerOneNewSigma - match.PlayerOneOldSigma;
            var playerTwo = Global.AllPlayers.Single(p => p.Id == match.PlayerTwoId);
            PlayerTwoName = string.Format("{0} {1}", playerTwo.FirstName, playerTwo.Surname);
            PlayerTwoMuDelta = match.PlayerTwoNewMu - match.PlayerTwoOldMu;
            PlayerTwoSigmaDelta = match.PlayerTwoNewSigma - match.PlayerTwoOldSigma;
            if (match.ConfirmationDate == null)
            {
                Status = MatchStatus.PendingConfirmation;
            }
            else if (match.PostDate == null)
            {
                Status = MatchStatus.PendingPosting;
            }
            else
            {
                Status = MatchStatus.Posted;
            }
        }

        public int PlayerOneId
        {
            get { return _match.PlayerOneId; }
        }

        public double PlayerOneMuDelta { get; private set; }

        public string PlayerOneName { get; private set; }

        public double PlayerOneNewMu
        {
            get { return _match.PlayerOneNewMu; }
        }

        public double PlayerOneNewSigma
        {
            get { return _match.PlayerOneNewSigma; }
        }

        public double PlayerOneOldMu
        {
            get { return _match.PlayerOneOldMu; }
        }

        public double PlayerOneOldSigma
        {
            get { return _match.PlayerOneOldSigma; }
        }

        public double PlayerOneSigmaDelta { get; private set; }

        public int PlayerTwoId
        {
            get { return _match.PlayerTwoId; }
        }

        public double PlayerTwoMuDelta { get; private set; }

        public string PlayerTwoName { get; private set; }

        public double PlayerTwoNewMu
        {
            get { return _match.PlayerTwoNewMu; }
        }

        public double PlayerTwoNewSigma
        {
            get { return _match.PlayerTwoNewSigma; }
        }

        public double PlayerTwoOldMu
        {
            get { return _match.PlayerTwoOldMu; }
        }

        public double PlayerTwoOldSigma
        {
            get { return _match.PlayerTwoOldSigma; }
        }

        public double PlayerTwoSigmaDelta { get; private set; }

        public DateTime? PostDate
        {
            get { return _match.PostDate; }
        }

        public MatchStatus Status { get; private set; }

        public int WinningPlayerId
        {
            get { return _match.WinningPlayerId; }
        }
    }
}