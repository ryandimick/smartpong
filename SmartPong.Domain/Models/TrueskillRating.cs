namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A rating using the Trueskill system.
    /// 
    /// </summary>
    public class TrueskillRating
    {
        /// <summary>
        /// 
        /// The unique identifier of the rating.
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// The mean value of the rating.
        /// 
        /// </summary>
        public double Skill { get; set; }

        /// <summary>
        /// 
        /// The standard deviation of the rating.
        /// 
        /// </summary>
        public double Variance { get; set; }

        /// <summary>
        /// 
        /// The displayed "score" of the rating using the standard formula.
        /// 
        /// </summary>
        public double Rating => Skill - (3 * Variance);
    }
}
