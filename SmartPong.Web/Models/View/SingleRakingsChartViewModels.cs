using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace SmartPong.Models.View
{
    public class SingleRakingsChartViewModels
    {
        public double OldSkill { get; set; }

        public double OldVariance { get; set; }

        public double NewSkill { get; set; }

        public double NewVariance { get; set; }

        public string DisplayName { get; set; }

        public int UserId { get; set; }

        public int MatchId { get; set; }
        public string MatchDate { get; set; }

        public int RatingTypeId { get; set; }
    }

    public static class RankingsVisualExtensions
    {
        public static IEnumerable<SingleRakingsChartViewModels> ToChartFormat(this IEnumerable<MatchUserRating> userRatings, IEnumerable<Match> matches)
        {
            List<SingleRakingsChartViewModels> viewModels = new List<SingleRakingsChartViewModels>();
            var serializer = new JavaScriptSerializer();

            foreach (var userRating in userRatings)
            {
                var ratingData = serializer.Deserialize<TrueskillRatingChange>(userRating.RatingData);

                SingleRakingsChartViewModels viewModel = new SingleRakingsChartViewModels
                {
                    UserId = userRating.UserId,
                    DisplayName = userRating.User.DisplayName,
                    OldVariance = ratingData.OldVariance,
                    NewVariance = ratingData.NewVariance,
                    OldSkill = ratingData.OldSkill,
                    NewSkill = ratingData.NewSkill,
                    MatchId = userRating.MatchId,
                    RatingTypeId = userRating.RatingTypeId
                };

                viewModels.Add(viewModel);
            }

            foreach (var view in viewModels)
            {
                view.MatchDate =  matches.First(f => f.MatchId == view.MatchId).MatchDate.ToString();
            }

            return viewModels;
        }
    }
}