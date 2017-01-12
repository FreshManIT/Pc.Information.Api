using System.Collections.Generic;
using System.Threading.Tasks;
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
        int AddUserInfo(PiFUsersModel newUserInfoModel);

        /// <summary>
        /// Update user info.
        /// </summary>
        /// <param name="newUserInfoModel"></param>
        /// <returns></returns>
        int UpdateUserInfo(PiFUsersModel newUserInfoModel);
    }
}
