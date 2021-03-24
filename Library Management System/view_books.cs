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
            TryCatchViewBooks("SELECT * FROM books_info;", false);
        }

        private void TryCatchViewBooks(string commandText, bool load)
        {
            try
            {
                LoadViewBooksGrid(commandText, load);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadViewBooksGrid(string commandText, bool load)
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
            if (load) FillTextboxes(dataTable);
        }

        private void NoBooksFound(int numberOfBooks)
        {
            if (numberOfBooks == 0)
            {
                MessageBox.Show("No Books Found!");
            }    
        }

        private void searchByNameTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            TryCatchViewBooks("SELECT * FROM books_info WHERE books_name like('%" + searchByNameTextbox.Text + "%')", false);
        }

        private void searchByAuthorTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            TryCatchViewBooks("SELECT * FROM books_info WHERE author_name = '%" + searchByAuthorTextbox.Text + "%'", false);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            TryCatchViewBooks("SELECT * FROM books_info WHERE ID = " + id + ";", true);
            
        }

        private void FillTextboxes(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                booksNameTextbox.Text = dataRow["books_name"].ToString();
                authorNameTextbox.Text = dataRow["author_name"].ToString();
                publicationDateTimepicker.Value = Convert.ToDateTime(dataRow["publication_date"].ToString());
                pagesNumberTextbox.Text = dataRow["pages_number"].ToString();
                priceTextbox.Text = dataRow["price"].ToString();
                quantityTextbox.Text = dataRow["books_quantity"].ToString();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            TryCatchViewBooks("UPDATE books_info SET books_name = '" + booksNameTextbox.Text + "', author_name = '" + authorNameTextbox.Text + "', " +
                    "publication_date = '" + publicationDateTimepicker.Value.ToString("MM/dd/yyyy") + "', pages_number = '" + pagesNumberTextbox.Text + "'," +
                    "price = '" + priceTextbox.Text + "', books_quantity = '" + quantityTextbox.Text + "' WHERE ID='" + id + "';", false);
            TryCatchViewBooks("SELECT * FROM books_info;", false);
            MessageBox.Show("Books updated successfully");
        }
    }
}
