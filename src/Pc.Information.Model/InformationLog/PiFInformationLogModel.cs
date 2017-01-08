using System;

namespace Pc.Information.Model.InformationLog
{
    /// <summary>
    /// information log history model.
    /// </summary>
    public class PiFInformationLogModel
    {
        /// <summary>
        /// Log id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Send information user id
        /// </summary>
        public int PiFFromId { get; set; }
        /// <summary>
        /// Get information user id
        /// </summary>
        public int PiFToId { get; set; }
        /// <summary>
        /// if the message is send to group,group id.
        /// </summary>
        public int PiFToGroupId { get; set; }
        /// <summary>
        /// Send message type(0:txt(default);1:image/photos)
        /// </summary>
        public int PiFContentType { get; set; }
        /// <summary>
        /// Send message content,txt,image base 64 code.
        /// </summary>
        public string PiFContent { get; set; }
        /// <summary>
        /// Send time.
        /// </summary>
        public DateTime PiFSendTime { get; set; }
    }
}
