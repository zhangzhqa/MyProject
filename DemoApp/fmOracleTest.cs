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
    public partial class fmOracleTest : Form
    {
        public fmOracleTest()
        {
            InitializeComponent();
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            string sql = "";
            DataSet ds = OracleDAOHelper.GetDateSet(sql);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string sql = "select * from bd_customer";
            DataSet ds = OracleDAOHelper.GetDateSet(sql);
            dgvData.DataSource = ds.Tables[0];
        }

    }
}
