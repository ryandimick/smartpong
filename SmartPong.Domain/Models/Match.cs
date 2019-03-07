using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A representation of a ping pong game.
    /// 
    /// </summary>
    public class Match
    {
        public Match()
        {
            
        }

        public Match(MatchType.Type type, DateTime? matchDate = null)
        {
            MatchTypeId = (int) type;
            MatchDate = matchDate;
            MatchParticipants = new List<MatchParticipant>();
        }

        /// <summary>
        /// 
        /// The unique identifier of the match.
        /// 
        /// </summary>
        [Key]
        public int MatchId { get; set; }

        /// <summary>
        /// 
        /// The timestamp when the match was submitted.
        /// 
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 
        /// The match type of the match.
        /// 
        /// </summary>
        [Required]
        public int MatchTypeId { get; set; }

        /// <summary>
        /// 
        /// The status code of the match.
        /// 
        /// </summary>
        //[ForeignKey("MatchStatus")]
        public MatchStatus Status { get; set; }

        /// <summary>
        /// 
        /// The winning team number.  Null if has not occurred yet.
        /// 
        /// </summary>
        public int? WinningTeam { get; set; }

        /// <summary>
        /// 
        /// The timestamp when the match occurred.  Null if has not occurred yet.
        /// 
        /// </summary>
        public DateTime? MatchDate { get; set; }

        /// <summary>
        /// 
        /// The timestamp when the match was confirmed as valid.  Null if not confirmed.
        /// 
        /// </summary>
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the user that confirmed the match.  Null if not confirmed.
        /// 
        /// </summary>
        public int? ConfirmUser { get; set; }

        /// <summary>
        /// 
        /// The type of the match.
        /// 
        /// </summary>
        [ForeignKey("MatchTypeId")]
        public MatchType MatchType { get; set; }

        /// <summary>
        /// 
        /// Object to input users game outcome.
        /// 
        /// </summary>
        //[ForeignKey("MatchId")]
        //public MatchScores MatchScores { get; set; }

        /// <summary>
        /// 
        /// The status of the match.
        /// 
        /// </summary>
        //[ForeignKey("Status")]
        //public MatchStatus MatchStatus { get; set; }

        /// <summary>
        /// 
        /// The collection of users that took part in the match.
        /// 
        /// </summary>
        public ICollection<MatchParticipant> MatchParticipants { get; set; }

        /// <summary>
        /// 
        /// The collection of user rating updates associated with the match.
        /// 
        /// </summary>
        public ICollection<MatchUserRating> MatchUserRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of team rating updates associated with the match.
        /// 
        /// </summary>
        public ICollection<MatchTeamRating> MatchTeamRatings { get; set; }
    }
}
