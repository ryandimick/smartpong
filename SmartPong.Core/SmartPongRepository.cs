using System.Collections.Generic;
using SmartPong.Models;

namespace SmartPong
{
    public class SmartPongRepository : ISmartPongRepository
    {
        private readonly SettingsManager _settingsManager;
        private readonly UserManager _userManager;

        internal SmartPongRepository(string connectionString)
        {
            var context = new SmartPongContext(connectionString);
            _userManager = new UserManager(context);
            _settingsManager = new SettingsManager(context);
        }

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
        public User CreateUser(string username, string givenName, string surname, string email, string nickname = "")
        {
            return _userManager.CreateUser(username, givenName, surname, email, nickname);
        }

        /// <summary>
        /// 
        /// Disables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="userId">The unique identifier of the user.</param>
        /// 
        /// <returns>The disabled user with updated values.</returns>
        public User DisableUser(int userId)
        {
            return _userManager.DisableUser(userId);
        }

        /// <summary>
        /// 
        /// Disables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="username">The unique authentication credential for the user.</param>
        /// 
        /// <returns>The disabled user with updated values.</returns>
        public User DisableUser(string username)
        {
            return _userManager.DisableUser(username);
        }

        /// <summary>
        /// 
        /// Enables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="userId">The unique identifier of the user.</param>
        /// 
        /// <returns>The enabled user with updated values.</returns>
        public User EnableUser(int userId)
        {
            return _userManager.EnableUser(userId);
        }

        /// <summary>
        /// 
        /// Enables a user.
        /// 
        /// </summary>
        /// 
        /// <param name="username">The unique authentication credential for the user.</param>
        /// 
        /// <returns>The enabled user with updated values.</returns>
        public User EnableUser(string username)
        {
            return _userManager.EnableUser(username);
        }

        /// <summary>
        /// 
        /// Returns all settings.
        /// 
        /// </summary>
        /// 
        /// <returns>The collection of settings.</returns>
        public IEnumerable<Setting> RetrieveSettings()
        {
            return _settingsManager.RetrieveSettings();
        } 

        /// <summary>
        /// 
        /// Returns a user.
        /// 
        /// </summary>
        /// 
        /// <param name="userId">The unique identifier of the user.</param>
        /// 
        /// <returns>The requested user.</returns>
        public User RetrieveUser(int userId)
        {
            return _userManager.RetrieveUser(userId);
        }

        /// <summary>
        /// 
        /// Returns a user.
        /// 
        /// </summary>
        /// 
        /// <param name="username">The unique authentication credential for the user.</param>
        /// 
        /// <returns>The requested user.</returns>
        public User RetrieveUser(string username)
        {
            return _userManager.RetrieveUser(username);
        }

        /// <summary>
        /// 
        /// Updates a configuration setting with a new value.
        /// 
        /// </summary>
        /// 
        /// <param name="updatedSetting">The setting object containing updated values.</param>
        /// 
        /// <returns>The setting object with updated values.</returns>
        public Setting UpdateSetting(Setting updatedSetting)
        {
            return _settingsManager.UpdateSetting(updatedSetting);
        }

        /// <summary>
        /// 
        /// Updates a user with new values.
        /// 
        /// </summary>
        /// 
        /// <param name="updatedUser">The user object containing updated values.</param>
        /// 
        /// <returns>The user object with updated values.</returns>
        public User UpdateUser(User updatedUser)
        {
            return _userManager.UpdateUser(updatedUser);
        }
    }
}
