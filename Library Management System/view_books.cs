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
    public partial class view_books : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IL2LHNT;Initial Catalog=library-menagement;Integrated Security=True;Pooling=False");

        public view_books()
        {
            InitializeComponent();
        }

        private void view_books_Load(object sender, EventArgs e)
        {
            TryCatchViewBooks("SELECT * FROM books_info;");
        }

        private void TryCatchViewBooks(string commandText)
        {
            try
            {
                LoadViewBooksGrid(commandText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadViewBooksGrid(string commandText)
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
            command.ExecuteNonQuery();
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
            NoBooksFound(Convert.ToInt32(dataTable.Rows.Count.ToString()));
        }

        private void NoBooksFound(int numberOfBooks)
        {
            if (numberOfBooks == 0)
            {
                MessageBox.Show("No Books Found!");
            }    
        }

        private void searchBookBox_KeyUp(object sender, KeyEventArgs e)
        {
            TryCatchViewBooks("SELECT * FROM books_info WHERE books_name like('%" + searchBookBox.Text + "%')");
        }
    }
}
