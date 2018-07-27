using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmartPong.Models.View
{
    public class MatchCreateViewModel
    {
        public DateTime MatchTime { get; set; }

        public MatchType.Type MatchType { get; set; }

        public IEnumerable<SelectListItem> DoubleOpponents { get; set; }

        public string SelectedOpponents { get; set; }

        public string Teammate { get; set; }

        public IEnumerable<SelectListItem> SingleOpponent { get; set; }

        public int YourScore { get; set; }

        public int OpponentScore { get; set; }
    }
}