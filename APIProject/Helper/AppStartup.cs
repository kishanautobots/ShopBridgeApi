
using DataModel;

namespace Api.Helpers
{
    public class AppStartup
    {
        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load()
        {
            ConnectionStringPath.Connection = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;
        }
    }
}