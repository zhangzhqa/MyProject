using System;
using System.Data;
using System.Configuration;
using System.Web;
using Oracle.ManagedDataAccess.Client;

/// <summary>
/// OracleDAOHelp 的摘要说明
/// </summary>
public class OracleDAOHelper
{
    //ConfigurationManager.ConnectionStrings["PLMDBConnection"].ConnectionString;
    protected static string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.31.11)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=zsjt;Password=Sunpowercom1;";
    //protected static string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.24.12.207)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCLnj)));Persist Security Info=True;User ID=SLHG65;Password=1;";

    public OracleDAOHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
   
	/// <summary>
	/// 获取Sql计数
	/// </summary>
	/// <param name="sql"></param>
	/// <returns></returns>
	public static long ExecuteSqlToCount(string sql)
	{
		long result = 0;
		OracleConnection theConn = new OracleConnection(connStr);
		theConn.Open();
		OracleCommand theComm = new OracleCommand(sql, theConn);
		object returnValue = theComm.ExecuteScalar();
		theConn.Close();

		if (returnValue != DBNull.Value)
		{
			result = long.Parse(returnValue.ToString());
		}
		theConn = null;
		return result;
	}

	/// <summary>
	///  execute insert,update,delete operation.
	/// </summary>
	/// <param name="sql"></param>
	/// <returns></returns>
	public static bool ExecuteSql(string sql)
	{
		bool ret = true;
		string []sqlStatments = sql.Trim().Split(';');
		OracleConnection theConn = new OracleConnection(connStr);
		//OracleTransaction transaction;
		theConn.Open();
		// Start a local transaction
		//transaction = theConn.BeginTransaction(IsolationLevel.ReadCommitted);
		int afftedRecords = 0;
		foreach (string executeSql in sqlStatments)
			{
				if (String.IsNullOrEmpty(executeSql) ==false)
				{
                    try
                    {
                        OracleCommand theComm = new OracleCommand(executeSql, theConn);
                        //theComm.Transaction = transaction;
                        afftedRecords = theComm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //ProjectTools.writeLog(ex.ToString(),"Oracel Access Error,Please Check Log:Sql statement " + executeSql);
                        throw  (ex);
                    }
				}
			}
		theConn.Close();
		theConn = null;
		return ret;
	}
	
	/// <summary>
	/// Get DataSet
	/// </summary>
	/// <param name="sql"></param>
	/// <returns></returns>
	public static DataSet GetDateSet(string sql)
	{
		OracleConnection theConn = new OracleConnection(connStr);
		theConn.Open();
		OracleDataAdapter theAd = new OracleDataAdapter(sql, theConn);
		DataSet ds = new DataSet();
		theAd.Fill(ds, "defaultTable");
		theConn.Close();
		theAd = null;
		theConn = null;
		return ds;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="selectSql"></param>
	/// <returns></returns>
	public static OracleDataReader GetDataReader(string selectSql)
	{
		OracleConnection theConn = new OracleConnection(connStr);
		theConn.Open();
		OracleCommand theCommand = new OracleCommand(selectSql, theConn);
		OracleDataReader dr = theCommand.ExecuteReader(CommandBehavior.CloseConnection);
		return dr;
	}
}
