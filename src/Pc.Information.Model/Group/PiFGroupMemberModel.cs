using System;

namespace Pc.Information.Model.Group
{
    /// <summary>
    /// group member model
    /// </summary>
    public class PiFGroupMemberModel
    {
        /// <summary>
        /// Construct function.
        /// </summary>
        public PiFGroupMemberModel()
        {
            PiFIsValid = 1;
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Group name
        /// </summary>
        public string PiFGroupNick { get; set; }
        /// <summary>
        /// Gourp id.
        /// </summary>
        public int PiFGroupId { get; set; }
        /// <summary>
        /// Is valid 1:yes (default);0:no.
        /// </summary>
        public int PiFIsValid { get; set; }
        /// <summary>
        /// Join group datetime.
        /// </summary>
        public DateTime PiFJoinTime { get; set; }
        /// <summary>
        /// User Id.
        /// </summary>
        public int PiFUserId { get; set; }
    }
}
