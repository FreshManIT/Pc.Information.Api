using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
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

        #region [1、ExcuteNonQuery 增、删、改同步操作]

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
        #endregion

        #region [2、ExcuteNonQueryAsync 增、删、改异步操作]

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

        #region [3、ExecuteScalar 同步查询操作]

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
        #endregion

        #region [4、ExecuteScalarAsync 异步查询操作]

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
                result =await con.ExecuteScalarAsync(cmd, param, null, null, CommandType.Text);
            }
            return result;
        }
        #endregion

        #region [5、FindOne  同步查询一条数据]
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
            IDataReader dataReader = con.ExecuteReader(cmd, param, null, null, flag ? CommandType.StoredProcedure : CommandType.Text);
            if (dataReader == null) return null;
            Type type = typeof(T);
            T t = new T();
            foreach (var item in type.GetProperties())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    //属性名与查询出来的列名比较
                    if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                    var kvalue = dataReader[item.Name];
                    if (kvalue == DBNull.Value) continue;
                    item.SetValue(t, kvalue, null);
                    break;
                }
            }
            con.Close();
            return t;
        }
        #endregion

        #region [6、FindOne  异步查询一条数据]
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
            IDataReader dataReader;
            if (flag)
            {
                dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.StoredProcedure);
            }
            else
            {
                dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.Text);
            }
            if (dataReader == null) return null;
            Type type = typeof(T);
            T t = new T();
            foreach (var item in type.GetProperties())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    //属性名与查询出来的列名比较
                    if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                    var kvalue = dataReader[item.Name];
                    if (kvalue == DBNull.Value) continue;
                    item.SetValue(t, kvalue, null);
                    break;
                }
            }
            con.Close();
            return t;
        }
        #endregion

        #region [7、FindToList  同步查询数据集合]
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
            IDataReader dataReader = con.ExecuteReader(cmd, param, null, null, flag ? CommandType.StoredProcedure : CommandType.Text);
            if (dataReader == null) return null;
            Type type = typeof(T);
            List<T> tlist = new List<T>();
            while (dataReader.Read())
            {
                T t = new T();
                foreach (var item in type.GetProperties())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        //属性名与查询出来的列名比较
                        if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                        var kvalue = dataReader[item.Name];
                        if (kvalue == DBNull.Value) continue;
                        item.SetValue(t, kvalue, null);
                        break;
                    }
                }
                tlist.Add(t);
            }
            con.Close();
            return tlist;
        }
        #endregion

        #region [8、FindToListAsync  异步查询数据集合]
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
            IDataReader dataReader;
            if (flag)
            {
                dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.StoredProcedure);
            }
            else
            {
                dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.Text);
            }
            if (dataReader == null) return null;
            Type type = typeof(T);
            List<T> tlist = new List<T>();
            while (dataReader.Read())
            {
                T t = new T();
                foreach (var item in type.GetProperties())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        //属性名与查询出来的列名比较
                        if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                        var kvalue = dataReader[item.Name];
                        if (kvalue == DBNull.Value) continue;
                        item.SetValue(t, kvalue, null);
                        break;
                    }
                }
                tlist.Add(t);
            }
            con.Close();
            return tlist;
        }
        #endregion

        /// <summary>
        /// MySQL分页
        /// </summary>
        /// <param name="tbName">table name</param>
        /// <param name="strWhere">where case</param>
        /// <param name="orderBy">order field.</param>
        /// <param name="fieldList">search field</param>
        /// <param name="pageIndex">current page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="allCount">all count number.</param>
        /// <param name="connectionstring">connection string.</param>
        /// <returns></returns>
        public void GetCommPaddMySql(string tbName, string strWhere, string orderBy, string fieldList, int pageIndex, int pageSize, out int allCount,string connectionstring=null)
        {
            allCount = 0;
            MySqlConnection con = FreshSqlConnectionHelper.GetConnection(connectionstring);
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            long startPageNum = (pageIndex - 1) * pageSize;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT SQL_NO_CACHE SQL_CALC_FOUND_ROWS ");
            sql.Append($" {fieldList} FROM {tbName} WHERE {strWhere} ");
            sql.Append($" {orderBy} limit {startPageNum} ");
            sql.Append($",{pageSize} ");
            sql.Append(" ;SELECT FOUND_ROWS(); ");
            var ds = con.ExecuteReader(@"SELECT SQL_CALC_FOUND_ROWS
	*
FROM 
	world.city 
WHERE 
	id <= ( 
		SELECT 
			id 
		FROM 
			world.city  
WHERE CountryCode='CHN' 
		ORDER BY 
			id DESC 
		LIMIT 20,1 
	) 
ORDER BY 
	id DESC 
LIMIT 20;

select FOUND_ROWS()");
            while (ds.Read())
            {
                    for (int i = 0; i < ds.FieldCount; i++)
                    {
                        //属性名与查询出来的列名比较
                        var name = ds.GetName(i).ToLower();
                        var value = ds[name];

                    }
            }
        }
    }
}
