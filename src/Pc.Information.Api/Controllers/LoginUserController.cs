﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Model.User;
using Pc.Information.Model.BaseModel;
using System.Collections.Generic;
using FreshCommonUtility.DataConvert;
using FreshCommonUtility.CoreModel;

namespace Pc.Information.Api.Controllers
{
    /// <summary>
    /// Login user info controller.
    /// </summary>
    public class LoginUserController : BaseController
    {
        /// <summary>
        /// App Setting config info.
        /// </summary>
        private AppSettingsModel AppSettings { get; set; }

        /// <summary>
        /// Interface user info server.
        /// </summary>
        private IUserInfoBll UserInfoBll { get; set; }

        /// <summary>
        /// Constructed function.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="userInfoBll"></param>
        public LoginUserController(IOptions<AppSettingsModel> settings, IUserInfoBll userInfoBll)
        {
            AppSettings = settings.Value;
            UserInfoBll = userInfoBll;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("Login")]
        public ApiResultModel<PiFUsersModel> Login(string userName, string password)
        {
            var info = UserInfoBll.GetUserInfo(userName, password);
            return ResponseDataApi(info);
        }

        /// <summary>
        /// Get hot user info list.
        /// </summary>
        /// <param name="number">need number</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("GetHotUserInfoList")]
        public ApiResultModel<List<HotUsersModel>> GetHotUserInfoList(int number = 16)
        {
            var hotUserList = UserInfoBll.GetTopHotUserList(number);
            return ResponseDataApi(hotUserList);
        }

        /// <summary>
        /// Register user info.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="passWord">pass word</param>
        /// <param name="emailAddress">email address</param>
        /// <param name="sex">sex</param>
        /// <param name="birthday">birthday</param>
        /// <param name="ruleType">ruleType</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("RegisterUser")]
        public ApiResultModel<DataBaseModel> GetRegisterUser(string userName, string passWord, string emailAddress, int sex = 0, string birthday = null, int ruleType = 0)
        {
            var birthdayDate = DataTypeConvertHelper.ToDateTime(birthday);
            var newUser = new PiFUsersModel
            {
                PiFSex = sex,
                PiFUserName = userName,
                PiFPassword = passWord,
                PiFEmailAddress = emailAddress,
                PiFBirthday = birthdayDate,
                PiFRegisterTime = DateTime.Now,
                PiFRule = ruleType
            };
            var resulteInfo = UserInfoBll.AddUserInfo(newUser);
            return ResponseDataApi(resulteInfo);
        }

        /// <summary>
        /// Serch user info by user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("SearchUserInfoByUserId")]
        public ApiResultModel<PiFUsersModel> SearchUserInfoByUserId(int userId)
        {
            var userInfo = UserInfoBll.SearchUserInfoByUserId(userId);
            return ResponseDataApi(userInfo);
        }

        /// <summary>
        /// Update user info model
        /// </summary>
        /// <param name="newUserModel">new user info model</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("UpdateUserInfo")]
        public ApiResultModel<int> UpdateUserInfo(PiFUsersModel newUserModel)
        {
            var result = UserInfoBll.UpdateUserInfo(newUserModel);
            return ResponseDataApi(result);
        }

        /// <summary>
        /// update password
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("UpdateUserPassword")]
        public ApiResultModel<int> UpdateUserPassword(int userId, string oldPassword, string newPassword)
        {
            var result = UserInfoBll.UpdataUserPassword(userId, oldPassword, newPassword);
            return ResponseDataApi(result);
        }

        /// <summary>
        /// activation email
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("SendActivationEmail")]
        public ApiResultModel<DataBaseModel> SendActivationEmail(int userId)
        {
            var result = UserInfoBll.SendActivationEmail(userId);
            return ResponseDataApi(result);
        }

        /// <summary>
        /// ActivationEmail
        /// </summary>
        /// <param name="activationKey">activation key</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [Route("ActivationEmail")]
        public ApiResultModel<int> ActivationEmail(string activationKey)
        {
            var result = UserInfoBll.ActivationEmail(activationKey);
            return ResponseDataApi(result);
        }
    }
}
