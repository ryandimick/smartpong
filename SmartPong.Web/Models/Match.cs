using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int MatchTypeId { get; set; }
        
        public int? WinningTeam { get; set; }

        public DateTime? MatchDate { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public int? ConfirmUser { get; set; }

        public DateTime? PostDate { get; set; }

        public int Status { get; set; }

        [ForeignKey("MatchTypeId")]
        public MatchType MatchType { get; set; }

        [ForeignKey("Status")]
        public MatchStatus MatchStatus { get; set; }

        public ICollection<MatchParticipant> MatchParticipants { get; set; }

        public ICollection<MatchUserRating> MatchUserRatings { get; set; }
    }
}