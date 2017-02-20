using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Model.User;
using Pc.Information.CoreModel;
using Pc.Information.Model.BaseModel;
using Pc.Information.Utility.DataConvert;
using System.Collections.Generic;

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
            var userInfo= UserInfoBll.SearchUserInfoByUserId(userId);
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
    }
}
