using System.Collections.Generic;
using Pc.Information.Model.BaseModel;
using Pc.Information.Model.User;

namespace Pc.Information.Interface.IUserInfoBll
{
    /// <summary>
    /// Deal user info interface.
    /// </summary>
    public interface IUserInfoBll
    {
        /// <summary>
        /// Get user info by username and password.
        /// </summary>
        /// <param name="username">login user name</param>
        /// <param name="password">login for secret</param>
        /// <returns></returns>
        PiFUsersModel GetUserInfo(string username, string password);

        /// <summary>
        /// Add new user info users.
        /// </summary>
        /// <param name="newUserInfoModel"></param>
        /// <returns></returns>
        DataBaseModel AddUserInfo(PiFUsersModel newUserInfoModel);

        /// <summary>
        /// Update user info.
        /// </summary>
        /// <param name="newUserInfoModel"></param>
        /// <returns></returns>
        int UpdateUserInfo(PiFUsersModel newUserInfoModel);

        /// <summary>
        /// Get hot user list.
        /// </summary>
        /// <param name="number">need number.</param>
        /// <returns></returns>
        List<HotUsersModel> GetTopHotUserList(int number = 16);
    }
}
