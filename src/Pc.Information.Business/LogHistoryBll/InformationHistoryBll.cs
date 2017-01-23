using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Pc.Information.CoreModel;
using Pc.Information.DataAccess.LogDataAccess;
using Pc.Information.Interface.ILogHistoryBll;
using Pc.Information.Model.InformationLog;
using Pc.Information.Model.User;
using Pc.Information.Utility.Cache;
using Pc.Information.Utility.Configure;

namespace Pc.Information.Business.LogHistoryBll
{
    /// <summary>
    /// Information bll
    /// </summary>
    public class InformationHistoryBll : IInformationHistoryBll
    {
        #region [1、Private method]

        /// <summary>
        /// write log info to redis
        /// </summary>
        /// <param name="chatHistoryModel">log info.</param>
        /// <returns></returns>
        private Task<long> WriteAllLogToRedis(PiFInformationLogModel chatHistoryModel)
        {
            var cachKey = AppConfigurationHelper.GetAppSettings<CacheLogModel>("AppSettings:ChatInfoHistoryCache");
            cachKey = string.IsNullOrEmpty(cachKey?.Cachekey) ? new CacheLogModel { Cachekey = "ChatInfoHistoryInfo", DatabaseNumber = 1 } : cachKey;
            var addTask = RedisCacheHelper.AddListAsync(cachKey.Cachekey, chatHistoryModel, cachKey.DatabaseNumber);
            return addTask;
        }

        /// <summary>
        /// Stock Method
        /// </summary>
        private async void StockChatHistoryInfo()
        {
            var logInfoDataAccess = new ChatInfoHistoryDataAccess();
            var cacheKey = AppConfigurationHelper.GetAppSettings<CacheLogModel>("AppSettings:ChatInfoHistoryCache");
            cacheKey = string.IsNullOrEmpty(cacheKey?.Cachekey) ? new CacheLogModel { Cachekey = "ChatInfoHistoryInfo", DatabaseNumber = 1 } : cacheKey;
            long cacheListLength = RedisCacheHelper.GetListLength(cacheKey.Cachekey, cacheKey.DatabaseNumber);
            while (cacheListLength > 0)
            {
                var cacheModel = RedisCacheHelper.GetLastOneList<PiFInformationLogModel>(cacheKey.Cachekey,
                    cacheKey.DatabaseNumber);
                await logInfoDataAccess.AddChatInfoAsync(cacheModel);
                cacheListLength = RedisCacheHelper.GetListLength(cacheKey.Cachekey, cacheKey.DatabaseNumber);
            }
        }
        #endregion

        #region [2、Interface]
        /// <summary>
        /// Add chat info to db.
        /// </summary>
        /// <param name="chatModel"></param>
        /// <returns></returns>
        public long AddChatInfo(PiFInformationLogModel chatModel)
        {
            if (chatModel == null) return 0;
            var resulte = WriteAllLogToRedis(chatModel);
            if (resulte.Result > 0)
            {
                StockChatHistoryInfo();
            }
            return resulte.Result;
        }

        /// <summary>
        /// Add new online user nodel.
        /// </summary>
        /// <param name="onlineModel">new user info</param>
        /// <returns></returns>
        public long AddOnlineUser(OnlineUserModel onlineModel)
        {
            var cachKey = AppConfigurationHelper.GetAppSettings<CacheLogModel>("AppSettings:OnlineUserCache");
            cachKey = string.IsNullOrEmpty(cachKey?.Cachekey) ? new CacheLogModel { Cachekey = "OnlineUserCacheInfo", DatabaseNumber = 2 } : cachKey;
            var addTask = RedisCacheHelper.AddListAsync(cachKey.Cachekey, onlineModel, cachKey.DatabaseNumber);
            return addTask.Result;
        }

        /// <summary>
        /// Remove off line user info
        /// </summary>
        /// <param name="removeUserModel">need remove user model.</param>
        /// <returns>the number of removed elements</returns>
        public long RemoveOnlineUser(OnlineUserModel removeUserModel)
        {
            var cachKey = AppConfigurationHelper.GetAppSettings<CacheLogModel>("AppSettings:OnlineUserCache");
            cachKey = string.IsNullOrEmpty(cachKey?.Cachekey) ? new CacheLogModel { Cachekey = "OnlineUserCacheInfo", DatabaseNumber = 2 } : cachKey;
            var addTask = RedisCacheHelper.RemoveValueListAsync(cachKey.Cachekey, removeUserModel, cachKey.DatabaseNumber);
            return addTask.Result;
        }

        /// <summary>
        /// Get list range info.
        /// </summary>
        /// <param name="ruleType">rule type:1:usually user;2:teacher user.</param>
        /// <param name="start">start index,begin is zore.</param>
        /// <param name="fail">the fail of list.</param>
        /// <returns>rule type online user list</returns>
        public List<OnlineUserModel> GetAllOnlineUserModels(int ruleType = 0, long start = 0, long fail = -1)
        {
            var cacheKey = AppConfigurationHelper.GetAppSettings<CacheLogModel>("AppSettings:OnlineUserCache");
            cacheKey = string.IsNullOrEmpty(cacheKey?.Cachekey) ? new CacheLogModel { Cachekey = "OnlineUserCacheInfo", DatabaseNumber = 2 } : cacheKey;
            var userListTask = RedisCacheHelper.GetListRangeAsync<OnlineUserModel>(cacheKey.Cachekey, start, fail,
                cacheKey.DatabaseNumber);
            var userList = userListTask.Result;
            if (userList == null || !userList.Any()) return new List<OnlineUserModel>();
            if (ruleType != 0) return userList.Where(f => f.RuleType == ruleType).ToList();
            return userList;
        }
        #endregion
    }
}
