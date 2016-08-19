using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{   
    /// <summary>
    /// 
    /// A mapping between a match, a match team, and a user.
    /// 
    /// </summary>
    public class MatchParticipant
    {
        /// <summary>
        /// 
        /// The unique identifier of the match participant.
        /// 
        /// </summary>
        [Key]
        public int MatchParticipantId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the match.
        /// 
        /// </summary>
        [Required]
        public int MatchId { get; set; }

        /// <summary>
        /// 
        /// The match team number the participant was on.
        /// 
        /// </summary>
        [Required]
        public int MatchTeamId { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the user that participated.
        /// 
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// The match associated with the match participant record.
        /// 
        /// </summary>
        [ForeignKey("MatchId")]
        public Match Match { get; set; }

        /// <summary>
        /// 
        /// The user associated with the match participant record.
        /// 
        /// </summary>
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
