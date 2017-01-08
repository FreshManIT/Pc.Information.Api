using MySql.Data.MySqlClient;
using Pc.Information.Utility.Configure;

namespace Pc.Information.DataAccess.FreshSqlHelper
{
    /// <summary>
    /// Sql connection helper class.Use this helper class must first use InitConnectionServer function.
    /// </summary>
    public class FreshSqlConnectionHelper
    {
        /// <summary>
        /// Init connection server function.
        /// </summary>
        public static void InitConnectionServer()
        {
            ConnectionString = AppConfigurationHelper.GetAppSettings("MySqlConnectionString");
        }

        /// <summary>
        /// Get connection string.
        /// </summary>
        private static string ConnectionString { get; set; }

        /// <summary>
        /// Return connection object.
        /// </summary>
        private static MySqlConnection _conn = null;

        /// <summary>
        /// Lock resource
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// cut put point.
        /// </summary>
        private FreshSqlConnectionHelper() { }

        /// <summary>
        /// Get single connection object
        /// </summary>
        /// <param name="connectionString">you need new connection object.</param>
        /// <returns></returns>
        public static MySqlConnection GetConnection(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString) && connectionString.GetHashCode() != ConnectionString.GetHashCode())
            {
                _conn = new MySqlConnection(connectionString);
            }
            if (_conn == null)
            {
                lock (SyncRoot)
                {
                    if (_conn == null)
                    {
                        _conn = new MySqlConnection(ConnectionString);
                    }
                }
            }
            return _conn;
        }
    }
}
