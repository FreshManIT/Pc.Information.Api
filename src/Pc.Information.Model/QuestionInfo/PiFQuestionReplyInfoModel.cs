using System;

namespace Pc.Information.Model.QuestionInfo
{
    /// <summary>
    /// Question reply model.
    /// </summary>
    public class PiFQuestionReplyInfoModel
    {
        /// <summary>
        /// Reply id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Reply for question id.
        /// </summary>
        public int PiFQuestionId { get; set; }
        /// <summary>
        /// Reply content info.
        /// </summary>
        public string PiFReplyContent { get; set; }
        /// <summary>
        /// Is best reply.1:yes;0:no(default)
        /// </summary>
        public int PiFReplyIsBest { get; set; }
        /// <summary>
        /// Reply time.
        /// </summary>
        public DateTime PiFReplyTime { get; set; }
        /// <summary>
        /// Reply user id.
        /// </summary>
        public int PiFReplyUserId { get; set; }
    }
}
