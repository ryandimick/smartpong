using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    public class MatchScores
    {
        /// <summary>
        /// 
        /// The unique identifier of the match score entry.
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// The unique identifier of the match.
        /// 
        /// </summary>
        //[ForeignKey("MatchId")]
        [Required]
        public int MatchId { get; set; }

        /// <summary>
        /// 
        /// The score for the losing team.
        /// 
        /// </summary>
        //[Required]
        public int LosingTeamScore { get; set; }

        /// <summary>
        /// 
        /// The score for the winning team.
        /// 
        /// </summary>
        //[Required]
        public int WinningTeamScore { get; set; }

        /// <summary>
        /// 
        /// Boolean flag to know if match has been confirmed.
        /// 
        /// </summary>
        //[Required]
        public MatchStatus Status { get; set; }
    }
}
