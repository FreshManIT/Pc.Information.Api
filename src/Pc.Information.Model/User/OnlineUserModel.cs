namespace Pc.Information.Model.User
{
    /// <summary>
    /// Online user model
    /// </summary>
    public class OnlineUserModel
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Rule type:1:usually user;2:teacher user.
        /// </summary>
        public int RuleType { get; set; }
    }
}
