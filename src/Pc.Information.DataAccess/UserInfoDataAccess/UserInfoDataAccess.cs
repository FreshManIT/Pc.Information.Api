using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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
            long sqlint;
            var param=new DynamicParameters();
            param.Add("id",1);
            var pagedata=fhelper.SearchPageList<PiFUsersModel>("pifusers", "and id=@id", null,"*",0, 1, param, out sqlint);
            return userElist.ToList();
        }
    }
}
