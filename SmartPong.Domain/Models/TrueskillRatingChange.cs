namespace SmartPong.Models
{
    /// <summary>
    /// 
    /// A representation of an entity's TrueSkill Rating delta after a match outcome.
    /// 
    /// </summary>
    public class TrueskillRatingChange
    {
        /// <summary>
        /// 
        /// The Skill value before the match.
        /// 
        /// </summary>
        public double OldSkill { get; set; }

        /// <summary>
        /// 
        /// The Variance value before the match.
        /// 
        /// </summary>
        public double OldVariance { get; set; }

        /// <summary>
        /// 
        /// The Skill value after the match's outcome.
        /// 
        /// </summary>
        public double NewSkill { get; set; }

        /// <summary>
        /// 
        /// The Variance value after the match's outcome.
        /// 
        /// </summary>
        public double NewVariance { get; set; }
    }
}
