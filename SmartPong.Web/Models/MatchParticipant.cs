using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPong.Models
{
    public class MatchParticipant
    {
        [Key]
        public int MatchParticipantId { get; set; }

        [Required]
        public int MatchId { get; set; }

        [Required]
        public int MatchTeamId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("MatchId")]
        public Match Match { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}