using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pc.Information.Model.QuestionInfo;

namespace Pc.Information.Interface.IQuestionReplyBll
{
    /// <summary>
    /// Interface question reply
    /// </summary>
    public interface IQuestionReplyBll
    {
        /// <summary>
        /// Updata question info.
        /// </summary>
        /// <param name="newQuestionReplyInfo"></param>
        /// <returns></returns>
        int AddQuestionReplyInfo(PiFQuestionReplyInfoModel newQuestionReplyInfo);

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
        List<PiFQuestionReplyInfoModel> GetReplyInfoList(out long countNumber,int questionId = 0, string likeContent = null, DateTime? startTime = null, DateTime? endTime = null, int userId = 0, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// add reply praised info
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="replyId">reply id</param>
        /// <returns></returns>
       int AddReplyPraised(int replyId, int userId);
    }
}
