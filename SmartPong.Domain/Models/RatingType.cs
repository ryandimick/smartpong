using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A classification of a rating.
    /// 
    /// </summary>
    public class RatingType
    {
        /// <summary>
        /// 
        /// Initializes a new copy of rating type with default values.
        /// 
        /// </summary>
        public RatingType()
        {
            MatchTeamRatings = new List<MatchTeamRating>();
            MatchUserRatings = new List<MatchUserRating>();
            TeamRatings = new List<TeamRating>();
            UserRatings = new List<UserRating>();
        }

        /// <summary>
        /// 
        /// The unique identifier of the rating type.
        /// 
        /// </summary>
        [Key]
        public int RatingTypeId { get; set; }

        /// <summary>
        /// 
        /// The text describing the rating type.
        /// 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// The collection of match team rating changes associated with the rating type.
        /// 
        /// </summary>
        public ICollection<MatchTeamRating> MatchTeamRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of match user rating changes associated with the rating type.
        /// 
        /// </summary>
        public ICollection<MatchUserRating> MatchUserRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of team ratings associated with the rating type.
        /// 
        /// </summary>
        public ICollection<TeamRating> TeamRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of user ratings associated with the rating type.
        /// 
        /// </summary>
        public ICollection<UserRating> UserRatings { get; set; }
    }
}
