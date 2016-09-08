using System.Collections.Generic;
using SmartPong.Models;

namespace SmartPong
{
    public interface ISmartPongRepository
    {
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
