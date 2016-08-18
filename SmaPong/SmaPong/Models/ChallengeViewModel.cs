using System;

namespace SmaPong.Models
{
    public class ChallengeViewModel
    {
        public int ChallengeeId { get; set; }
        public string ChallengeeName { get; set; }
        public string ChallengeResponse { get; set; }
        public int ChallengerId { get; set; }
        public string ChallengerName { get; set; }
        public ChallengeStatus ChallengeStatus { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}