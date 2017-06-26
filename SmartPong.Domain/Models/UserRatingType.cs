namespace SmartPong.Models
{
    public enum UserRatingType
    {
        /// <summary>
        /// 
        /// A user rating used to hold an invididual's singles rating calculated via the Trueskill formula.
        /// 
        /// </summary>
        TrueskillSingles = 1,

        /// <summary>
        /// 
        /// A user rating used to hold an individual's doubles rating calculated via the Trueskill formula.
        /// 
        /// </summary>
        TrueskillDoubles = 2
    }
}
