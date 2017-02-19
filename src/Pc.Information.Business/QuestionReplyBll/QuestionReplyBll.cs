using System;
using System.Collections.Generic;
using System.Linq;
using Pc.Information.DataAccess.ReplyDataAccess;
using Pc.Information.Interface.IQuestionReplyBll;
using Pc.Information.Model.QuestionInfo;

namespace Pc.Information.Business.QuestionReplyBll
{
    /// <summary>
    /// Question Reply info.
    /// </summary>
    public class QuestionReplyBll : IQuestionReplyBll
    {
        /// <summary>
        /// Updata question info.
        /// </summary>
        /// <param name="newQuestionReplyInfo"></param>
        /// <returns></returns>
        public int AddQuestionReplyInfo(PiFQuestionReplyInfoModel newQuestionReplyInfo)
        {
            return new ReplyDataAccess().AddQuestionReplyInfo(newQuestionReplyInfo);
        }

        /// <summary>
        /// search Qustion reply info.
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
            return new ReplyDataAccess().GetReplyInfoList(out countNumber, questionId, likeContent, startTime, endTime, userId, pageIndex, pageSize);
        }

        /// <summary>
        /// add reply praised info
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="replyId">reply id</param>
        /// <returns></returns>
        public int AddReplyPraised(int replyId, int userId)
        {
            if (userId < 1 || replyId < 1) return 0;
            var praisedModel = new PiFReplyPraisedInfoModel { PiFPraisedTime = DateTime.Now, PiFReplyId = replyId, PiFUerId = userId };
            var dataAccess = new ReplyDataAccess();
            var hasPraisedList = dataAccess.GetPraisedInList(replyId, userId);
            if (hasPraisedList != null && hasPraisedList.Any()) return 2;
            var id = dataAccess.AddReplyPraised(praisedModel);
            return id;
        }
    }
}
