using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    public class RatingType
    {
        [Key]
        public int RatingTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        
        public ICollection<MatchRating> MatchRatings { get; set; }

        public ICollection<TeamRating> TeamRatings { get; set; }

        public ICollection<UserRating> UserRatings { get; set; }

        public RatingType()
        {
            MatchRatings = new List<MatchRating>();
            TeamRatings = new List<TeamRating>();
            UserRatings = new List<UserRating>();
        }
    }
}