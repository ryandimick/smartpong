using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public int RatingTypeId { get; set; }

        [Required]
        public int ObjectId { get; set; }

        [Required]
        public string RatingData { get; set; }
        
        public virtual object RatingObject { get; set; }
    }
}