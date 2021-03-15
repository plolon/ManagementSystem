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

namespace Library_Management_System
{
    public partial class login : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IL2LHNT;Initial Catalog=library-menagement;Integrated Security=True;Pooling=False");
        int count = 0;
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            TryCatchLogin();

        }

        private void TryCatchLogin()
        {
            try
            {
                LoginToDataBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginToDataBase()
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM library_person WHERE username = '" + textBox1.Text + "' AND password = '" + textBox2.Text + "'";
            command.ExecuteNonQuery();
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            count = Convert.ToInt32(dataTable.Rows.Count.ToString());
            CheckPasswordAndLogin(count);
        }

        private void CheckPasswordAndLogin(int AccountId)
        {
            if (AccountId == 0)
            {
                MessageBox.Show("Username and Password doesn't match!");
            }
            else
            {
                this.Hide();
                mdi_user mdiUser = new mdi_user();
                mdiUser.Show();
            }
        }
    }
}
