using System.Configuration;

namespace SmartPong
{
    public class Global
    {
        public static ISmartPongRepository Repository = RepositoryManager.Create(ConfigurationManager.ConnectionStrings["Testing"].ConnectionString);
    }
}