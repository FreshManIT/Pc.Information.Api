using System.Threading.Tasks;
using Pc.Information.DataAccess.FreshSqlHelper;
using Pc.Information.Interface.IUserInfoBll;

namespace Pc.Information.Business.UserInfoBll
{
    /// <summary>
    /// User info business deal.
    /// </summary>
    public class UserInfoBll: IUserInfoBll
    {
        /// <summary>
        /// Test MySqlhelper method.
        /// </summary>
        public void GetUserInfo()
        {
            FreshSqlHelper.tst();
        }

        /// <summary>
        /// Get user info by username and password.
        /// </summary>
        /// <param name="username">login user name</param>
        /// <param name="password">login for secret</param>
        /// <returns></returns>
        public Task GetUserInfo(string username, string password)
        {
            var info = Task.Factory.StartNew(GetUserInfo);
            return info;
        }
    }
}
