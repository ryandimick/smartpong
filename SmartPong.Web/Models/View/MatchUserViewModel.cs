namespace SmartPong.Models.View
{
    public class MatchUserViewModel
    {
        public int MatchId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string RatingChange { get; set; }

        public MatchUserViewModel(int matchId, int userId, string userName, string ratingsChange)
        {
            MatchId = matchId;
            UserId = userId;
            UserName = userName;
            RatingChange = ratingsChange;
        }
    }
}