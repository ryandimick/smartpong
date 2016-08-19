using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A rating update for a team after a match.
    /// 
    /// </summary>
    public class MatchTeamRating
    {
        /// <summary>
        /// 
        /// The unique identifier of the match team rating.
        /// 
        /// </summary>
        [Key]
        public int MatchTeamRatingId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the match.
        /// 
        /// </summary>
        [Required]
        public int MatchId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the team.
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
        public string RatingData { get; set; }

        /// <summary>
        /// 
        /// The match associated with the match team rating.
        /// 
        /// </summary>
        [ForeignKey("MatchId")]
        public Match Match { get; set; }

        /// <summary>
        /// 
        /// The team associated with the match team rating.
        /// 
        /// </summary>
        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        /// <summary>
        /// 
        /// The rating type associated with the match team rating.
        /// 
        /// </summary>
        [ForeignKey("RatingTypeId")]
        public RatingType RatingType { get; set; }
    }
}
