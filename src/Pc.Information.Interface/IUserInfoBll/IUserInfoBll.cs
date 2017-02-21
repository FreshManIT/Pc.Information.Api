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

        /// <summary>
        /// Serch user info by user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        PiFUsersModel SearchUserInfoByUserId(int userId);

        /// <summary>
        /// update password
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns></returns>
        int UpdataUserPassword(int userId, string oldPassword, string newPassword);

        /// <summary>
        /// activation email
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        DataBaseModel SendActivationEmail(int userId);

        /// <summary>
        /// ActivationEmail
        /// </summary>
        /// <param name="activationKey">activation key</param>
        /// <returns></returns>
        int ActivationEmail(string activationKey);
    }
}
