using SmartPong.Models;
using System.Data.Entity;

namespace SmartPong.Core
{
    public class SmartPongContext : DbContext
    {
        public SmartPongContext(string connectionString) : base(connectionString)
        {

        }

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchParticipant> MatchParticipants { get; set; }

        public DbSet<MatchRating> MatchRatings { get; set; }

        public DbSet<MatchStatus> MatchStatuses { get; set; }

        public DbSet<MatchType> MatchTypes { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<RatingType> RatingTypes { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRating> UserRatings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().
            HasMany(t => t.Users).
            WithMany(u => u.Teams).
            Map(
                m =>
                {
                    m.MapLeftKey("TeamId");
                    m.MapRightKey("UserId");
                    m.ToTable("TeamUsers");
                });
        }
    }
}