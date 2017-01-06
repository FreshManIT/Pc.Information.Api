using System.Threading.Tasks;

namespace Pc.Information.Interface.IUserInfoBll
{
    /// <summary>
    /// Deal user info interface.
    /// </summary>
    public interface IUserInfoBll
    {
        /// <summary>
        /// Get user info by username and password.
        /// </summary>
        /// <param name="username">login user name</param>
        /// <param name="password">login for secret</param>
        /// <returns></returns>
        Task GetUserInfo(string username, string password);
    }
}
