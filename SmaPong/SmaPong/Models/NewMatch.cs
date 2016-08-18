using System;
using System.Web.Mvc;

namespace SmaPong.Models
{
    public class NewMatch
    {
        private DateTime _matchDate = DateTime.Now;

        public DateTime MatchDate
        {
            get { return _matchDate; }
            set { _matchDate = value; }
        }

        public int PlayerOneId { get; set; }
        public int PlayerTwoId { get; set; }
        public int PlayerOnePlace { get; set; }
        public int Placement { get; set; }
        public SelectList Opponents { get; set; }
        public SelectList PossibleOutcomes { get; set; }
    }
}