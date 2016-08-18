namespace SmaPong.Models
{
	public class PlayerRecord : Record
	{
        public int OpponentId { get; private set; }

        public string OpponentName { get; private set; }

	    public PlayerRecord(int playerId, string playerName, int wins, int losses) : base(wins, losses)
	    {
	        OpponentId = playerId;
	        OpponentName = playerName;
	    }
	}
}