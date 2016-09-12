using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    public class MatchType
    {
        [Key]
        public int MatchTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}