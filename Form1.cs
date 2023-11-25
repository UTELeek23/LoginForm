using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LoginForm
{
    public partial class Form1 : Form
    {
        string connect_string = "Data Source=34.176.57.189;Initial Catalog=OOP;Persist Security Info=True;User ID=sqlserver;Password=Lak@2302;";
        Encrypt encr = new Encrypt();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String username = textBox1.Text;
                String password = textBox2.Text;
                String query = "SELECT * FROM ACCOUNTS WHERE username = @username;";
                using (SqlConnection sqlConnection = new SqlConnection(connect_string))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@username", username);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if(sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            byte[] hashedPasswordBytes = (byte[])sqlDataReader["password"];
                            if (encr.VerifyPassword(password, hashedPasswordBytes))
                            {
                                MessageBox.Show("Login successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Wrong password!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong username!");
                    }
                    sqlConnection.Close();
                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
