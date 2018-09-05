using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DataSetDemo
{
    public partial class Form1 : Form
    {
        private SqlConnection conn = null;
        private SqlDataAdapter da = null;
        private DataSet set = null;
        private SqlCommandBuilder cmd = null;
        string cs = "";
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            conn.ConnectionString = cs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(cs);
                set = new DataSet();
                string sql = textBox1.Text;
                da = new SqlDataAdapter(sql, conn);
                dataGridView1.DataSource = null;
                cmd = new SqlCommandBuilder(da);
                da.Fill(set);
                dataGridView1.DataSource = set.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.Update(set);
        }
    }
}
