using System;
using System.Web.Mvc;

namespace SmaPong.Models
{
    public class Challenge
    {
        public int ChallengedId { get; set; }
        public string ChallengerName { get; set; }
        public string Message { get; set; }
        public SelectList Opponents { get; set; }
        public DateTime Timestamp { get; set; }
    }
}