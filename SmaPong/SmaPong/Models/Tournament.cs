using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmaPong.Models
{
    public class Tournament
    {
        public IList<Player> Administrators { get; set; } 
        public string Description { get; set; }
        public int Id { get; set; }
        public IList<Match> Matches { get; set; } 
        public IList<Player> Participants { get; set; } 
        public int Players { get; set; }
        public int ProtectedPlayers { get; set; }
        public DateTime StartDate { get; set; }
    }
}