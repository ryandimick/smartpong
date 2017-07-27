using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moserware.Skills;
using NUnit.Framework;
using SmartPong;
using SmartPong.Models;
using Assert = NUnit.Framework.Assert;

namespace Smartpong.Core.Tests.UserController_Test
{
    [TestClass]
    public class TestingUserController
    {

        public ISmartPongRepository GetRepository()
        {
            ISmartPongRepository repository = RepositoryManager.Create("server = MLETONA1; database = SmartPong; User Id = sa; Password = opconxps;");
            return repository;
        }

        [Test]
        public void When_Retrieving_Matches()
        {
            var listOfMatches = GetRepository().RetrieveMatches();

            NUnit.Framework.CollectionAssert.AllItemsAreNotNull(listOfMatches);
        }

        [Test]
        public void When_Retrieving_Settings()
        {
            var listOfSettings = GetRepository().RetrieveSettings();

            NUnit.Framework.CollectionAssert.AllItemsAreNotNull(listOfSettings);
        }

        [Test]
        public void When_Retrieving_UserRatings()
        {
            var userRatings = GetRepository().RetrieveUserRatings(UserRatingType.TrueskillSingles);
            
            NUnit.Framework.CollectionAssert.IsNotEmpty(userRatings);
        }

        [Test]
        public void When_Deleting_Match()
        {
            var matchToBeDeleted = GetRepository().RetrieveMatches().Last();
            GetRepository().DeleteMatch(matchToBeDeleted.MatchId);

            Assert.That(matchToBeDeleted.MatchId, Is.Not.EqualTo(GetRepository().RetrieveMatches().Last().MatchId));
        }

        [Test]
        public void When_Creating_New_Player()
        {

            var createUser = GetRepository().CreateUser("TesUser" + DateTime.Now.Hour+DateTime.Now.Millisecond, "TestGivenName", "TestSurName",
                            "testemail@email.com", "TestingBot");

            var singles = createUser.UserRatings.First(f => f.RatingTypeId == 1);
            var doubles = createUser.UserRatings.First(f => f.RatingTypeId == 2);
            var desInfo = new JavaScriptSerializer();
            var singleInfo = desInfo.Deserialize<TrueskillRating>(singles.RatingData);
            var doublesInfo = desInfo.Deserialize<TrueskillRating>(doubles.RatingData);

            Assert.That(singleInfo.Skill, Is.EqualTo(GameInfo.DefaultGameInfo.DefaultRating.Mean));
            Assert.That(singleInfo.Variance, Is.EqualTo(GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation));
            Assert.That(doublesInfo.Skill, Is.EqualTo(GameInfo.DefaultGameInfo.DefaultRating.Mean));
            Assert.That(doublesInfo.Variance, Is.EqualTo(GameInfo.DefaultGameInfo.DefaultRating.StandardDeviation));
        }

        [Test]
        public void When_Updating_A_User()
        {
            var user = new User
            {
                Username = "UserName",
                GivenName = "GivenName",
                Surname = "SurName",
                Nickname = "UpdatedNickName",
                Email = "email@email.com",
                Notifications = false,
                Enabled = true,
                Admin = false,
                CreateDate = DateTime.Now,
                UserId = 1019
            };

            var updatedUser = GetRepository().UpdateUser(user);
            Assert.That(Trim(updatedUser.CreateDate, TimeSpan.TicksPerHour), Is.EqualTo(Trim(DateTime.Now, TimeSpan.TicksPerHour)));
        }

        [Test]
        public void When_Updating_Setting()
        {
            var setting = new Setting(){KeyName = "TestName", KeyValue = "TestValue"+DateTime.Now.Second};
            var updatedSetting = GetRepository().UpdateSetting(setting);
            Assert.That(updatedSetting.KeyValue, Is.EqualTo(setting.KeyValue));
        }

        static DateTime Trim(DateTime date, long roundTicks)
        {
            return new DateTime(date.Ticks - date.Ticks % roundTicks);
        }

        [TestMethod]
        public void When_UserName_DoesExists()
        {
            try
            {
                var repeatUserName = GetRepository().CreateUser("UserName", "GivenName", "SurName",
                    "email@email.com", "UpdatedNickName");
            }
            catch (Exception e)
            {
                Assert.IsNotEmpty(e.Message, "User Already Created!");
            }
        }

        [Test]
        public void When_User_Exists_Try_Catch()
        {
            try
            {
                var repeatUserName = GetRepository().CreateUser("UserName", "GivenName", "SurName",
                    "email@email.com", "UpdatedNickName");
            }
            catch (Exception)
            {
                Throws.TypeOf<Exception>().With.Message.EqualTo("User Already Created!");
                Assert.AreEqual(GetRepository().RetrieveUsers(u => u.Username.Equals("UserName")).Count(), 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void When_User_Exists()
        {
            var repeatUserName = GetRepository().CreateUser("UserName", "GivenName", "SurName",
                "email@email.com", "UpdatedNickName");
        }

        [Test]
        public void When_Creating_Match()
        {
            var scorePlayerOne = 21;
            var scorePlayerTwo = 15;
            var submitter = GetRepository().RetrieveUser(11);
            var opponent = GetRepository().RetrieveUser(9);

            var match = new Match(MatchType.Type.Singles, DateTime.Now);
            match.AddTeam(1, new List<User> { submitter });
            match.AddTeam(2, new List<User> { opponent });
            match.SetOutcome(scorePlayerOne > scorePlayerTwo ? 1 : 2);
            var submittedMatch = GetRepository().CreateMatch(match);

            Assert.That(submittedMatch.WinningTeam.Value, Is.EqualTo(1));
        }


        [Test]
        public void When_Updating_User_Rating()
        {
            var submitterRating = GetRepository().RetrieveUserRatings(UserRatingType.TrueskillSingles).First(u => u.UserId == 9);
            var opponentRating = GetRepository().RetrieveUserRatings(UserRatingType.TrueskillSingles).First(u => u.UserId == 11);
            var subAndOppRatings = new JavaScriptSerializer();
            var subStartRating = subAndOppRatings.Deserialize<TrueskillRating>(submitterRating.RatingData);
            var oppStartRating = subAndOppRatings.Deserialize<TrueskillRating>(opponentRating.RatingData);

            var scorePlayerOne = 21;
            var scorePlayerTwo = 15;
            var submitter = GetRepository().RetrieveUser(9);
            var opponent = GetRepository().RetrieveUser(11);


            var match = new Match(MatchType.Type.Singles, DateTime.Now);
            match.AddTeam(1, new List<User> { submitter });
            match.AddTeam(2, new List<User> { opponent });
            match.SetOutcome(scorePlayerOne > scorePlayerTwo ? 2 : 1);

            var submitMatch = GetRepository().CreateMatch(match);
            var desInfo = new JavaScriptSerializer();
            var ratingDataSubmitter = desInfo.Deserialize<TrueskillRatingChange>(submitMatch.MatchUserRatings.First(f => f.UserId == 9).RatingData);
            var ratingDataOpponent = desInfo.Deserialize<TrueskillRatingChange>(submitMatch.MatchUserRatings.First(f => f.UserId == 11).RatingData);


            Assert.That(ratingDataSubmitter.NewSkill, Is.Not.EqualTo(subStartRating.Skill));
            Assert.That(ratingDataOpponent.NewSkill, Is.Not.EqualTo(oppStartRating.Skill));
        }

    }
}
