using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace Pc.Information.Utility.FreshSqlHelper
{
    /// <summary>
    /// FreshSql helper.
    /// </summary>
    public class FreshSqlHelper
    {
        /// <summary>
        /// Init connection server from single connection object
        /// </summary>
        public FreshSqlHelper()
        {
            FreshSqlConnectionHelper.InitConnectionServer();
        }

        #region [1、ExcuteNonQuery get result of execute sql.]

        /// <summary>
        /// 增、删、改同步操作
        ///  </summary>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">链接字符串</param>
        /// <returns>int</returns>
        public int ExcuteNonQuery(string cmd, DynamicParameters param, bool flag = false, string connection = null)
        {
            int result;
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            result = con.Execute(cmd, param, null, null, flag ? CommandType.StoredProcedure : CommandType.Text);
            return result;
        }

        /// <summary>
        /// 增、删、改异步操作
        /// </summary>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">链接字符串</param>
        /// <returns>int</returns>
        public async Task<int> ExcuteNonQueryAsync(string cmd, DynamicParameters param, bool flag = false, string connection = null)
        {
            int result;
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            if (flag)
            {
                result = await con.ExecuteAsync(cmd, param, null, null, CommandType.StoredProcedure);
            }
            else
            {
                result = await con.ExecuteAsync(cmd, param, null, null, CommandType.Text);
            }
            return result;
        }
        #endregion

        #region [2、ExecuteScalar get single value sql result.]

        /// <summary>
        /// 同步查询操作
        /// </summary>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>The first cell selected</returns>
        public object ExecuteScalar(string cmd, DynamicParameters param, bool flag = false, string connection = null)
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            var result = con.ExecuteScalar(cmd, param, null, null, flag ? CommandType.StoredProcedure : CommandType.Text);
            return result;
        }

        /// <summary>
        /// 异步查询操作
        /// </summary>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>The first cell selected</returns>
        public async Task<object> ExecuteScalarAsync(string cmd, DynamicParameters param, bool flag = false, string connection = null)
        {
            object result;
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            if (flag)
            {
                result = await con.ExecuteScalarAsync(cmd, param, null, null, CommandType.StoredProcedure);
            }
            else
            {
                result = await con.ExecuteScalarAsync(cmd, param, null, null, CommandType.Text);
            }
            return result;
        }
        #endregion

        #region [3、FindOne get first one data.]
        /// <summary>
        /// 同步查询一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>t</returns>
        public T FindOne<T>(string cmd, DynamicParameters param, bool flag = false, string connection = null) where T : class, new()
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            var dataReader = con.QueryFirstOrDefault<T>(cmd, param, commandType: flag ? CommandType.StoredProcedure : CommandType.Text);
            return dataReader;
        }

        /// <summary>
        /// 异步查询一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>t</returns>
        public async Task<T> FindOneAsync<T>(string cmd, DynamicParameters param, bool flag = false, string connection = null) where T : class, new()
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            var dataReader = await con.QueryFirstOrDefaultAsync<T>(cmd, param, commandType: flag ? CommandType.StoredProcedure : CommandType.Text);
            return dataReader;
        }
        #endregion

        #region [4、FindToList  find data to list]
        /// <summary>
        /// 同步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>t</returns>
        public IList<T> FindToList<T>(string cmd, DynamicParameters param, bool flag = false, string connection = null) where T : class, new()
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            var dataReader = con.Query<T>(cmd, param, commandType: flag ? CommandType.StoredProcedure : CommandType.Text);
            return dataReader.ToList();
        }

        /// <summary>
        /// 异步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句(default)</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>t</returns>
        public async Task<IList<T>> FindToListAsync<T>(string cmd, DynamicParameters param, bool flag = false, string connection = null) where T : class, new()
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connection);
            var dataReader = await con.QueryAsync<T>(cmd, param, commandType: flag ? CommandType.StoredProcedure : CommandType.Text);
            return dataReader.ToList();
        }
        #endregion

        #region [5、Slow searchPage data]
        /// <summary>
        /// search page data,slowly.e.g:long sqlint;
        /// var param = new DynamicParameters();
        /// param.Add("id",1);
        /// var pagedata = fhelper.SearchPageList< PiFUsersModel />("pifusers", "and id=@id", null, "*", 0, 1, param, out sqlint);
        /// </summary>
        /// <param name="tbName">table name</param>
        /// <param name="strWhere">where case</param>
        /// <param name="orderBy">order field.</param>
        /// <param name="fieldList">search field</param>
        /// <param name="pageIndex">current page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="param">params.</param>
        /// <param name="allCount">all count number.</param>
        /// <param name="connectionstring">connection string.</param>
        /// <returns>page data</returns>
        public IList<T> SearchPageList<T>(string tbName, string strWhere, string orderBy, string fieldList, int pageIndex, int pageSize, DynamicParameters param, out long allCount, string connectionstring = null) where T : class, new()
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connectionstring);
            //Search count.
            var searchCountStr = $"SELECT COUNT(*) as dataCount from {tbName}  where 1=1 {strWhere}";
            var dataCount = con.ExecuteScalar(searchCountStr, param);
            allCount = (long)dataCount;
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            long startPageNum = (pageIndex - 1) * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append($" {fieldList} FROM {tbName} WHERE 1=1 {strWhere} ");
            sql.Append($" {orderBy} limit {startPageNum} ");
            sql.Append($",{pageSize} ");
            var tList = con.Query<T>(sql.ToString(), param, commandType: CommandType.Text);
            return tList.ToList();
        }

        /// <summary>
        /// search page data,slowly.e.g:long sqlint;
        /// var param = new DynamicParameters();
        /// param.Add("id",1);
        /// var pagedata = fhelper.SearchPageList< PiFUsersModel />("pifusers", "and id=@id", null, "*", 0, 1, param, out sqlint);
        /// </summary>
        /// <param name="tbName">table name</param>
        /// <param name="strWhere">where case</param>
        /// <param name="orderBy">order field.</param>
        /// <param name="fieldList">search field</param>
        /// <param name="pageIndex">current page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="param">params.</param>
        /// <param name="connectionstring">connection string.</param>
        /// <returns>page data</returns>
        public async Task<IList<T>> SearchPageListAsync<T>(string tbName, string strWhere, string orderBy, string fieldList, int pageIndex, int pageSize, DynamicParameters param, string connectionstring = null) where T : class, new()
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connectionstring);
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            long startPageNum = (pageIndex - 1) * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append($" {fieldList} FROM {tbName} WHERE 1=1 {strWhere} ");
            sql.Append($" {orderBy} limit {startPageNum} ");
            sql.Append($",{pageSize} ");
            var tList = await con.QueryAsync<T>(sql.ToString(), param, commandType: CommandType.Text);
            return tList.ToList();
        }

        /// <summary>
        /// search page data,high.e.g:long sqlint;
        /// var param = new DynamicParameters();
        /// param.Add("id",1);
        /// var pagedata = fhelper.SearchPageList< PiFUsersModel />("pifusers", "and id=@id", null, "*", 0, 1, param, out sqlint);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName">table name</param>
        /// <param name="strWhere">where case(begin and)</param>
        /// <param name="orderBy">order filed</param>
        /// <param name="fieldList">search field</param>
        /// <param name="primaryKey">primary key for imporove speed</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="allCount">all count data row</param>
        /// <param name="param">params</param>
        /// <param name="connectionstring">connection database string.</param>
        /// <returns></returns>
        public IList<T> SearchPageListHigh<T>(string tbName, string strWhere, string orderBy, string fieldList, string primaryKey, int pageIndex, int pageSize,out long allCount, DynamicParameters param, string connectionstring = null)
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connectionstring);
            //Search count.
            var searchCountStr = $"SELECT COUNT({primaryKey}) as dataCount from {tbName}  where 1=1 {strWhere}";
            var dataCount =con.ExecuteScalar(searchCountStr, param);
            allCount = (long)dataCount;
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            long startPageNum = (pageIndex - 1) * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append($" {fieldList} FROM {tbName} WHERE {primaryKey} < (SELECT {primaryKey} FROM {tbName} WHERE {strWhere} ORDER BY {primaryKey} desc LIMIT {startPageNum},1)");
            sql.Append($" {strWhere} ");
            sql.Append($" {orderBy} limit {pageSize} ");
            var tList = con.Query<T>(sql.ToString(), param, commandType: CommandType.Text);
            return tList.ToList();
        }

        /// <summary>
        /// Async search page data,high.e.g:long sqlint;
        /// var param = new DynamicParameters();
        /// param.Add("id",1);
        /// var pagedata = fhelper.SearchPageList< PiFUsersModel />("pifusers", "and id=@id", null, "*", 0, 1, param, out sqlint);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName">table name</param>
        /// <param name="strWhere">where case(begin and)</param>
        /// <param name="orderBy">order filed</param>
        /// <param name="fieldList">search field</param>
        /// <param name="primaryKey">primary key for imporove speed</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <param name="allCount">all count data row</param>
        /// <param name="param">params</param>
        /// <param name="connectionstring">connection database string.</param>
        /// <returns></returns>
        public async Task<IList<T>> SearchPageListHighAsync<T>(string tbName, string strWhere, string orderBy, string fieldList, string primaryKey, int pageIndex, int pageSize, DynamicParameters param, string connectionstring = null)
        {
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connectionstring);
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            long startPageNum = (pageIndex - 1) * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append($" {fieldList} FROM {tbName} WHERE {primaryKey} < (SELECT {primaryKey} FROM {tbName} WHERE {strWhere} ORDER BY {primaryKey} desc LIMIT {startPageNum},1)");
            sql.Append($" {strWhere} ");
            sql.Append($" {orderBy} limit {pageSize} ");
            var tList =await con.QueryAsync<T>(sql.ToString(), param, commandType: CommandType.Text);
            return tList.ToList();
        }
        #endregion
    }
}
