using System.Collections.Generic;
using Pc.Information.DataAccess.UserInfoDataAccess;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Model.BaseModel;
using Pc.Information.Model.User;
using Pc.Information.Utility.Email;
using Pc.Information.Utility.Security;

namespace Pc.Information.Business.UserInfoBll
{
    /// <summary>
    /// User info business deal.
    /// </summary>
    public class UserInfoBll : IUserInfoBll
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
            if (string.IsNullOrEmpty(password)) return null;
            password = DesHelper.DesEnCode(password);
            var info = _userInfoDataAccess.GetUserInfo(username, password);
            return info;
        }

        /// <summary>
        /// Add new use info.
        /// </summary>
        /// <param name="newUserInfoModel"></param>
        /// <returns></returns>
        public DataBaseModel AddUserInfo(PiFUsersModel newUserInfoModel)
        {
            var addResult = new DataBaseModel { StateCode = "0000" };
            if (string.IsNullOrEmpty(newUserInfoModel?.PiFUserName) || string.IsNullOrEmpty(newUserInfoModel.PiFPassword) || !EmailHelper.IsEmailAddress(newUserInfoModel.PiFEmailAddress))
            {
                addResult.StateCode = "0001";
                addResult.StateDesc = "Add info is Error";
                return addResult;
            }
            newUserInfoModel.PiFPassword = DesHelper.DesEnCode(newUserInfoModel.PiFPassword);
            var oldUseInfo = SearchUserInfoByUserName(newUserInfoModel.PiFUserName);
            if (oldUseInfo != null)
            {
                addResult.StateCode = "0001";
                addResult.StateDesc = "This username has being used.";
                return addResult;
            }
            var userId = _userInfoDataAccess.AddUserInfo(newUserInfoModel);
            addResult.StateCode = "0000";
            addResult.StateDesc = userId.ToString();
            EmailHelper.SendEmailAsync(newUserInfoModel.PiFEmailAddress, "New user register info.", "Thanks for you register FreshManChatSystem VIP.The next time we will push some interesting and useful info for you.");
            return addResult;
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

        /// <summary>
        /// Get hot user list.
        /// </summary>
        /// <param name="number">need number.</param>
        /// <returns></returns>
        public List<HotUsersModel> GetTopHotUserList(int number = 16)
        {
            return new UserInfoDataAccess().GetHotUsersList(number);
        }

        /// <summary>
        /// Search user info by user name.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>user info.</returns>
        private PiFUsersModel SearchUserInfoByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;
            var userInfo = _userInfoDataAccess.GetUserInfoByUserName(userName);
            return userInfo;
        }
    }
}
