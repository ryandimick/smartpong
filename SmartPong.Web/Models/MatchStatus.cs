using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    public class MatchStatus
    {
        [Key]
        public int MatchStatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}