using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    public class TeamRating
    {
        [Key]
        public int TeamRatingId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public int RatingTypeId { get; set; }

        [Required]
        public string RatingData { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        [ForeignKey("RatingTypeId")]
        public RatingType RatingType { get; set; }
    }
}