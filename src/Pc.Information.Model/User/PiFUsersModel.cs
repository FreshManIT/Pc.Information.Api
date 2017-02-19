using System;

namespace Pc.Information.Model.User
{
    /// <summary>
    /// user model.
    /// </summary>
    public class PiFUsersModel
    {
        /// <summary>
        /// User id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User sex（0：female(default)；1：male；2：other）
        /// </summary>
        public int PiFSex { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string PiFUserName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string PiFPassword { get; set; }
        /// <summary>
        /// Rule（0：usually；1：usually service；2：manager）
        /// </summary>
        public int PiFRule { get; set; }
        /// <summary>
        /// Job type
        /// </summary>
        public string PiFJob { get; set; }
        /// <summary>
        /// Email address.
        /// </summary>
        public string PiFEmailAddress { get; set; }
        /// <summary>
        /// Birthday.
        /// </summary>
        public DateTime PiFBirthday { get; set; }
        /// <summary>
        /// Register time.
        /// </summary>
        public DateTime PiFRegisterTime { get; set; }
    }

    /// <summary>
    /// hot user model
    /// </summary>
    public class HotUsersModel
    {
        /// <summary>
        /// User id
        /// </summary>
        public int PiFFromId { get; set; }

        /// <summary>
        /// Hot view count
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string PiFUserName { get; set; }

        /// <summary>
        /// jobname
        /// </summary>
        public string PiFJob { get; set; }
    }
}
