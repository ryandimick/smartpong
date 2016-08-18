using System;

namespace SmaPong.Models
{
    public class Match
    {
        public DateTime? ConfirmationDate { get; set; }
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public int PlayerOneId { get; set; }
        public double PlayerOneOldMu { get; set; }
        public double PlayerOneNewMu { get; set; }
        public double PlayerOneOldSigma { get; set; }
        public double PlayerOneNewSigma { get; set; }
        public int PlayerTwoId { get; set; }
        public double PlayerTwoOldMu { get; set; }
        public double PlayerTwoNewMu { get; set; }
        public double PlayerTwoOldSigma { get; set; }
        public double PlayerTwoNewSigma { get; set; }
        public DateTime? PostDate { get; set; }
        public int WinningPlayerId { get; set; }
    }
}