using System;
using System.Collections.Generic;
using FreshCommonUtility.Cache;
using FreshCommonUtility.Email;
using FreshCommonUtility.Security;
using Pc.Information.Business.LogHistoryBll;
using Pc.Information.Model.BaseModel;
using Pc.Information.Model.User;
using Pc.Information.DataAccess.UserInfoDataAccess;
using Pc.Information.Interface.IUserInfoBll;

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
            if (!string.IsNullOrEmpty(newUserInfoModel.PiFPassword))
            {
                newUserInfoModel.PiFPassword = DesHelper.DesEnCode(newUserInfoModel.PiFPassword);
            }
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

        /// <summary>
        /// Serch user info by user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        public PiFUsersModel SearchUserInfoByUserId(int userId)
        {
            if (userId < 1) return null;
            var userInfo = _userInfoDataAccess.GetUserInfoByUserId(userId);
            return userInfo;
        }

        /// <summary>
        /// update password
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns></returns>
        public int UpdataUserPassword(int userId, string oldPassword, string newPassword)
        {
            if (userId < 1 || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || oldPassword == newPassword) return 0;
            oldPassword = DesHelper.DesEnCode(oldPassword);
            newPassword = DesHelper.DesEnCode(newPassword);
            var result = _userInfoDataAccess.UpdateUserPassword(userId, oldPassword, newPassword);
            return result;
        }

        /// <summary>
        /// activation email
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        public DataBaseModel SendActivationEmail(int userId)
        {
            var resultModel = new DataBaseModel { StateCode = "0000", StateDesc = "Success." };
            var userInfo = _userInfoDataAccess.GetUserInfoByUserId(userId);
            if (userInfo == null)
            {
                resultModel.StateCode = "1000";
                resultModel.StateDesc = "User is invalid";
                return resultModel;
            }
            if (string.IsNullOrEmpty(userInfo.PiFEmailAddress) || !EmailHelper.IsEmailAddress(userInfo.PiFEmailAddress))
            {
                resultModel.StateCode = "1000";
                resultModel.StateDesc = "Emial Address is invalid.";
                return resultModel;
            }
            var jsonKey = Guid.NewGuid().ToString();
            var activationModel = new UserEmailActivationModel { EndTime = DateTime.Now.AddMinutes(30), GuId = jsonKey, UserId = userId };
            var endTime = new TimeSpan(0, 30, 0);
            var isSave = RedisCacheHelper.AddSet(jsonKey, activationModel, endTime);
            if (!isSave)
            {
                var logBll = new ErrorLogBll();
                logBll.WriteWarningInfo("Redis cache is save fail,please check redis server.");
                resultModel.StateCode = "1000";
                resultModel.StateDesc = "Redis cache is save fail,please check redis server.";
                return resultModel;
            }
            var message = string.Format("<div class=\"wui-FileReadList paraStyle\" style=\"background-color:#fff\"><div class=\"text\"><div class=\"txt\"><div class=\"mailMainArea\" style=\"font-size:14px;font-family:Verdana,&quot;宋体&quot;,Helvetica,sans-serif;line-height:1.66;padding:8px 10px;margin:0;overflow:auto\"><div class=\"\"><a target =\"_blank\" class=\"\" href='http://localhost:3000/home' _act=\"check_domail\"><b>FM 信息咨询系统</b></a></div><br style =\"clear:both; height:0\"><div class=\"\" style=\"background: none repeat scroll 0 0 #FFFFFF; border: 1px solid #E9E9E9; margin: 2px 0 0; padding: 30px;\"><p>您好: </p><p>这是您在 FM 信息咨询系统 上的重要邮件, 功能是进行 FM 信息咨询系统 帐户邮箱验证, 请点击下面的连接完成验证</p><p style =\"border-top: 1px solid #DDDDDD;margin: 15px 0 25px;padding: 15px;\">请点击链接继续: <a target =\"_blank\" href='{0}' _act=\"check_domail\">{0}</a></p><p></p><p class=\"\" style=\"border-top: 1px solid #DDDDDD; padding-top:6px; margin-top:25px; color:#838383;\">请勿回复本邮件, 此邮箱未受监控, 您不会得到任何回复. 要获得帮助, 请登录网站 | <a target=\"_blank\" href=\"http://localhost:3000/set/#info\" _act=\"check_domail\">修改通知设置</a><br><br><a target=\"_blank\" href='http://localhost:3000/set/#info' _act=\"check_domail\">FM 信息咨询系统</a></p></div></div>", "http://localhost:3000/activation?key=" + jsonKey);
            EmailHelper.SendEmail(userInfo.PiFEmailAddress, "FM 信息咨询系统 帐户邮箱验证", message, "FM 信息咨询系统 帐户邮箱验证", true);
            return resultModel;
        }

        /// <summary>
        /// ActivationEmail
        /// </summary>
        /// <param name="activationKey">activation key</param>
        /// <returns></returns>
        public int ActivationEmail(string activationKey)
        {
            if (string.IsNullOrEmpty(activationKey)) return 0;
            var activationModel = RedisCacheHelper.Get<UserEmailActivationModel>(activationKey);
            if (activationModel == null || activationModel.UserId < 1 || activationModel.EndTime < DateTime.Now) return -1;
            return _userInfoDataAccess.UpdateUserEmailActivation(activationModel.UserId);
        }
    }
}
