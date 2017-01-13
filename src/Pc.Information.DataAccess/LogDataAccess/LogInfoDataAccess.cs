using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pc.Information.Model.ErrorInfoLog;
using Pc.Information.Model.User;
using Pc.Information.Utility.FreshSqlHelper;

namespace Pc.Information.DataAccess.LogDataAccess
{
    public class LogInfoDataAccess
    {
        /// <summary>
        /// Add log
        /// </summary>
        /// <param name="newLogInfo"></param>
        /// <returns></returns>
        public int AddLogInfo(ErrorInfoLogModel newLogInfo)
        {
            if (newLogInfo == null) return 0;
            var searchSql = @"INSERT INTO piferrorlog (
	ContentType,
	ErrorMessage,
	InnerErrorMessage,
	ErrorTypeFullName,
	StackTrace,
	ErrorTime,
	ErrorTyp
)
VALUES
	(
		@ContentType,
		@ErrorMessage,
		@InnerErrorMessage,
		@ErrorTypeFullName,
		@StackTrace,
		@ErrorTime,
		ErrorTyp
	)";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters(newLogInfo);
            var userId = sqlHelper.ExcuteNonQuery(searchSql, param);
            return userId;
        }
    }
}
