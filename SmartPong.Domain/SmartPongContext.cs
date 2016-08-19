using System.Data.Entity;
using SmartPong.Models;

namespace SmartPong
{
    /// <summary>
    /// 
    /// The database context used for running SmartPong.
    /// 
    /// </summary>
    public class SmartPongContext : DbContext
    {
        /// <summary>
        /// 
        /// Initializes a new instance of the database context that utilizes the provided connection string.
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SmartPongContext(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// 
        /// The collection of matches in the database context.
        /// 
        /// </summary>
        public DbSet<Match> Matches { get; set; }

        /// <summary>
        /// 
        /// The collection of match participants in the database context.
        /// 
        /// </summary>
        public DbSet<MatchParticipant> MatchParticipants { get; set; }

        /// <summary>
        /// 
        /// The collection of match statuses in the database context.
        /// 
        /// </summary>
        public DbSet<MatchStatus> MatchStatuses { get; set; }

        /// <summary>
        /// 
        /// The collection of match team ratings in the database context.
        /// 
        /// </summary>
        public DbSet<MatchTeamRating> MatchTeamRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of match types in the database context.
        /// 
        /// </summary>
        public DbSet<MatchType> MatchTypes { get; set; }

        /// <summary>
        /// 
        /// The collection of match user ratings in the database context.
        /// 
        /// </summary>
        public DbSet<MatchUserRating> MatchUserRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of rating types in the database context.
        /// 
        /// </summary>
        public DbSet<RatingType> RatingTypes { get; set; }

        /// <summary>
        /// 
        /// The collection of team ratings in the database context.
        /// 
        /// </summary>
        public DbSet<TeamRating> TeamRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of teams in the database context.
        /// 
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// 
        /// The collection of user ratings in the database context.
        /// 
        /// </summary>
        public DbSet<UserRating> UserRatings { get; set; }

        /// <summary>
        /// 
        /// The collection of users in the database context.
        /// 
        /// </summary>
        public DbSet<User> Users { get; set; }

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
