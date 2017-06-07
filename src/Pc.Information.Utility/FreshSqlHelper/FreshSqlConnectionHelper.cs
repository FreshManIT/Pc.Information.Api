using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using Pc.Information.CoreModel;
using Pc.Information.Utility.Configure;
using Pc.Information.Utility.Dapper;

namespace Pc.Information.Utility.FreshSqlHelper
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
            var appSettings = AppConfigurationHelper.GetAppSettings<AppSettingsModel>("AppSettings");
            ConnectionString = appSettings?.MySqlConnectionString;
        }

        /// <summary>
        /// Get connection string.
        /// </summary>
        private static string ConnectionString { get; set; }

        /// <summary>
        /// Return connection object.
        /// </summary>
        private static MySqlConnection _conn;

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
        public static MySqlConnection GetMySqlConnectionConnection(string connectionString = null)
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

        /// <summary>
        /// Get connection string.
        /// </summary>
        public static string GetConnectionString() => ConnectionString;

        /// <summary>
        /// set connection type
        /// </summary>
        /// <param name="dbtype"></param>
        public static void SetConnectionType(SimpleCRUD.Dialect dbtype)
        {
            _dbtype = dbtype;
        }

        /// <summary>
        /// connection type
        /// </summary>
        private static SimpleCRUD.Dialect _dbtype;

        /// <summary>
        /// Get open connection
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetOpenConnection()
        {
            IDbConnection connection;
            if (_dbtype == SimpleCRUD.Dialect.PostgreSQL)
            {
                connection = new NpgsqlConnection(
                    $"Server={"localhost"};Port={"5432"};User Id={"postgres"};Password={"postgrespass"};Database={"testdb"};");
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            }
            //else if (_dbtype == SimpleCRUD.Dialect.SQLite)
            //{
            //    connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            //    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
            //}
            else if (_dbtype == SimpleCRUD.Dialect.MySQL)
            {
                connection = new MySqlConnection(
                    $"Server={"localhost"};Port={"3306"};User Id={"admin"};Password={"admin"};Database={"testdb"};");
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            }
            else
            {
                connection = new SqlConnection(@"Data Source = (LocalDB)\v11.0;Initial Catalog=DapperSimpleCrudTestDb;Integrated Security=True;MultipleActiveResultSets=true;");
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
            }

            connection.Open();
            return connection;
        }
    }
}
