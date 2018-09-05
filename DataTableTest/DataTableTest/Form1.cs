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

namespace DataTableTest
{
    public partial class Form1 : Form
    {
        private SqlDataReader reader;
        private DataTable table;
        private SqlConnection conn;
        private string cs = "";
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
                SqlCommand comm = new SqlCommand();
                comm.CommandText = textBox1.Text;
                comm.Connection = conn;
                dataGridView1.DataSource = null;
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Соединение подключенно успешно!");
                }
                table = new DataTable();
                reader = comm.ExecuteReader();
                int line = 0;
                while (reader.Read())
                {
                    //создание полей
                    if(line == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            table.Columns.Add(reader.GetName(i));
                        }
                        line++;
                    }

                    //наполняем ряды
                    DataRow row = table.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }
                    table.Rows.Add(row);
                }
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn?.Close();
                reader?.Close();
            }
        }
    }
}
