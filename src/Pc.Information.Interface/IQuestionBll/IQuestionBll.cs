using System;
using System.Collections.Generic;
using Pc.Information.Model.QuestionInfo;

namespace Pc.Information.Interface.IQuestionBll
{
    /// <summary>
    /// Interface of question bll.
    /// </summary>
    public interface IQuestionBll
    {
        /// <summary>
        /// Add new question info.
        /// </summary>
        /// <param name="newQuestionInfo"></param>
        /// <returns></returns>
        int AddQuestionInfo(PiFQuestionInfoModel newQuestionInfo);

        /// <summary>
        /// Updata question info.
        /// </summary>
        /// <param name="newQuestionInfo"></param>
        /// <returns></returns>
        int UpdateQuestionInfo(PiFQuestionInfoModel newQuestionInfo);

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
        List<PiFQuestionInfoModel> SearchQustionInfo(long id = 0, DateTime startTime = default(DateTime),
            DateTime endTime = default(DateTime), string title = null, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// Add question view data.
        /// </summary>
        /// <param name="questionId">questionId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        int AddQuestionViewInfo(int questionId, int userId);
    }
}
