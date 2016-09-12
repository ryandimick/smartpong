namespace SmartPong.Models
{
    public class TrueskillRating 
    {
        public int Id { get; set; }

        public double Skill { get; set; }

        public double Variance { get; set; }

        public double Rating => Skill - (3 * Variance);
    }
}