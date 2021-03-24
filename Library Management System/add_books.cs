using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class add_books : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IL2LHNT;Initial Catalog=library-menagement;Integrated Security=True;Pooling=False");

        public add_books()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            TryCatchBookSaveToDataBase();
            ClearAddBookView();
        }
        private void ClearAddBookView()
        {
            nameBox.Text = "";
            authorBox.Text = "";
            pagesBox.Text = "";
            priceBox.Text = "";
            quantityBox.Text = "";
        }
        private void TryCatchBookSaveToDataBase()
        {
            try
            {
                SaveBookToDataBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
        private void SaveBookToDataBase()
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO books_info VALUES('" + nameBox.Text + "','" + authorBox.Text 
                + "','" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','" + pagesBox.Text + "','" + priceBox.Text
                + "','" + quantityBox.Text + "')";
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Books added successfully!");
        }
    }
}
