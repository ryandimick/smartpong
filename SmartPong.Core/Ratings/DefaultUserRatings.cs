using SmartPong.Models;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace SmartPong.Ratings
{
    internal class DefaultUserRatings
    {
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

        internal static List<UserRating> Generate()
        {
            var defaultUserRatings = new List<UserRating>();
            foreach (UserRatingType type in Enum.GetValues(typeof(UserRatingType)))
            {
                var defaultRating = GetDefaultRating(type);
                var defaultUserRating = new UserRating
                {
                    RatingTypeId = (int) type,
                    RatingData = serializer.Serialize(defaultRating)
                };

                defaultUserRatings.Add(defaultUserRating);
            }
            return defaultUserRatings;
        }

        private static object GetDefaultRating(UserRatingType type)
        {
            switch (type)
            {
                case UserRatingType.TrueskillSingles:
                case UserRatingType.TrueskillDoubles:
                    return TrueskillRating.Default;
                default:
                    throw new Exception($"UserRatingType {type} not implemented!");
            }
        }
    }
}
