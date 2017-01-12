using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pc.Information.DataAccess.UserInfoDataAccess;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Model.User;

namespace Pc.Information.Business.UserInfoBll
{
    /// <summary>
    /// User info business deal.
    /// </summary>
    public class UserInfoBll: IUserInfoBll
    {
        /// <summary>
        /// 数据访问对象
        /// </summary>
        readonly UserInfoDataAccess _userInfoDataAccess = new UserInfoDataAccess();

        /// <summary>
        /// Get user info by username and password.
        /// </summary>
        /// <param name="username">login user name</param>
        /// <param name="password">login for secret</param>
        /// <returns></returns>
        public PiFUsersModel GetUserInfo(string username, string password)
        {
            var info = _userInfoDataAccess.GetUserInfo(username, password);
            return info;
        }

        /// <summary>
        /// Add new use info.
        /// </summary>
        /// <param name="newUserInfoModel"></param>
        /// <returns></returns>
        public int AddUserInfo(PiFUsersModel newUserInfoModel)
        {
            if (newUserInfoModel == null) return 0;
            var userId = _userInfoDataAccess.AddUserInfo(newUserInfoModel);
            return userId;
        }

        /// <summary>
        /// Update user info.
        /// </summary>
        /// <param name="newUserInfoModel"></param>
        /// <returns></returns>
        public int UpdateUserInfo(PiFUsersModel newUserInfoModel)
        {
            if (newUserInfoModel == null) return 0;
            var userId = _userInfoDataAccess.UpdateUserInfo(newUserInfoModel);
            return userId;
        }
    }
}
