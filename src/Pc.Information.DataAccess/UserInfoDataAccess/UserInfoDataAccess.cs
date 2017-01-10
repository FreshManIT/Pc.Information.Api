using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pc.Information.Model.User;
using Pc.Information.Utility.FreshSqlHelper;

namespace Pc.Information.DataAccess.UserInfoDataAccess
{
    public class UserInfoDataAccess
    {
        public IList<PiFUsersModel> GetUserInfo()
        {
            var sql = "select * FROM pifusers";
            var fhelper = new FreshSqlHelper();
            var userElist = fhelper.FindToList<PiFUsersModel>(sql, null, false);
            int sqlint;
            fhelper.GetCommPaddMySql(null, null, null, null, 0, 1, out sqlint);
            return userElist.ToList();
        }
    }
}
