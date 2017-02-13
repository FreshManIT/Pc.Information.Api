using System;
using System.Collections.Generic;
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
            return new QuestionDataAccess().SearchQustionInfo(id, startTime, endTime, title, pageIndex, pageSize);
        }
    }
}
