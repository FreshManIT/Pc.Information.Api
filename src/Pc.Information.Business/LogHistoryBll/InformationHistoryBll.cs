using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pc.Information.CoreModel;
using Pc.Information.DataAccess.LogDataAccess;
using Pc.Information.Interface.ILogHistoryBll;
using Pc.Information.Model.InformationLog;
using Pc.Information.Utility.Cache;
using Pc.Information.Utility.Configure;

namespace Pc.Information.Business.LogHistoryBll
{
    /// <summary>
    /// 
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
        #endregion
    }
}
