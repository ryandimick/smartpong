using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A rating that is associated with a user.
    /// 
    /// </summary>
    public class UserRating
    {
        /// <summary>
        /// 
        /// The unique identifier of the user rating.
        /// 
        /// </summary>
        [Key]
        public int UserRatingId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the associated user.
        /// 
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the rating type.
        /// 
        /// </summary>
        [Required]
        public int RatingTypeId { get; set; }

        /// <summary>
        /// 
        /// The serialized data containing applicable rating information.
        /// 
        /// </summary>
        [Required]
        public string RatingData { get; set; }

        /// <summary>
        /// 
        /// The user associated with the rating.
        /// 
        /// </summary>
        [ForeignKey("UserId")]
        public User User { get; set; }

        /// <summary>
        /// 
        /// The rating type associated with the rating.
        /// 
        /// </summary>
        [ForeignKey("RatingTypeId")]
        public RatingType RatingType { get; set; }
    }
}