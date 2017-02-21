using System.Collections.Generic;
using System.Linq;
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
            if (userId < 1) return null;
            var searchSql = string.Format("SELECT * from {0} where Id=@userId limit 1", DataTableGlobal.PiFUsers);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("userId", userId);
            var userList = sqlHelper.FindOne<PiFUsersModel>(searchSql, param);
            return userList;
        }

        /// <summary>
        /// Get hot user info list.
        /// </summary>
        /// <param name="number">need number.</param>
        /// <returns></returns>
        public List<HotUsersModel> GetHotUsersList(int number = 16)
        {
            if (number < 1) number = 16;
            var searchSql = string.Format(@"SELECT
    hotUserId.*, {0}.PiFUserName,
    {0}.PiFJob 
FROM
    (
        SELECT
            PiFFromId,
            COUNT(PiFFromId) ViewCount
        FROM
            {1}
        GROUP BY
            PiFFromId
        ORDER BY
            ViewCount DESC
        LIMIT 0,
        {2}
    ) AS hotUserId,
    {0}
WHERE
    hotUserId.PiFFromId = {0}.Id", DataTableGlobal.PiFUsers, DataTableGlobal.PiFinformationlog, number);
            var sqlHelper = new FreshSqlHelper();
            var userList = sqlHelper.FindToList<HotUsersModel>(searchSql, null);
            return userList.ToList();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        public int AddUserInfo(PiFUsersModel newUserInfo)
        {
            if (newUserInfo == null) return 0;
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

        /// <summary>
        /// update password
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns></returns>
        public int UpdateUserPassword(int userId, string oldPassword, string newPassword)
        {
            if (userId < 1 || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || oldPassword == newPassword) return 0;
            var searchSql = string.Format(@"UPDATE {0} 
SET PiFPassword =@newPassword 
WHERE 
	Id =@Id 
AND PiFPassword =@oldPassword", DataTableGlobal.PiFUsers);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("newPassword", newPassword, System.Data.DbType.String);
            param.Add("oldPassword", oldPassword, System.Data.DbType.String);
            param.Add("Id", userId, System.Data.DbType.Int32);
            var resulte = sqlHelper.ExcuteNonQuery(searchSql, param);
            return resulte;
        }

        /// <summary>
        /// Update user Activation.
        /// </summary>
        /// <returns></returns>
        public int UpdateUserEmailActivation(int userId)
        {
            if (userId < 1) return 0;
            var searchSql = string.Format(@"UPDATE {0} SET	PiFEmailActivation=1 where Id=@Id", DataTableGlobal.PiFUsers);
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("Id", userId, System.Data.DbType.Int32);
            var result = sqlHelper.ExcuteNonQuery(searchSql, param);
            return result;
        }
    }
}
