using Dapper;
using Pc.Information.Model.User;
using Pc.Information.Utility.FreshSqlHelper;

namespace Pc.Information.DataAccess.UserInfoDataAccess
{
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
            var searchSql = "SELECT * from pifusers where PiFUserName=@PiFUserName and PiFPassword=@PiFPassword limit 1";
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
            var searchSql = "SELECT * from pifusers where PiFUserName=@PiFUserName limit 1";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("PiFUserName", userName);
            var userList = sqlHelper.FindOne<PiFUsersModel>(searchSql, param);
            return userList;
        }

        /// <summary>
        /// search user info by username.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PiFUsersModel GetUserInfoByUserId(int userId)
        {
            if (userId<1) return null;
            var searchSql = "SELECT * from pifusers where Id=@userId limit 1";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters();
            param.Add("userId", userId);
            var userList = sqlHelper.FindOne<PiFUsersModel>(searchSql, param);
            return userList;
        }

        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        public int AddUserInfo(PiFUsersModel newUserInfo)
        {
                if (newUserInfo==null) return 0;
                var searchSql = @"INSERT INTO pifusers (
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
	)";
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
            var searchSql = @"UPDATE pifusers SET	PiFSex=@PiFSex,
	PiFRule=@PiFRule,
	PiFJob=@PiFJob,
	PiFEmailAddress=@PiFEmailAddress,
	PiFBirthday=@PiFBirthday 
where PiFUserName=@PiFUserName and PiFPassword=@PiFPassword";
            var sqlHelper = new FreshSqlHelper();
            var param = new DynamicParameters(newuserInfo);
            var userId = sqlHelper.ExcuteNonQuery(searchSql, param);
            return userId;
        }
    }
}
