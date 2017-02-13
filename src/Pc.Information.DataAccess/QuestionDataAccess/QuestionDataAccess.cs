using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Pc.Information.DataAccess.Common;
using Pc.Information.Model.QuestionInfo;
using Pc.Information.Utility.FreshSqlHelper;

namespace Pc.Information.DataAccess.QuestionDataAccess
{
    /// <summary>
    /// Question data access class
    /// </summary>
    public class QuestionDataAccess
    {
        /// <summary>
        /// Add new question info.
        /// </summary>
        /// <param name="newQuestionInfo"></param>
        /// <returns></returns>
        public int AddQuestionInfo(PiFQuestionInfoModel newQuestionInfo)
        {
            if (newQuestionInfo == null) return 0;
            var searchSql = @"INSERT INTO pifquestioninfo (
	PiFQuestionTitle,
	PiFQuestionContent,
	PiFCreateTime,
	PiFSendUserId,
	PiFSendUserName
)
VALUES
	(
		@PiFQuestionTitle,
		@PiFQuestionContent,
		@PiFCreateTime,
		@PiFSendUserId,
		@PiFSendUserName
	)";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters(newQuestionInfo);
            var id = sqlHelper.ExcuteNonQuery(searchSql, param);
            return id;
        }

        /// <summary>
        /// Updata question info.
        /// </summary>
        /// <param name="newQuestionInfo"></param>
        /// <returns></returns>
        public int UpdateQuestionInfo(PiFQuestionInfoModel newQuestionInfo)
        {
            if (newQuestionInfo == null || newQuestionInfo.Id < 1) return 0;
            var searchSql = @"UPDATE pifquestioninfo
SET PiFQuestionTitle =@PiFQuestionTitle,
 PiFQuestionContent =@PiFQuestionContent
WHERE
	Id =@Id";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters(newQuestionInfo);
            var id = sqlHelper.ExcuteNonQuery(searchSql, param);
            return id;
        }

        /// <summary>
        /// search Qustion info.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="startTime">start time</param>
        /// <param name="endTime">end time</param>
        /// <param name="title">Fuzzy search word</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">pagesize</param>
        /// <returns></returns>
        public List<PiFQuestionInfoModel> SearchQustionInfo(long id = 0, DateTime startTime = default(DateTime), DateTime endTime = default(DateTime), string title = null, int pageIndex = 1, int pageSize = 10)
        {
            var strWhere = new StringBuilder();
            if (id > 1) strWhere.Append(" and Id=@id ");
            if (startTime != default(DateTime)) strWhere.Append(" and PiFCreateTime>@startTime ");
            if (endTime != default(DateTime)) strWhere.Append(" and PiFCreateTime<@endTime ");
            if (!string.IsNullOrEmpty(title)) strWhere.Append(" and PiFQuestionTitle like @PiFQuestionTitle ");
            var orderBy = " order by Id desc ";
            var fieldList = " * ";
            long countNumber;
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("id", id);
            param.Add("startTime", startTime, DbType.DateTime);
            param.Add("endTime", endTime, DbType.DateTime);
            param.Add("PiFQuestionTitle", "%" + title + "%");
            var errorLogList = sqlHelper.SearchPageList<PiFQuestionInfoModel>(DataTableGlobal.PiFquestioninfo, strWhere.ToString(), orderBy,
                fieldList, pageIndex, pageSize, param, out countNumber);
            return errorLogList.ToList();
        }
    }
}
