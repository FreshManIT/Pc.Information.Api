using System;
using Pc.Information.CoreModel;
using Pc.Information.Utility.Configure;
using StackExchange.Redis;

namespace Pc.Information.Utility.Cache
{
    /// <summary>
    /// FreshMan redis connection helper.
    /// </summary>
    public class FreshRedisConnectionHelper
    {
        /// <summary>
        /// Return connection object.
        /// </summary>
        private static ConnectionMultiplexer _conn;

        /// <summary>
        /// Lock resource
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// cut put point.
        /// </summary>
        private FreshRedisConnectionHelper() { }

        /// <summary>
        /// Get single connection object
        /// </summary>
        /// <returns></returns>
        public static ConnectionMultiplexer GetConnection()
        {
            if (_conn == null)
            {
                lock (SyncRoot)
                {
                    if (_conn == null)
                    {
                        var appSettings = AppConfigurationHelper.GetAppSettings<AppSettingsModel>("AppSettings");
                        var connectionString = appSettings?.RedisCaching?.ConnectionString;
                        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("Redis connection config is Empty,please check you config.");
                        _conn = ConnectionMultiplexer.Connect(connectionString);
                    }
                }
            }
            return _conn;
        }
    }
}
