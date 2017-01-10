using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Utility.Web;
using Pc.Information.Model.User;
using Pc.Information.Utility.Cache;
using Pc.Information.CoreModel;
using System.Collections.Generic;
using System.Linq;

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
        public List<PiFUsersModel> GetUserInfo()
        {
            var Info = UserInfoBll.GetUserInfo();
            return Info.ToList();
        }

        [Route("GetInfo")]
        public string GetInfo()
        {
            return "Freshman";
        }
    }
}
