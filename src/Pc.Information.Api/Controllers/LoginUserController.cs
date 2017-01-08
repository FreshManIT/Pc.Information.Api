using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pc.Information.Model.Config;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Utility.Web;
using Pc.Information.Model.User;
using Pc.Information.Api.MiddleWares;
using Pc.Information.Utility.Configure;
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
        public PiFUsersModel GetUserInfo()
        {
            var k=AppConfigurationHelper.GetAppSettings("MySqlConnectionString");
            var j = UserInfoBll.GetUserInfo();
            var t = j.ToList();
            var s = AppConfigurationHelper.GetAppSettings<AppSettingsModel>("AppSettings");
            var para = HttpContext.GetStringFromParameters("fresh");
            var Info = UserInfoBll.GetUserInfo(string.Empty,string.Empty);
            return new PiFUsersModel();
        }

        [Route("GetInfo")]
        public string GetInfo()
        {
            return "Freshman";
        }
    }
}
