using System.Threading.Tasks;
using Dapper;
using Pc.Information.Model.InformationLog;
using Pc.Information.Utility.FreshSqlHelper;

namespace Pc.Information.DataAccess.LogDataAccess
{
    /// <summary>
    /// Chat info history data access
    /// </summary>
    public class ChatInfoHistoryDataAccess
    {
        /// <summary>
        /// Add log
        /// </summary>
        /// <param name="newLogInfo">chat model</param>
        /// <returns></returns>
        public Task<int> AddChatInfoAsync(PiFInformationLogModel newLogInfo)
        {
            var searchSql = @"INSERT INTO pifinformationlog (
	PiFFromId,
PiFToId,
PiFToGroupId,
PiFContentType,
PiFContent,
PiFSendTime
)
VALUES
	(
		@PiFFromId,
@PiFToId,
@PiFToGroupId,
@PiFContentType,
@PiFContent,
@PiFSendTime
	)";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters(newLogInfo);
            var id = sqlHelper.ExcuteNonQueryAsync(searchSql, param);
            return id;
        }
    }
}
