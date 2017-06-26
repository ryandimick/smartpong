using System;
using System.Collections.Generic;
using SmartPong.Models;

namespace SmartPong
{
    public interface ISmartPongRepository
    {
        /// <summary>
        /// 
        /// Confirms the outcome of a match and makes the posted rating changes official.
        /// 
        /// </summary>
        /// 
        /// <param name="matchId">The unique identifier of the match's outcome to confirm.</param>
        /// <param name="userId">The unique identifier of the user that confirmed the match's outcome.</param>
        /// 
        /// <returns>The match that was confirmed.</returns>
        Match ConfirmMatch(int matchId, int userId);

        /// <summary>
        /// 
        /// Submits a new match into the SmartPong application that will require confirmation.
        /// 
        /// </summary>
        /// 
        /// <param name="newMatch">The complete match object to create.</param>
        /// 
        /// <returns>The match that was submitted.</returns>
        Match CreateMatch(Match newMatch);

        /// <summary>
        /// 
        /// Creates a user.
        /// 
        /// </summary>
        /// 
        /// <param name="username">The authentication credential for the user.</param>
        /// <param name="givenName">The given name of the user.</param>
        /// <param name="surname">The family name of the user.</param>
        /// <param name="email">A valid e-mail address for the user for notifications.</param>
        /// <param name="nickname">An optional nickname for the user to be used throughout the system.</param>
        /// 
        /// <returns>The user that was created.</returns>
        User CreateUser(string username, string givenName, string surname, string email, string nickname = "");

        /// <summary>
        /// 
        /// Removes a match record and all associated records and rating changes associated with it.
        /// 
        /// </summary>
        /// 
        /// <param name="matchId">The unique identifier of the match to remove.</param>
        void DeleteMatch(int matchId);

        /// <summary>
        /// 
        /// Disables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="userId">The unique identifier of the user.</param>
        /// 
        /// <returns>The disabled user with updated values.</returns>
        User DisableUser(int userId);

        /// <summary>
        /// 
        /// Disables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="username">The unique authentication credential for the user.</param>
        /// 
        /// <returns>The disabled user with updated values.</returns>
        User DisableUser(string username);

        /// <summary>
        /// 
        /// Enables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="userId">The unique identifier of the user.</param>
        /// 
        /// <returns>The enabled user with updated values.</returns>
        User EnableUser(int userId);

        /// <summary>
        /// 
        /// Enables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="username">The unique authentication credential for the user.</param>
        /// 
        /// <returns>The enabled user with updated values.</returns>
        User EnableUser(string username);

        /// <summary>
        /// 
        /// Returns all matches.
        /// 
        /// </summary>
        /// 
        /// <returns>The collection of matches.</returns>
        IEnumerable<Match> RetrieveMatches();

        /// <summary>
        /// 
        /// Returns all matches that meet criteria.
        /// 
        /// </summary>
        /// 
        /// <param name="predicate">The criteria a match must meet.</param>
        /// 
        /// <returns>The collection of matches.</returns>
        IEnumerable<Match> RetrieveMatches(Func<Match, bool> predicate);

        /// <summary>
        /// 
        /// Returns all user ratings for a specified rating type.
        /// 
        /// </summary>
        /// 
        /// <param name="type">The user rating type to retrieve.</param>
        /// 
        /// <returns>The collection of user ratings.</returns>
        IEnumerable<UserRating> RetrieveUserRatings(UserRatingType type);

        /// <summary>
        /// 
        /// Returns all settings.
        /// 
        /// </summary>
        /// 
        /// <returns>The collection of settings.</returns>
        IEnumerable<Setting> RetrieveSettings();

        /// <summary>
        /// 
        /// Returns a user.
        /// 
        /// </summary>
        /// 
        /// <param name="userId">The unique identifier of the user.</param>
        /// 
        /// <returns>The requested user.</returns>
        User RetrieveUser(int userId);

        /// <summary>
        /// 
        /// Returns a user.
        /// 
        /// </summary>
        /// 
        /// <param name="username">The unique authentication credential for the user.</param>
        /// 
        /// <returns>The requested user.</returns>
        User RetrieveUser(string username);

        /// <summary>
        /// 
        /// Returns all users that meet the specified criteria.
        /// 
        /// </summary>
        /// 
        /// <param name="predicate">The criteria a user must mee.</param>
        /// 
        /// <returns>The collection of users.</returns>
        IEnumerable<User> RetrieveUsers(Func<User, bool> predicate);

        /// <summary>
        /// 
        /// Updates a configuration setting with a new value.
        /// 
        /// </summary>
        /// 
        /// <param name="updatedSetting">The setting object containing updated values.</param>
        /// 
        /// <returns>The setting object with updated values.</returns>
        Setting UpdateSetting(Setting updatedSetting);

        /// <summary>
        /// 
        /// Updates a user with new values.
        /// 
        /// </summary>
        /// 
        /// <param name="updatedUser">The user object containing updated values.</param>
        /// 
        /// <returns>The user object with updated values.</returns>
        User UpdateUser(User updatedUser);
    }
}
