using System.Collections.Generic;

namespace SmaPong.Models
{
    public class ChallengeListViewModel
    {
        public IEnumerable<ChallengeViewModel> PlayerChallenges { get; set; } 
        public IEnumerable<ChallengeViewModel> UpcomingChallenges { get; set; }
    }
}