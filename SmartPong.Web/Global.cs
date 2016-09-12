using System.Configuration;
using SmartPong.Core;

namespace SmartPong
{
    public class Global
    {
        public static SmartPongContext DbContext = new SmartPongContext(ConfigurationManager.ConnectionStrings["Testing"].ConnectionString);


    }
}