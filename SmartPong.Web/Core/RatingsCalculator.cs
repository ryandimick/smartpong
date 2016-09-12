using SmartPong.Models;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace SmartPong.Core
{
    public class RatingsCalculator
    {
        private const double DefaultSkill = 25;

        private const double DefaultVariance = DefaultSkill / 3;

        public static IEnumerable<UserRating> GenerateNewUserRatings(int userId)
        {
            List<UserRating> newRatings = new List<UserRating>();

            var serializer = new JavaScriptSerializer();
            TrueskillRating trueskillRating = new TrueskillRating { Id = userId, Skill = DefaultSkill, Variance = DefaultVariance };
            var ratingData = serializer.Serialize(trueskillRating);

            UserRating singlesRating = new UserRating { UserId = userId, RatingTypeId = 3, RatingData = ratingData };
            newRatings.Add(singlesRating);
            
            UserRating doublesRating = new UserRating { UserId = userId, RatingTypeId = 4, RatingData = ratingData };
            newRatings.Add(doublesRating);

            return newRatings;
        }
    }
}