using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using FreshCommonUtility.SqlHelper;
using Pc.Information.DataAccess.Common;
using Pc.Information.Model.QuestionInfo;

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
            var searchSql = string.Format(@"INSERT INTO {0} (
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
	)", DataTableGlobal.PiFquestioninfo);
            var param = new DynamicParameters(newQuestionInfo);
            var id = SqlHelper.ExcuteNonQuery(searchSql, param);
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
            var searchSql = string.Format(@"UPDATE {0}
SET PiFQuestionTitle =@PiFQuestionTitle,
 PiFQuestionContent =@PiFQuestionContent
WHERE
	Id =@Id", DataTableGlobal.PiFquestioninfo);
            var param = new DynamicParameters(newQuestionInfo);
            var id = SqlHelper.ExcuteNonQuery(searchSql, param);
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
        public List<PiFQuestionInfoModel> SearchQuestionInfo(long id = 0, DateTime startTime = default(DateTime), DateTime endTime = default(DateTime), string title = null, int pageIndex = 1, int pageSize = 10)
        {
            var strWhere = new StringBuilder();
            if (id > 0) strWhere.Append(" and Id=@id ");
            if (startTime != default(DateTime) && startTime > new DateTime(1900, 1, 1)) strWhere.Append(" and PiFCreateTime>@startTime ");
            if (endTime != default(DateTime) && endTime > new DateTime(1900, 1, 1)) strWhere.Append(" and PiFCreateTime<@endTime ");
            if (!string.IsNullOrEmpty(title)) strWhere.Append(" and PiFQuestionTitle like @PiFQuestionTitle ");
            var orderBy = " order by Id desc ";
            var fieldList = " * ";
            long countNumber;
            var param = new DynamicParameters();
            param.Add("id", id);
            param.Add("startTime", startTime, DbType.DateTime);
            param.Add("endTime", endTime, DbType.DateTime);
            param.Add("PiFQuestionTitle", "%" + title + "%");
            var errorLogList = SqlHelper.SearchPageList<PiFQuestionInfoModel>(DataTableGlobal.PiFquestioninfo, strWhere.ToString(), orderBy,
                 fieldList, pageIndex, pageSize, param, out countNumber);
            return errorLogList.ToList();
        }

        /// <summary>
        /// search question list info by questionid set.
        /// </summary>
        /// <param name="questionList">question id set</param>
        /// <param name="hotNumber">hot number.</param>
        /// <returns></returns>
        public List<PiFQuestionInfoModel> SearchQuestionInfo(IEnumerable<int> questionList, int hotNumber = 0)
        {
            var searchSql = string.Empty;
            if (questionList == null || !questionList.Any())
            {
                searchSql = string.Format(@" select * from {0} LIMIT 0,{1} ORDER BY Id desc ", DataTableGlobal.PiFquestioninfo, hotNumber < 1 ? 10 : hotNumber);
            }
            else
            {
                searchSql = string.Format(@"select * from {0}
where id in({1}) ", DataTableGlobal.PiFquestioninfo, questionList.Aggregate(string.Empty, (current, item) => current + (item + ",")).TrimEnd(','));
            }
            var result = SqlHelper.FindToList<PiFQuestionInfoModel>(searchSql, null);
            return result.ToList();
        }

        /// <summary>
        /// Get question view count
        /// </summary>
        /// <param name="questionIdList">questionIdList</param>
        /// <param name="topNumber">topNumber</param>
        /// <returns></returns>
        public List<PiFQuestionViewCountInfoModel> GetQuestionViewCount(IEnumerable<int> questionIdList, int topNumber = 0)
        {
            var strWhere = new StringBuilder();
            if (questionIdList != null && questionIdList.Any())
            {
                strWhere.AppendFormat(" and PiFQuestionId in ({0}) ", questionIdList.Aggregate(string.Empty, (current, item) => current + (item + ",")).TrimEnd(','));
                topNumber = questionIdList.Distinct().Count();
            }
            else
            {
                topNumber = 10;
            }
            var groupBy = " PiFQuestionId ";
            var orderBy = " order by Id desc ";
            var fieldList = " PiFQuestionId, Count(PiFQuestionId) AS ViewCount ";
            long countNumber;
            var resulteList = SqlHelper.SearchPageList<PiFQuestionViewCountInfoModel>(DataTableGlobal.PiFQuestionViewInfo, strWhere.ToString(), orderBy,
                 fieldList, 1, topNumber, null, out countNumber, groupBy);
            return resulteList.ToList();
        }

        /// <summary>
        /// Get question reply view count
        /// </summary>
        /// <param name="questionIdList">questionIdList</param>
        /// <param name="topNumber">topNumber</param>
        /// <returns></returns>
        public List<PiFQuestionViewCountInfoModel> GetQuestionReplyCount(IEnumerable<int> questionIdList, int topNumber = 0)
        {
            var strWhere = new StringBuilder();
            if (questionIdList != null && questionIdList.Any())
            {
                strWhere.AppendFormat(" and PiFQuestionId in ({0}) ", questionIdList.Aggregate(string.Empty, (current, item) => current + (item + ",")).TrimEnd(','));
                topNumber = questionIdList.Distinct().Count();
            }
            else
            {
                topNumber = 10;
            }
            var groupBy = " PiFQuestionId ";
            var orderBy = " order by Id desc ";
            var fieldList = " PiFQuestionId, Count(PiFQuestionId) AS ViewCount ";
            long countNumber;
            var resulteList = SqlHelper.SearchPageList<PiFQuestionViewCountInfoModel>(DataTableGlobal.PiFquestionreplyinfo, strWhere.ToString(), orderBy,
                 fieldList, 1, topNumber, null, out countNumber, groupBy);
            return resulteList.ToList();
        }

        /// <summary>
        /// Add view question detail page data.
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int AddQuestionViewData(int questionId, int userId)
        {
            var newViewModel = new PiFQuestionViewInfoModel { PiFQuestionId = questionId, PiFUserId = userId, PiFVisitTime = DateTime.Now };
            if (newViewModel == null || newViewModel.PiFQuestionId < 1) return 0;
            var searchSql = string.Format(@"INSERT INTO {0} (
	PiFQuestionId,
	PiFUserId,
	PiFVisitTime
)
VALUES
	(
		@PiFQuestionId,
		@PiFUserId,
		@PiFVisitTime
	)", DataTableGlobal.PiFQuestionViewInfo);
            var param = new DynamicParameters(newViewModel);
            var id = SqlHelper.ExcuteNonQuery(searchSql, param);
            return id;
        }

        /// <summary>
        /// Get question view count
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public long GetQuestionViewNumber(int questionId)
        {
            if (questionId < 1) return 0;
            var searchSql = string.Format(@"SELECT count(id) from {0} 
where PiFQuestionId={1}", DataTableGlobal.PiFQuestionViewInfo, questionId);
            var id = SqlHelper.ExcuteNonQuery(searchSql, null);
            return id;
        }
    }
}
