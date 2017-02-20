using System.Collections.Generic;
using Pc.Information.Model.InformationLog;
using Pc.Information.Model.User;

namespace Pc.Information.Interface.ILogHistoryBll
{
    /// <summary>
    /// Deal user chat info.
    /// </summary>
    public interface IInformationHistoryBll
    {
        /// <summary>
        /// Add chat info
        /// </summary>
        /// <param name="chatModel">chat information history model.</param>
        /// <returns></returns>
        long AddChatInfo(PiFInformationLogModel chatModel);

        /// <summary>
        /// Add new online user nodel.
        /// </summary>
        /// <param name="onlineModel">new user info</param>
        /// <returns></returns>
        long AddOnlineUser(OnlineUserModel onlineModel);

        /// <summary>
        /// Remove off line user info
        /// </summary>
        /// <param name="removeUserModel">need remove user model.</param>
        /// <returns>the number of removed elements</returns>
        long RemoveOnlineUser(OnlineUserModel removeUserModel);

        /// <summary>
        /// Get list range info.
        /// </summary>
        /// <param name="ruleType">rule type:1:usually user;2:teacher user.</param>
        /// <param name="start">start index,begin is zore.</param>
        /// <param name="fail">the fail of list.</param>
        /// <returns>rule type online user list</returns>
        List<OnlineUserModel> GetAllOnlineUserModels(int ruleType = -1, long start = 0, long fail = -1);
    }
}
