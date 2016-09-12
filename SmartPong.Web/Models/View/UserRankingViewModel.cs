using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace SmartPong.Models.View
{
    public class UserRankingViewModel
    {
        public int Rank { get; set; }

        public string DisplayName { get; set; }

        public int UserId { get; set; }

        public double Rating { get; set; }

        public double Skill { get; set; }

        public double Variance { get; set; }

        public DateTime? ActivityDate { get; set; }

        public static IEnumerable<UserRankingViewModel> Generate(IEnumerable<UserRating> userRatings)
        {
            List<UserRankingViewModel> viewModels = new List<UserRankingViewModel>();
            var serializer = new JavaScriptSerializer();

            foreach (var userRating in userRatings)
            {
                TrueskillRating rating = serializer.Deserialize<TrueskillRating>(userRating.RatingData);
                UserRankingViewModel viewModel = new UserRankingViewModel
                {
                    UserId = userRating.UserId,
                    DisplayName = userRating.User.DisplayName,
                    Rating = rating.Rating,
                    Skill = rating.Skill,
                    Variance = rating.Variance,
                    ActivityDate = userRating.User.ActivityDate
                };
                viewModels.Add(viewModel);
            }

            viewModels = viewModels.OrderByDescending(vm => vm.Rating).ToList();
            for (var i = 0; i < viewModels.Count; i++)
            {
                viewModels[i].Rank = i + 1;
            }

            return viewModels;
        }
    }
}