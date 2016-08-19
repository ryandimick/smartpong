using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A rating update for a user after a match.
    /// 
    /// </summary>
    public class MatchUserRating
    {
        /// <summary>
        /// 
        /// The unique identifier of the match user rating.
        /// 
        /// </summary>
        [Key]
        public int MatchUserRatingId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the match.
        /// 
        /// </summary>
        [Required]
        public int MatchId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the user.
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
        public string RatingData { get; set; }

        /// <summary>
        /// 
        /// The match associated with the match user rating.
        /// 
        /// </summary>
        [ForeignKey("MatchId")]
        public Match Match { get; set; }

        /// <summary>
        /// 
        /// The user associated with the match user rating.
        /// 
        /// </summary>
        [ForeignKey("UserId")]
        public User User { get; set; }

        /// <summary>
        /// 
        /// The rating type associated with the match user rating.
        /// 
        /// </summary>
        [ForeignKey("RatingTypeId")]
        public RatingType RatingType { get; set; }
    }
}
