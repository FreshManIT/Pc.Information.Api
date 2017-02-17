using System;
using System.Collections.Generic;
using System.Linq;
using Pc.Information.DataAccess.QuestionDataAccess;
using Pc.Information.Interface.IQuestionBll;
using Pc.Information.Model.QuestionInfo;

namespace Pc.Information.Business.QuestionBll
{
    /// <summary>
    ///  Interface of question bll.
    /// </summary>
    public class QuestionBll : IQuestionBll
    {
        /// <summary>
        /// Add new question info.
        /// </summary>
        /// <param name="newQuestionInfo"></param>
        /// <returns></returns>
        public int AddQuestionInfo(PiFQuestionInfoModel newQuestionInfo)
        {
            return new QuestionDataAccess().AddQuestionInfo(newQuestionInfo);
        }

        /// <summary>
        /// Add view question info.
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int AddQuestionViewInfo(int questionId, int userId)
        {
            return new QuestionDataAccess().AddQuestionViewData(questionId, userId);
        }

        /// <summary>
        /// Updata question info.
        /// </summary>
        /// <param name="newQuestionInfo"></param>
        /// <returns></returns>
        public int UpdateQuestionInfo(PiFQuestionInfoModel newQuestionInfo)
        {
            return new QuestionDataAccess().UpdateQuestionInfo(newQuestionInfo);
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
        public List<PiFQuestionInfoModel> SearchQustionInfo(long id = 0, DateTime startTime = default(DateTime),
            DateTime endTime = default(DateTime), string title = null, int pageIndex = 1, int pageSize = 10)
        {
            var dataAccess = new QuestionDataAccess();
            var questionInfoList = dataAccess.SearchQuestionInfo(id, startTime, endTime, title, pageIndex, pageSize);
            if (questionInfoList != null && questionInfoList.Any())
            {
                var questionIdList = questionInfoList.Select(f => f.Id);
                var viewListCount = dataAccess.GetQuestionViewCount(questionIdList);
                if (viewListCount != null && viewListCount.Any())
                {
                    questionInfoList.ForEach(f =>
                    {
                        f.ViewCount = viewListCount.FirstOrDefault(r => r.PiFQuestionId == f.Id)?.ViewCount;
                    });
                }
            }
            return questionInfoList;
        }

        /// <summary>
        /// get hot reply question list.
        /// </summary>
        /// <param name="number">number</param>
        /// <returns></returns>
        public List<PiFQuestionInfoWithReplyModel> GetHotReplyQuestionInfo(int number = 10)
        {
            var dataAccess = new QuestionDataAccess();
            var hotReplyQuestionList = dataAccess.GetQuestionReplyCount(null, number);
            var hotReplyQuestionId = hotReplyQuestionList.Select(f => f.PiFQuestionId);
            var hotNumber = 0;
            if (hotReplyQuestionId == null || !hotReplyQuestionId.Any()) hotNumber = 10;
            var hotQuestionList = dataAccess.SearchQuestionInfo(hotReplyQuestionId, hotNumber);
            if (hotQuestionList == null || !hotQuestionList.Any()) return new List<PiFQuestionInfoWithReplyModel>();
            var resulteList = hotQuestionList.Select(f => new PiFQuestionInfoWithReplyModel { Id = f.Id, PiFCreateTime = f.PiFCreateTime, PiFQuestionContent = f.PiFQuestionContent, PiFQuestionTitle = f.PiFQuestionTitle, PiFSendUserId = f.PiFSendUserId, PiFSendUserName = f.PiFSendUserName }).ToList();
            if (hotReplyQuestionList != null && hotReplyQuestionList.Any())
            {
                resulteList.ForEach(r =>
                {
                    var numberInfo = hotReplyQuestionList.FirstOrDefault(e => e.PiFQuestionId == r.Id);
                    r.CountNumber = numberInfo == null ? 0 : numberInfo.ViewCount;
                });
                resulteList = resulteList.OrderByDescending(s => s.CountNumber).ToList();
            }
            return resulteList;
        }

        /// <summary>
        /// GetHotViewQuestionInfo
        /// </summary>
        /// <param name="number">number</param>
        /// <returns></returns>
        public List<PiFQuestionInfoWithReplyModel> GetHotViewQuestionInfo(int number = 10)
        {
            var dataAccess = new QuestionDataAccess();
            var hotViewQuestionList = dataAccess.GetQuestionViewCount(null, number);
            var hotViewQuestionId = hotViewQuestionList.Select(f => f.PiFQuestionId);
            var hotNumber = 0;
            if (hotViewQuestionId == null || !hotViewQuestionId.Any()) hotNumber = 10;
            var hotQuestionList = dataAccess.SearchQuestionInfo(hotViewQuestionId, hotNumber);
            if (hotQuestionList == null || !hotQuestionList.Any()) return new List<PiFQuestionInfoWithReplyModel>();
            var resulteList = hotQuestionList.Select(f => new PiFQuestionInfoWithReplyModel { Id = f.Id, PiFCreateTime = f.PiFCreateTime, PiFQuestionContent = f.PiFQuestionContent, PiFQuestionTitle = f.PiFQuestionTitle, PiFSendUserId = f.PiFSendUserId, PiFSendUserName = f.PiFSendUserName }).ToList();
            if (hotViewQuestionList != null && hotViewQuestionList.Any())
            {
                resulteList.ForEach(r =>
                {
                    var numberInfo = hotViewQuestionList.FirstOrDefault(e => e.PiFQuestionId == r.Id);
                    r.ViewCount = numberInfo == null ? 0 : numberInfo.ViewCount;
                });
                resulteList = resulteList.OrderByDescending(s => s.ViewCount).ToList();
            }
            return resulteList;
        }
    }
}
