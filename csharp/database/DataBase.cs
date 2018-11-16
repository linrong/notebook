using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageApplication.BaseClass
{
    /// <summary>
    /// 数据库的操作类
    /// </summary>
    class DataBase
    {
        private SqlConnection con;

        /// <summary>
        /// 打开数据库
        /// </summary>
        private void Open()
        {
            if (con == null)//判断连接对象是否为空
            {
                //数据库连接字符串，注意这个写法（localdb）后面必须是两个斜杠，因为这中间有个转义的过程
                //Initial Catalog=要连接的数据库名
                //Intergrated Security=true  开启windows身份验证
                string conStr = "Data Source=(localdb)\\Projects;Initial Catalog=db_ems;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
                con = new SqlConnection(conStr);
            }
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void Close()
        {
            if (con != null)
            {
                con.Close();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (con != null)//判断连接对象是否不为空
            {
                con.Dispose();//释放数据库连接资源
                con = null;//设置数据库连接为空
            }
        }

        /// <summary>
        /// 传入参数
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// 初始化参数值
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;//声明SQL参数对象
            if (Size > 0)//判断参数字段是否大于0
            {
                param = new SqlParameter(ParamName, DbType, Size);//根据指定的类型和大小创建SQL参数
            }
            else
            {
                param = new SqlParameter(ParamName, DbType);//根据指定的类型创建SQL参数
            }
            param.Direction = Direction;//设置SQL参数的类型
            if (!(Direction == ParameterDirection.Output && Value == null))//判断是否为输出参数
            {
                param.Value = Value;//设置参数返回值
            }
            return param;//返回SQL参数
        }

        /// <summary>
        /// 执行命令，主要是添加，修改和删除
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <returns></returns>
        public int RunProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);//创建SqlCommand对象
            cmd.ExecuteNonQuery();//执行SQL语句
            this.Close();//关闭数据库连接
            return (int)cmd.Parameters["ReturnValue"].Value;//得到执行成功返回值
        }

        /// <summary>
        /// 直接执行SQL语句,主要用于数据库的恢复和备份
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <returns></returns>
        public int RunProc(string procName)
        {
            this.Open();//打开数据库连接
            SqlCommand cmd = new SqlCommand(procName, con);//创建SqlCommand对象
            cmd.ExecuteNonQuery();//执行SQL语句
            this.Close();//关闭数据库
            return 1;//返回1，表示执行成功
        }

        /// <summary>
        /// 执行查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="tbName">数据表名称</param>
        /// <returns></returns>
        public DataSet RunProcReturn(string procName, SqlParameter[] prams, string tbName)
        {
            SqlDataAdapter dap = CreateDataAdapter(procName, prams);//创建桥接器对象
            DataSet ds = new DataSet();//创建数据集对象
            dap.Fill(ds, tbName);//填充数据集
            this.Close();//关闭数据库连接
            return ds;//返回数据集
        }

        /// <summary>
        /// 执行命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="tbName">数据表名称</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcReturn(string procName, string tbName)
        {
            //没有传递参数，直接执行文本命令
            SqlDataAdapter dap = CreateDataAdapter(procName, null);//创建数据集对象
            DataSet ds = new DataSet();//创建数据集对象
            dap.Fill(ds, tbName);//填充数据集
            this.Close();//关闭数据库连接
            return ds;//返回数据集
        }


        /// <summary>
        /// 创建一个SqlDataAdapter对象以此来执行命令文本
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <returns></returns>
        private SqlDataAdapter CreateDataAdapter(string procName, SqlParameter[] prams)
        {
            this.Open();//打开数据库连接
            SqlDataAdapter dap = new SqlDataAdapter(procName, con);//创建桥接器对象
            dap.SelectCommand.CommandType = CommandType.Text;//指定要执行的类型为命令文本
            if (prams != null)//判断SQL参数是否不为空
            {
                foreach (SqlParameter parameter in prams)//遍历传递的每个SQL参数
                    dap.SelectCommand.Parameters.Add(parameter);//将SQL参数添加到执行命令对象中
            }
            //加入返回参数
            //参数名称、参数类型、参数大小、ParameterDirection(参数的类型为ReturnValue型)、参数精度(是否可空)、参数小数位数、源列、要使用的 DataRowVersion 以及参数值初始化 SqlParameter 类的新实例。
            dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return dap;//返回桥接器对象
        }

        /// <summary>
        /// 创建一个SqlCommand对象以此来执行命令文本
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams"命令文本所需参数</param>
        /// <returns>返回SqlCommand对象</returns>
        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            this.Open();//打开数据库连接
            SqlCommand cmd = new SqlCommand(procName, con);//创建SqlCommand对象
            cmd.CommandType = CommandType.Text;//要执行的命令是文本命令
            //依次把参数传入命令文本
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);//将参数添加到命令对象中
            }
            //加入返回参数(这个参数大概是用来根据返回值判断是否成功？0或者1？)
            //参数名称、参数类型、参数大小、ParameterDirection(参数的类型为ReturnValue型)、参数精度(是否可空)、参数小数位数、源列、要使用的 DataRowVersion 以及参数值初始化 SqlParameter 类的新实例。
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;//返回SqlCommand命令对象
        }

    }
}
