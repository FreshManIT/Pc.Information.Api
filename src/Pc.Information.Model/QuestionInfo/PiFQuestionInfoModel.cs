using System;

namespace Pc.Information.Model.QuestionInfo
{
    /// <summary>
    /// Question info model.
    /// </summary>
    public class PiFQuestionInfoModel
    {
        /// <summary>
        /// Question id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Question title
        /// </summary>
        public string PiFQuestionTitle { get; set; }
        /// <summary>
        /// Question content
        /// </summary>
        public string PiFQuestionContent { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        public DateTime PiFCreateTime { get; set; }
        /// <summary>
        /// Creat question uer id
        /// </summary>
        public int PiFSendUserId { get; set; }
    }
}
