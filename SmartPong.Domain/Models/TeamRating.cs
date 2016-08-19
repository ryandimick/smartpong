using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A rating that is assocated with a team.
    /// 
    /// </summary>
    public class TeamRating
    {
        /// <summary>
        /// 
        /// The unique identifier of the team rating.
        /// 
        /// </summary>
        [Key]
        public int TeamRatingId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the associated team.
        /// 
        /// </summary>
        [Required]
        public int TeamId { get; set; }

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
        /// The team associated with the rating.
        /// 
        /// </summary>
        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        /// <summary>
        /// 
        /// The rating type associated with the rating.
        /// 
        /// </summary>
        [ForeignKey("RatingTypeId")]
        public RatingType RatingType { get; set; }
    }
}
