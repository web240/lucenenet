using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DotNet.SQLServer.DataAccess
{

    public class EntityConvertor
    {
        #region 查询实体生成

        /// <summary>
        /// 反射：指定控制绑定和由反射执行的成员和类型搜索方法的标志
        /// </summary>
        private static BindingFlags bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;

        /// <summary>
        /// 查询一个对象
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="rdr">datareader</param>
        /// <returns>一个实体对象或者null</returns>
        public static T QueryForObject<T>(IDataReader rdr) where T : class, new()
        {
            IList<T> listModel = QueryForList<T>(rdr);
            T result = listModel.Count > 0 ? listModel[0] : default(T);
            return result;
        }
        /// <summary>
        /// 查询对象列表
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="rdr">datareader</param>
        /// <returns>实体集合（没有时count=0）</returns>
        public static IList<T> QueryForList<T>(IDataReader rdr) where T : class, new()
        {
            Type objType = typeof(T);
            IDictionary<string, string> dictProperties = GetProperties(objType);
            IList<T> listModel = new List<T>();
            while (rdr.Read() == true)
            {
                T model = new T();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string columnName = rdr.GetName(i).ToUpper();
                    if (dictProperties.ContainsKey(columnName) == false)
                    {
                        continue;
                    }
                    object objValue = rdr[i];
                    SetValue(objType, columnName, model, objValue);
                }
                listModel.Add(model);
            }
            rdr.Close();
            return listModel;
        }
        /// <summary>
        /// 实体字典
        /// </summary>
        /// <typeparam name="K">唯一的键</typeparam>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strKey">主键对应的列名（不区分大小写）</param>
        /// <param name="rdr">datareader</param>
        /// <returns>实体字典</returns>
        public static IDictionary<K, T> QueryForDictionary<K, T>(string strKey, IDataReader rdr) where T : class, new()
        {
            Type objType = typeof(T);
            IDictionary<string, string> dictProperties = GetProperties(objType);
            IDictionary<K, T> dictModels = new Dictionary<K, T>();
            while (rdr.Read())
            {
                K key = default(K);
                T model = new T();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string columnName = rdr.GetName(i).ToUpper();
                    if (dictProperties.ContainsKey(columnName) == false)
                    {
                        continue;
                    }
                    object objValue = rdr[i];
                    if (string.Compare(strKey.Trim().ToUpper(), columnName) == 0)
                    {
                        key = (K)objValue;
                    }
                    SetValue(objType, columnName, model, objValue);
                }
                dictModels.Add(key, model);
            }
            rdr.Close();
            return dictModels;
        }

        /// <summary>
        /// 查询一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString">查询sql语句</param>
        /// <param name="strSqlConn">数据库连接字符串</param>
        /// <param name="sqlParams">参数集合 可以为null或者count=0</param>
        /// <returns></returns>
        public static T QueryForObject<T>(string sqlString, string strSqlConn, SqlParameter[] sqlParams) where T : class, new()
        {
            IList<T> listModel = QueryForList<T>(sqlString, strSqlConn, sqlParams);
            T result = listModel.Count > 0 ? listModel[0] : default(T);
            return result;
        }
        /// <summary>
        /// 查询对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString">查询sql语句</param>
        /// <param name="strSqlConn">数据库连接字符串</param>
        /// <param name="sqlParams">参数集合 可以为null或者count=0</param>
        /// <returns></returns>
        public static IList<T> QueryForList<T>(string sqlString, string strSqlConn, SqlParameter[] sqlParams) where T : class, new()
        {
            IList<T> listModel = new List<T>();
            Type objType = typeof(T);
            IDictionary<string, string> dictProperties = GetProperties(objType);
            using (SqlConnection conn = new SqlConnection(strSqlConn))
            {
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                SqlHelper.PrepareCommand(cmd, Config.commandTimeout);
                SqlHelper.PrepareCommand(cmd, sqlParams);
                conn.Open();
                IDataReader rdr = cmd.ExecuteReader();
                listModel = QueryForList<T>(rdr);
            }
            return listModel;
        }
        /// <summary>
        /// 实体字典
        /// </summary>
        /// <typeparam name="K">唯一的键</typeparam>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strKey">主键对应的列名（不区分大小写）</param>
        /// <param name="sqlString">查询sql语句</param>
        /// <param name="strSqlConn">数据库连接字符串</param>
        /// <param name="sqlParams">参数集合 可以为null或者count=0</param>
        /// <returns></returns>
        public static IDictionary<K, T> QueryForDictionary<K, T>(string strKey, string sqlString, string strSqlConn, SqlParameter[] sqlParams) where T : class, new()
        {
            Type objType = typeof(T);
            IDictionary<string, string> dictProperties = GetProperties(objType);
            IDictionary<K, T> dictModels = new Dictionary<K, T>();
            using (SqlConnection conn = new SqlConnection(strSqlConn))
            {
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                SqlHelper.PrepareCommand(cmd, Config.commandTimeout);
                SqlHelper.PrepareCommand(cmd, sqlParams);
                conn.Open();
                IDataReader rdr = cmd.ExecuteReader();
                dictModels = QueryForDictionary<K, T>(strKey, rdr);
            }
            return dictModels;
        }

        /// <summary>
        /// 查询一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString">查询sql语句</param>
        /// <param name="strSqlConn">数据库连接字符串</param>
        /// <param name="paramNames">参数名</param>
        /// <param name="paramValues">参数值</param>
        /// <returns></returns>
        public static T QueryForObject<T>(string sqlString, string strSqlConn, string[] paramNames, object[] paramValues) where T : class, new()
        {
            SqlParameter[] sqlParams = SqlHelper.PrepareParameters(paramNames, paramValues);
            return QueryForObject<T>(sqlString, strSqlConn, sqlParams);
        }
        /// <summary>
        /// 查询对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString">查询sql语句</param>
        /// <param name="strSqlConn">数据库连接字符串</param>
        /// <param name="paramNames">参数名</param>
        /// <param name="paramValues">参数值</param>
        /// <returns></returns>
        public static IList<T> QueryForList<T>(string sqlString, string strSqlConn, string[] paramNames, object[] paramValues) where T : class, new()
        {
            SqlParameter[] sqlParams = SqlHelper.PrepareParameters(paramNames, paramValues);
            return QueryForList<T>(sqlString, strSqlConn, sqlParams);
        }
        /// <summary>
        /// 查询实体字典
        /// </summary>
        /// <typeparam name="K">唯一的键</typeparam>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strKey">主键对应的列名（不区分大小写)</param>
        /// <param name="sqlString">查询sql语句</param>
        /// <param name="strSqlConn">数据库连接字符串</param>
        /// <param name="paramNames">参数名</param>
        /// <param name="paramValues">参数值</param>
        /// <returns></returns>
        public static IDictionary<K, T> QueryForDictionary<K, T>(string strKey, string sqlString, string strSqlConn, string[] paramNames, object[] paramValues) where T : class, new()
        {
            SqlParameter[] sqlParams = SqlHelper.PrepareParameters(paramNames, paramValues);
            return QueryForDictionary<K, T>(strKey, sqlString, strSqlConn, sqlParams);
        }

        /// <summary>
        /// 查询一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static T QueryForObject<T>(DataTable dt) where T : class, new()
        {
            IList<T> listModel = QueryForList<T>(dt);
            T result = listModel.Count > 0 ? listModel[0] : default(T);
            return result;
        }
        /// <summary>
        /// 查询对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static IList<T> QueryForList<T>(DataTable dt) where T : class, new()
        {
            IList<T> listModel = new List<T>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return listModel;
            }
            Type objType = typeof(T);
            IDictionary<string, string> dictProperties = GetProperties(objType);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T model = new T();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn dc in dt.Columns)
                {
                    string columnName = dc.ColumnName;
                    if (dictProperties.ContainsKey(columnName.ToUpper()) == false)
                    {
                        continue;
                    }
                    object objValue = dr[columnName];
                    SetValue(objType, columnName, model, objValue);
                }
                listModel.Add(model);
            }
            return listModel;
        }
        /// <summary>
        /// 实体字典
        /// </summary>
        /// <typeparam name="K">唯一的键</typeparam>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strKey">主键对应的列名（不区分大小写）</param>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static IDictionary<K, T> QueryForDictionary<K, T>(string strKey, DataTable dt) where T : class, new()
        {
            IDictionary<K, T> dictModels = new Dictionary<K, T>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return dictModels;
            }
            Type objType = typeof(T);
            IDictionary<string, string> dictProperties = GetProperties(objType);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                K key = default(K);
                T model = new T();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn dc in dt.Columns)
                {
                    string columnName = dc.ColumnName;
                    if (dictProperties.ContainsKey(columnName.ToUpper()) == false)
                    {
                        continue;
                    }
                    object objValue = dr[columnName];
                    SetValue(objType, columnName, model, objValue);
                    if (string.Compare(strKey.Trim().ToUpper(), columnName.ToUpper()) == 0)
                    {
                        key = (K)objValue;
                    }
                }
                dictModels.Add(key, model);
            }
            return dictModels;
        }

        #endregion

        #region 实体生成辅助方法

        /// <summary>
        /// 根据输入类型获取其属性
        /// </summary>
        /// <param name="objType">对象类型</param>
        /// <returns></returns>
        private static IDictionary<string, string> GetProperties(Type objType)
        {
            IDictionary<string, string> dictProperties = new Dictionary<string, string>();
            PropertyInfo[] props = objType.GetProperties(bf);
            foreach (PropertyInfo item in props)
            {
                if (string.IsNullOrEmpty(item.Name))
                {
                    continue;
                }
                if (dictProperties.ContainsKey(item.Name.ToUpper()) == false)
                {
                    dictProperties.Add(item.Name.ToUpper(), item.Name.ToUpper());//大写
                }
            }
            return dictProperties;
        }

        /// <summary>
        /// 给实体反射赋值
        /// </summary>
        /// <param name="objType">对象类型</param>
        /// <param name="columnName">数据库查询的某一列名（不区分大小写）</param>
        /// <param name="model">要赋值的实体对象</param>
        /// <param name="objValue">某一属性的值</param>
        private static void SetValue(Type objType, string columnName, object model, object objValue)
        {
            //  IConvertible obj = new string(new char[] { 'a' });
            //using (SqlConnection conn = new SqlConnection())
            //{
            //    SqlCommand cmd = conn.CreateCommand();
            //    cmd.CommandType = CommandType.TableDirect;
            //    SqlConnection.ClearAllPools();
            //}


            if (objValue == null || objValue == DBNull.Value)
            {
                return;
            }
            foreach (PropertyInfo item in objType.GetProperties())
            {
                string propName = item.Name;
                if (string.Compare(propName.ToUpper(), columnName.ToUpper()) != 0)
                {
                    continue;
                }
                try
                {
                    item.SetValue(model, objValue, null);
                }
                catch
                {
                    object realValue = null;
                    try
                    {
                        realValue = Convert.ChangeType(objValue, item.PropertyType);
                        item.SetValue(model, realValue, null);
                    }
                    catch
                    {
                    }
                }
                break;
            }
        }

        #endregion
    }

}
