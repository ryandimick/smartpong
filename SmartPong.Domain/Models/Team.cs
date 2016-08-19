using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// An object formed by a collection of users playing together that maintains its own history and ratings.
    /// 
    /// </summary>
    public class Team
    {
        /// <summary>
        /// 
        /// Initiailizes a new instance of the Team object with default values.
        /// 
        /// </summary>
        public Team()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// 
        /// The unique identifier of the team.
        /// 
        /// </summary>
        [Key]
        public int TeamId { get; set; }

        /// <summary>
        /// 
        /// The timestamp for when the team was created.
        /// 
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 
        /// The timestamp for when the team was last active.
        /// 
        /// </summary>
        public DateTime? ActivityDate { get; set; }

        /// <summary>
        /// 
        /// The users that are make up the team.
        /// 
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// 
        /// The ratings associated with the team.
        /// 
        /// </summary>
        public ICollection<TeamRating> TeamRatings { get; set; }
        
        /// <summary>
        /// 
        /// The ratings updates from match history associated with the team.
        /// 
        /// </summary>
        public ICollection<MatchTeamRating> MatchTeamRatings { get; set; }  
    }
}
