using System;
using System.Collections.Generic;
using System.Linq;
using Pc.Information.DataAccess.Common;
using Dapper;
using Pc.Information.Model.QuestionInfo;
using Pc.Information.Utility.FreshSqlHelper;
using System.Text;
using System.Data;

namespace Pc.Information.DataAccess.ReplyDataAccess
{
    /// <summary>
    /// question reply data access
    /// </summary>
    public class ReplyDataAccess
    {
        /// <summary>
        /// search question reply info
        /// </summary>
        /// <param name="countNumber">countNumber</param>
        /// <param name="questionId">questionId</param>
        /// <param name="likeContent">likeContent</param>
        /// <param name="startTime">startTime</param>
        /// <param name="endTime">endTime</param>
        /// <param name="userId">userId</param>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <returns></returns>
        public List<PiFQuestionReplyInfoModel> GetReplyInfoList(out long countNumber,int questionId = 0, string likeContent = null, DateTime? startTime = null, DateTime? endTime = null, int userId = 0, int pageIndex = 1, int pageSize = 10)
        {
            var strWhere = new StringBuilder(" and reply.PiFReplyUserId = users.Id ");
            if (questionId > 0) strWhere.Append(" and reply.PiFQuestionId=@id ");
            if (startTime != null && startTime != default(DateTime) && startTime > new DateTime(1900, 1, 1)) strWhere.Append(" and PiFReplyTime>@startTime ");
            if (endTime != null && endTime != default(DateTime) && endTime > new DateTime(1900, 1, 1)) strWhere.Append(" and PiFReplyTime<@endTime ");
            if (!string.IsNullOrEmpty(likeContent)) strWhere.Append(" and PiFReplyContent like @PiFReplyContent ");
            var orderBy = " order by Id desc ";
            var fieldList = " reply.*, users.PiFUserName ";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("id", questionId);
            param.Add("startTime", startTime, DbType.DateTime);
            param.Add("endTime", endTime, DbType.DateTime);
            param.Add("PiFReplyContent", "%" + likeContent + "%");
            var errorLogList = sqlHelper.SearchPageList<PiFQuestionReplyInfoModel>(DataTableGlobal.PiFquestionreplyinfo + " reply,"+DataTableGlobal.PiFUsers +" users " , strWhere.ToString(), orderBy,
                fieldList, pageIndex, pageSize, param, out countNumber);
            return errorLogList.ToList();
        }

        /// <summary>
        /// add new reply info.
        /// </summary>
        /// <param name="newReplyInfo">new reply info.</param>
        /// <returns></returns>
        public int AddQuestionReplyInfo(PiFQuestionReplyInfoModel newReplyInfo)
        {
            if (newReplyInfo == null || newReplyInfo.PiFQuestionId < 1 || newReplyInfo.PiFReplyUserId < 1) return 0;
            var searchSql = string.Format(@"INSERT INTO {0} (
	PiFQuestionId,
	PiFReplyContent,
	PiFReplyIsBest,
	PiFReplyTime,
	PiFReplyUserId
)
VALUES
	(
		@PiFQuestionId,
		@PiFReplyContent,
		0,
		@PiFReplyTime,
		@PiFReplyUserId
	)", DataTableGlobal.PiFquestionreplyinfo);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters(newReplyInfo);
            var userId = sqlHelper.ExcuteNonQuery(searchSql, param);
            return userId;
        }
    }
}
