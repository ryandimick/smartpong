using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    public class MatchUserRating
    {
        [Key]
        public int MatchUserRatingId { get; set; }

        [Required]
        public int MatchId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RatingTypeId { get; set; }

        public string RatingData { get; set; }

        [ForeignKey("MatchId")]
        public Match Match { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RatingTypeId")]
        public RatingType RatingType { get; set; }
    }
}