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
using System.Configuration;

namespace GrodnoKhim
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GrodnoKhim.Properties.Settings.DatabaseConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

            listView1.Columns.Add("Id");
            listView1.Columns.Add("Период");
            listView1.Columns.Add("Нефть");
            listView1.Columns.Add("Бензол");
            listView1.Columns.Add("Капролактам");
            listView1.Columns.Add("Полиамид-6");
            listView1.Columns.Add("Нить полиамидная техническая");
            listView1.Columns.Add("Ткань кордная");

            await LoadAsiaAsync();

            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.View = View.Details;
            listView2.Columns.Add("Период");
            listView2.Columns.Add("Цена нефти");
            await LoadOilAsync();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.BeginTransaction();
                sqlConnection.Close();
                
            }
        }
        private async Task LoadAsiaAsync() //SELECT
        {
            SqlDataReader sqlReader = null;
            SqlCommand getAsiaCommand = new SqlCommand("SELECT * FROM [Asia_Table]", sqlConnection);
            

            try
            {
                sqlReader = await getAsiaCommand.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader["Id"]),
                        Convert.ToString(sqlReader["Period"]),
                        Convert.ToString(sqlReader["Oil"]),
                        Convert.ToString(sqlReader["Benzene"]),
                        Convert.ToString(sqlReader["Caprolactam"]),
                        Convert.ToString(sqlReader["Polyamid"]),
                        Convert.ToString(sqlReader["FPT"]),
                        Convert.ToString(sqlReader["CF"])
                    }
                        );
                    listView1.Items.Add(item);

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async Task LoadOilAsync() //SELECT
        {
            SqlDataReader sqlReader1 = null;
            SqlCommand getOilCommand = new SqlCommand("SELECT [Period], [OilPrise] FROM [Oil_table]", sqlConnection);

            try
            {
                sqlReader1 = await getOilCommand.ExecuteReaderAsync();
                while (await sqlReader1.ReadAsync())
                {
                    ListViewItem item2 = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader1["Period"]),
                        Convert.ToString(sqlReader1["OilPrise"]),
                    }
                        );
                    listView2.Items.Add(item2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader1 != null && !sqlReader1.IsClosed)
                {
                    sqlReader1.Close();
                }
            }
        }

        private async void toolStripButton4_Click(object sender, EventArgs e) //Обновиить
        {
            listView1.Items.Clear();
            await LoadAsiaAsync();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) //Добавить
        {
            Insert_Asia insert = new Insert_Asia(sqlConnection);
            insert.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Update_Asia update = new Update_Asia(sqlConnection, Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                update.Show();
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                switch (res)
                {
                    case DialogResult.Yes:

                        SqlCommand deleteAsiaCommand = new SqlCommand("DELETE FROM [Asia_Table] WHERE [Id]=@Id", sqlConnection);
                        deleteAsiaCommand.Parameters.AddWithValue("Id", Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));

                        try
                        {
                            await deleteAsiaCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        listView1.Items.Clear();
                        await LoadAsiaAsync();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}

