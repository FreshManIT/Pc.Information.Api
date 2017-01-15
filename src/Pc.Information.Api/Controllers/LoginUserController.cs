using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Model.User;
using Pc.Information.CoreModel;

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
        [Route("Login")]
        public ApiResultModel<PiFUsersModel> Login(string userName, string password)
        {
            var info = UserInfoBll.GetUserInfo(userName, password);
            var newUseInfo = new PiFUsersModel { PiFBirthday = new System.DateTime(1994, 5, 3), PiFEmailAddress = "langyuelei@163.com", PiFJob = "前端开发助理", PiFPassword = "langyuelei", PiFRegisterTime = System.DateTime.Now, PiFRule = 1, PiFSex = 1, PiFUserName = "langyuelei" };
            var userId = UserInfoBll.UpdateUserInfo(newUseInfo);
            return ResponseDataApi(info);
        }

        /// <summary>
        /// Get user info by userid
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInfo")]
        public string GetInfo()
        {
            return "Freshman";
        }
    }
}
