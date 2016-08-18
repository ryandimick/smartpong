using System.Collections.Generic;
using System.Web;

namespace SmaPong.Business
{
    public static class Resource
    {
        private static Dictionary<int, Dictionary<string, string>> _resources;

        /// <summary>
        /// Returns the internationalized string value for the 
        /// request resource.  Will retrieve the language from 
        /// the Session object.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns>Language-specific string value for resource.</returns>
        public static string Get(string resourceName)
        {
            if (_resources == null)
            {
                Load();
            }

            int langId;
            if (!int.TryParse(HttpContext.Current.Session["LangId"].ToString(), out langId))
            {
                // probably some fancy exception
                return null;
            }

            Dictionary<string, string> languageDictionary;
            if (!_resources.TryGetValue(langId, out languageDictionary))
            {
                // probably some fancy exception
                return null;
            }

            string value;
            if (!languageDictionary.TryGetValue(resourceName, out value))
            {
                // probably some fancy exception
                return null;
            }

            return value;
        }

        // THIS WOULD COME FROM THE API PRE BUILT FOR YOU
        private static void Load()
        {
            // create english values
            var englishValues = new Dictionary<string, string>
            {
                {"IdLabel", "Id"},
                {"UsernameLabel", "User Name"},
                {"FirstnameLabel", "First Name"}
            };

            // create spanish values
            var spanishValues = new Dictionary<string, string>
            {
                {"IdLabel", "Id"},
                {"UsernameLabel", "Nombre de Usuario"},
                {"FirstnameLabel", "Nombre de Pila"}
            };

            // langId 1 = english
            // langId 2 = spanish
            _resources = new Dictionary<int, Dictionary<string, string>> {{1, englishValues}, {2, spanishValues}};
        }
    }
}