using Dapper;
using Pc.Information.DataAccess.Common;
using Pc.Information.Model.User;
using Pc.Information.Utility.FreshSqlHelper;

namespace Pc.Information.DataAccess.UserInfoDataAccess
{
    /// <summary>
    /// User info data access
    /// </summary>
    public class UserInfoDataAccess
    {
        /// <summary>
        /// search user info.
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public PiFUsersModel GetUserInfo(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) return null;
            var searchSql = string.Format("SELECT * from {0} where PiFUserName=@PiFUserName and PiFPassword=@PiFPassword limit 1", DataTableGlobal.PiFUsers);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("PiFUserName", userName);
            param.Add("PiFPassword", password);
            var userList = sqlHelper.FindOne<PiFUsersModel>(searchSql, param);
            return userList;
        }

        /// <summary>
        /// search user info by username.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public PiFUsersModel GetUserInfoByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;
            var searchSql = string.Format("SELECT * from {0} where PiFUserName=@PiFUserName limit 1", DataTableGlobal.PiFUsers);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("PiFUserName", userName);
            var userList = sqlHelper.FindOne<PiFUsersModel>(searchSql, param);
            return userList;
        }

        /// <summary>
        /// search user info by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PiFUsersModel GetUserInfoByUserId(int userId)
        {
            if (userId<1) return null;
            var searchSql = string.Format("SELECT * from {0} where Id=@userId limit 1", DataTableGlobal.PiFUsers);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("userId", userId);
            var userList = sqlHelper.FindOne<PiFUsersModel>(searchSql, param);
            return userList;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        public int AddUserInfo(PiFUsersModel newUserInfo)
        {
                if (newUserInfo==null) return 0;
                var searchSql = string.Format(@"INSERT INTO {0} (
	PiFSex,
	PiFUserName,
	PiFPassword,
	PiFRule,
	PiFJob,
	PiFEmailAddress,
	PiFBirthday,
	PiFRegisterTime
)
VALUES
	(
		@PiFSex,
	@PiFUserName,
	@PiFPassword,
	@PiFRule,
	@PiFJob,
	@PiFEmailAddress,
	@PiFBirthday,
	@PiFRegisterTime
	)", DataTableGlobal.PiFUsers);
                var sqlHelper = new FreshSqlHelper();
                var param = new DynamicParameters(newUserInfo);
                var userId = sqlHelper.ExcuteNonQuery(searchSql, param);
                return userId;
        }

        /// <summary>
        /// Update user info.
        /// </summary>
        /// <param name="newuserInfo"></param>
        /// <returns></returns>
        public int UpdateUserInfo(PiFUsersModel newuserInfo)
        {
            if (newuserInfo == null) return 0;
            var searchSql = string.Format(@"UPDATE {0} SET	PiFSex=@PiFSex,
	PiFRule=@PiFRule,
	PiFJob=@PiFJob,
	PiFEmailAddress=@PiFEmailAddress,
	PiFBirthday=@PiFBirthday 
where PiFUserName=@PiFUserName and PiFPassword=@PiFPassword", DataTableGlobal.PiFUsers);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters(newuserInfo);
            var userId = sqlHelper.ExcuteNonQuery(searchSql, param);
            return userId;
        }
    }
}
