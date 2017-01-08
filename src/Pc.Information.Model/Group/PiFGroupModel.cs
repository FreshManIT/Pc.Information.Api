using System;

namespace Pc.Information.Model.Group
{
    /// <summary>
    /// Group info model.
    /// </summary>
    public class PiFGroupModel
    {
        /// <summary>
        /// Group id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Group nick name.
        /// </summary>
        public string PiFGroupNick { get; set; }
        /// <summary>
        /// Group creat time.
        /// </summary>
        public DateTime PiFCreateTime { get; set; }
        /// <summary>
        /// Group topic.
        /// </summary>
        public string PiFGroupTopic { get; set; }
        /// <summary>
        /// Is valid 1:yes (default);0:no
        /// </summary>
        public int PiFIsValid { get; set; }
        /// <summary>
        /// Group count.
        /// </summary>
        public int PiFGroupCount { get; set; }
        /// <summary>
        /// Group belong user id.
        /// </summary>
        public int PiFGroupBelongId { get; set; }
    }
}
