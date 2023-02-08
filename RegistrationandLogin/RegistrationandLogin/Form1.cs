using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RegistrationandLogin
{
    public partial class Form1 : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RegistrationandLogin\RegistrationandLogin\Database1.mdf;Integrated Security=True");
            cn.Open();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 login = new Form2();
            login.ShowDialog();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (textConfirm.Text != string.Empty || textPass.Text != string.Empty || textUser.Text != string.Empty)
            {
                if (textPass.Text == textConfirm.Text)
                {
                    cmd = new SqlCommand("select * from LoginTables where username='" + textUser.Text + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        MessageBox.Show("Username Already exist please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand("insert into LoginTables (username, password) values (@username,@password)", cn);
                        cmd.Parameters.AddWithValue("username", textUser.Text);
                        cmd.Parameters.AddWithValue("password", textPass.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your Account is created . Please login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both password same ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

