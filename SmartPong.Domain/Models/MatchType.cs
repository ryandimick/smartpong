using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A classification of a type of match.
    /// 
    /// </summary>
    public class MatchType
    {
        /// <summary>
        /// 
        /// The unique identeifier of the match type.
        /// 
        /// </summary>
        [Key]
        public int MatchTypeId { get; set; }

        /// <summary>
        /// 
        /// The text describing the match type.
        /// 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// The collection of matches that are of the type.
        /// 
        /// </summary>
        public ICollection<Match> Matches { get; set; }
    }
}
