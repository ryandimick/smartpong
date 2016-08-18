using System.Collections.Generic;

namespace SmaPong.Models
{
    public class MatchOutcome
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public static IEnumerable<MatchOutcome> PossibleOutcomes = new List<MatchOutcome>
        {
            new MatchOutcome {Id = 1, Description = "Win"},
            new MatchOutcome {Id = 2, Description = "Loss"}
        };
    }
}