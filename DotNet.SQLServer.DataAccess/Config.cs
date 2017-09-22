using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DotNet.SQLServer.DataAccess
{
    public class Config
    {
        #region configuration

        public static readonly int commandTimeout = 60;//默认60秒

        static Config()
        {
            try
            {
                commandTimeout = int.Parse(ConfigurationManager.AppSettings["CommandTimeout"]);//获取或设置在终止执行命令的尝试并生成错误之前的等待时间
            }
            catch
            {
                commandTimeout = 60;
            }
        }


        //public static void SetCommandTimeout(IDbCommand cmd)
        //{
        //    cmd.CommandTimeout = commandTimeout;
        //}

        #endregion
    }
}
