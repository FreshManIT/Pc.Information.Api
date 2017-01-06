using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pc.Information.Api.MiddleWares;
using Pc.Information.Model.Config;
using Pc.Information.Interface.IUserInfoBll;


namespace Pc.Information.Api.Controllers
{
    /// <summary>
    /// Login user info controller.
    /// </summary>
    public class LoginUserController:BaseController
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
        public LoginUserController(IOptions<AppSettingsModel> settings,IUserInfoBll userInfoBll)
        {
            AppSettings = settings.Value;
            UserInfoBll = userInfoBll;
        }

        [HttpGet]
        public UserInfo GetUserInfo()
        {
            var para = WebRequestHelperMiddleWare.GetStringFromParameters("fresh",HttpContext);
            var Info = UserInfoBll.GetUserInfo(string.Empty,string.Empty);
            return new UserInfo();
        }

        [Route("GetInfo")]
        public string GetInfo()
        {
            return "Freshman";
        }
    }

    public class UserInfo
    {
        public int age = 1;
        public string name = "Freshman";
    }
}
