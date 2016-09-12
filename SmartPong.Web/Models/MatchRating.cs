using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    public class MatchRating
    {
        [Key]
        public int MatchRatingId { get; set; }

        [Required]
        public int MatchId { get; set; }

        [Required]
        public int RatingTypeId { get; set; }

        [Required]
        public string RatingData { get; set; }

        [ForeignKey("MatchId")]
        public Match Match { get; set; }

        [ForeignKey("RatingTypeId")]
        public RatingType RatingType { get; set; }
    }
}