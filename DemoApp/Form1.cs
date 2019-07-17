using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = true;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ReadDataByDataBase();
        }

        private void ReadDataByDataBase()
        {
            //连接字符串1
            string connectionString = "Data Source=10.24.12.207/orclnj;User ID=ybs02;PassWord=1";
            //连接字符串2
            connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.24.12.207)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orclnj)));Persist Security Info=True;User ID=ybs02;Password=1;";

            string queryString = "SELECT * FROM SM_User";

            OracleConnection myConnection = new OracleConnection(connectionString);
            myConnection.Open();
            OracleCommand myORACCommand = myConnection.CreateCommand();
            myORACCommand.CommandText = queryString;

            OracleDataReader myDataReader = myORACCommand.ExecuteReader();
            //myDataReader.Read();

            DataTable dt = new DataTable();
            dt.Load(myDataReader);

            this.dgvData.DataSource = dt;

            myDataReader.Close();
            myConnection.Close();

        }
    }
}
