using System;
using System.Collections.Generic;
using System.Linq;
using Pc.Information.DataAccess.Common;
using Dapper;
using Pc.Information.Model.QuestionInfo;
using System.Text;
using System.Data;
using FreshCommonUtility.SqlHelper;

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
        public List<PiFQuestionReplyInfoModel> GetReplyInfoList(out long countNumber, int questionId = 0, string likeContent = null, DateTime? startTime = null, DateTime? endTime = null, int userId = 0, int pageIndex = 1, int pageSize = 10)
        {
            var strWhere = new StringBuilder(" and reply.PiFReplyUserId = users.Id ");
            if (questionId > 0) strWhere.Append(" and reply.PiFQuestionId=@id ");
            if (startTime != null && startTime != default(DateTime) && startTime > new DateTime(1900, 1, 1)) strWhere.Append(" and PiFReplyTime>@startTime ");
            if (endTime != null && endTime != default(DateTime) && endTime > new DateTime(1900, 1, 1)) strWhere.Append(" and PiFReplyTime<@endTime ");
            if (!string.IsNullOrEmpty(likeContent)) strWhere.Append(" and PiFReplyContent like @PiFReplyContent ");
            var orderBy = " order by Id desc ";
            var fieldList = string.Format(@" reply.*, users.PiFUserName ,
    (
        SELECT
            count(*)
        FROM
            {0}
        WHERE
            {0}.PiFReplyId = reply.Id
    ) AS PraisedNumber,
    (
        SELECT
            COUNT(*)
        FROM
            {0}
        WHERE
            {0}.PiFReplyId = reply.Id
        AND {0}.PiFUerId = {1}
	) AS HasePraise ", DataTableGlobal.PiFreplypraisedinfo,userId);
            var param = new DynamicParameters();
            param.Add("id", questionId);
            param.Add("startTime", startTime, DbType.DateTime);
            param.Add("endTime", endTime, DbType.DateTime);
            param.Add("PiFReplyContent", "%" + likeContent + "%");
            var errorLogList = SqlHelper.SearchPageList<PiFQuestionReplyInfoModel>(DataTableGlobal.PiFquestionreplyinfo + " reply," + DataTableGlobal.PiFUsers + " users ", strWhere.ToString(), orderBy,
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
            var param = new DynamicParameters(newReplyInfo);
            var userId = SqlHelper.ExcuteNonQuery(searchSql, param);
            return userId;
        }

        /// <summary>
        /// Add reply info
        /// </summary>
        /// <param name="replyPraisedModel">reply praised model</param>
        /// <returns></returns>
        public int AddReplyPraised(PiFReplyPraisedInfoModel replyPraisedModel)
        {
            if (replyPraisedModel == null || replyPraisedModel.PiFReplyId < 1 || replyPraisedModel.PiFUerId < 1) return 0;
            var searchSql = string.Format(@"INSERT INTO {0} (
	PiFUerId,
	PiFPraisedTime,
	PiFReplyId,
	PiFPraisedType
)
VALUES
	(
		@PiFUerId,
		@PiFPraisedTime,
		@PiFReplyId,
		@PiFPraisedType
	)", DataTableGlobal.PiFreplypraisedinfo);
            var param = new DynamicParameters(replyPraisedModel);
            var id = SqlHelper.ExcuteNonQuery(searchSql, param);
            return id;
        }

        /// <summary>
        /// Get praised data.
        /// </summary>
        /// <param name="replyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<PiFReplyPraisedInfoModel> GetPraisedInList(int replyId, int userId)
        {
            if (replyId < 1 && userId < 1) return null;
            var searchSql = new StringBuilder();
            searchSql.AppendFormat(@"SELECT
	* 
FROM 
	{0} 
WHERE 
	1 = 1 ", DataTableGlobal.PiFreplypraisedinfo);
            if (replyId > 0) searchSql.AppendFormat(@" AND PiFReplyId = {0} ", replyId);
            if (userId > 0) searchSql.AppendFormat(@" AND PiFUerId = {0} ", userId);
            var resulteList = SqlHelper.FindToList<PiFReplyPraisedInfoModel>(searchSql.ToString(), null);
            return resulteList.ToList();
        }
    }
}
