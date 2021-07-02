namespace DataModel
{
    public class ConnectionStringPath
    {
        /// <summary>
        /// SQL connection from web config file.
        /// </summary>
        public static string Connection { get; set; }

        /// <summary>
        /// SQL connection to master database from web config file.
        /// </summary>
        public static string MasterConnection { get; set; }
    }
}