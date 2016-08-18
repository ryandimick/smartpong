namespace SmaPong.Models
{
    public class OpponentRecord
    {
        private readonly Record _record;

        public int Games
        {
            get { return _record.Games; }
        }

        public int Losses
        {
            get { return _record.Losses; }
        }

        public string Opponent { get; set; }

        public OpponentRecord(Record record)
        {
            _record = record;
        }

        public string Percentage
        {
            get { return _record.Percentage; }
        }

        public int Wins
        {
            get { return _record.Wins; }
        }
    }
}