using System;

namespace SmaPong.Models
{
    public class Record
    {
        public void CalculatePercentage()
        {
            Percentage = string.Format("{0:0.0000}", Convert.ToDouble(Wins) / Games);
        }

        public int Games { get; set; }
        public int Losses { get; set; }
        public string Percentage { get; set; }

        public Record(int wins, int losses)
        {
            Games = wins + losses;
            Wins = wins;
            Losses = losses;
            Percentage = string.Format("{0:0.0000}", Convert.ToDouble(wins) / (wins + losses));
        }

        public int Wins { get; set; }
    }
}