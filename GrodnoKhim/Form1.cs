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
using System.Data.Linq;
using System.Configuration;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

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
            listView1.Columns.Add("Нефть ($)");
            listView1.Columns.Add("Бензол ($)");
            listView1.Columns.Add("Капролактам ($)");
            listView1.Columns.Add("Полиамид-6 ($)");
            listView1.Columns.Add("Нить полиамидная техническая ($)");
            listView1.Columns.Add("Ткань кордная ($)");
            await LoadAsiaAsync();

            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.View = View.Details;
            listView2.Columns.Add("Id");
            listView2.Columns.Add("Период");
            listView2.Columns.Add("Цена нефти ($)");
            await LoadOilAsync();
            listView6.GridLines = true;
            listView6.FullRowSelect = true;
            listView6.View = View.Details;
            listView6.Columns.Add("Id");
            listView6.Columns.Add("Период");
            listView6.Columns.Add("Цена нефти ($)");
            await LoadOilAsync1();

            listView4.GridLines = true;
            listView4.FullRowSelect = true;
            listView4.View = View.Details;
            listView4.Columns.Add("Id");
            listView4.Columns.Add("Период");
            listView4.Columns.Add("Нефть ($)");
            listView4.Columns.Add("Бензол (€)");
            listView4.Columns.Add("Капролактам (€)");
            listView4.Columns.Add("Полиамид-6 (€)");
            listView4.Columns.Add("BCF (€)");
            await LoadEuropeAsync();

            listView3.GridLines = true;
            listView3.FullRowSelect = true;
            listView3.View = View.Details;
            listView3.Columns.Add("Период");
            listView3.Columns.Add("Бензол ($)");
            listView3.Columns.Add("Капролактам ($)");
            listView3.Columns.Add("Полиамид-6 ($)");
            listView3.Columns.Add("Нить полиамидная техническая ($)");
            listView3.Columns.Add("Ткань кордная ($)");

            listView5.GridLines = true;
            listView5.FullRowSelect = true;
            listView5.View = View.Details;
            listView5.Columns.Add("Период");
            listView5.Columns.Add("Бензол (€)");
            listView5.Columns.Add("Капролактам (€)");
            listView5.Columns.Add("Полиамид-6 (€)");
            listView5.Columns.Add("BCF (€)");
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                
                sqlConnection.Close();
                
            }
        }
        private async Task LoadAsiaAsync() //SELECT ASIA
        {
            SqlDataReader sqlReader = null;
            SqlCommand getAsiaCommand = new SqlCommand("SELECT [Id], [Period], [Oil], [Benzene], [Caprolactam], [Polyamid], [FPT], [CF] FROM [Asia_Table]", sqlConnection);
            
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

        private async Task LoadOilAsync() //SELECT FORECAST OIL ASIA
        {
            SqlDataReader sqlReader = null;
            SqlCommand getOilCommand = new SqlCommand("SELECT [Id], [Period], [OilPrise] FROM [Forecast_table]", sqlConnection);
           
            try
            {
                sqlReader = await getOilCommand.ExecuteReaderAsync();
          
                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader["Id"]),
                        Convert.ToString(sqlReader["Period"]),
                        Convert.ToString(sqlReader["OilPrise"])
                    }
                        );
                    listView2.Items.Add(item);                         
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

        private async Task LoadOilAsync1() //SELECT FORECAST OIL ASIA
        {
            SqlDataReader sqlReader = null;
            SqlCommand getOilCommand = new SqlCommand("SELECT [Id], [Period], [OilPrise] FROM [Forecast_table]", sqlConnection);

            try
            {
                sqlReader = await getOilCommand.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader["Id"]),
                        Convert.ToString(sqlReader["Period"]),
                        Convert.ToString(sqlReader["OilPrise"])
                    }
                        );
                    listView6.Items.Add(item);
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

        private async Task LoadForecastAsync() // SELECT FORECAST ASIA
        {
            SqlDataReader sqlReader = null;
            SqlCommand getForecastCommand = new SqlCommand("SELECT [Period], [ForecastBenzeneAsia], [ForecastCaprolactamAsia], [ForecastPolyamidAsia], [ForecastFPTAsia], [ForecastCFAsia] FROM Forecast_Table", sqlConnection);

            try
            {
                sqlReader = await getForecastCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                        {
                        Convert.ToString(sqlReader["Period"]),
                        Convert.ToString(sqlReader["ForecastBenzeneAsia"]),
                        Convert.ToString(sqlReader["ForecastCaprolactamAsia"]),
                        Convert.ToString(sqlReader["ForecastPolyamidAsia"]),
                        Convert.ToString(sqlReader["ForecastFPTAsia"]),
                        Convert.ToString(sqlReader["ForecastCFAsia"])
                        }
                        );
                    listView3.Items.Add(item);
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

        private async Task LoadEuropeAsync()
        {
            SqlDataReader sqlReader = null;
            SqlCommand getEuropeCommand = new SqlCommand("SELECT [Id], [Period], [Oil], [Benzene], [Caprolactam], [Polyamid], [BCF] FROM [Europe_Table]", sqlConnection);

            try
            {
                sqlReader = await getEuropeCommand.ExecuteReaderAsync();

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
                        Convert.ToString(sqlReader["BCF"]),
                    }
                        );
                    listView4.Items.Add(item);
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

        private async Task LoadForecast1Async() // SELECT FORECAST ASIA
        {
            SqlDataReader sqlReader = null;
            SqlCommand getForecastCommand = new SqlCommand("SELECT [Period], [ForecastBenzeneEurope], [ForecastCaprolactamEurope], [ForecastPolyamidEurope], [ForecastBCFEurope] FROM Forecast_Table", sqlConnection);

            try
            {
                sqlReader = await getForecastCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                        {
                        Convert.ToString(sqlReader["Period"]),
                        Convert.ToString(sqlReader["ForecastBenzeneEurope"]),
                        Convert.ToString(sqlReader["ForecastCaprolactamEurope"]),
                        Convert.ToString(sqlReader["ForecastPolyamidEurope"]),
                        Convert.ToString(sqlReader["ForecastBCFEurope"]),
                        }
                        );
                    listView5.Items.Add(item);
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

        private async void button1_Click(object sender, EventArgs e)
        {
            string script = File.ReadAllText(@"SQLQuery3.sql");
            var sqlCmd = new SqlCommand(script, sqlConnection);
            await sqlCmd.ExecuteNonQueryAsync();
            await LoadForecastAsync();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Insert_ForecastAsia insert = new Insert_ForecastAsia(sqlConnection);
            insert.Show();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            await LoadOilAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                Update_ForecastAsia update = new Update_ForecastAsia(sqlConnection, Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text));
                update.Show();
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                switch (res)
                {
                    case DialogResult.Yes:

                        SqlCommand deleteForecastAsiaCommand = new SqlCommand("DELETE FROM [Forecast_Table] WHERE [Id]=@Id", sqlConnection);
                        deleteForecastAsiaCommand.Parameters.AddWithValue("Id", Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text));

                        try
                        {
                            await deleteForecastAsiaCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        listView2.Items.Clear();
                        await LoadOilAsync();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonEuropeInsert_Click(object sender, EventArgs e)
        {
            Insert_Europe insert = new Insert_Europe(sqlConnection);
            insert.Show();
        }

        private async void toolStripButtonEuropeRefresh_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            await LoadEuropeAsync();
        }

        private void toolStripButtonEuropeUpdate_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                Update_Europe update = new Update_Europe(sqlConnection, Convert.ToInt32(listView4.SelectedItems[0].SubItems[0].Text));
                update.Show();
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void toolStripButtonEuropeDelete_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                switch (res)
                {
                    case DialogResult.Yes:

                        SqlCommand deleteEuropeCommand = new SqlCommand("DELETE FROM [Europe_Table] WHERE [Id]=@Id", sqlConnection);
                        deleteEuropeCommand.Parameters.AddWithValue("Id", Convert.ToInt32(listView4.SelectedItems[0].SubItems[0].Text));

                        try
                        {
                            await deleteEuropeCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        listView4.Items.Clear();
                        await LoadEuropeAsync();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Insert_ForecastAsia insert = new Insert_ForecastAsia(sqlConnection);
            insert.Show();
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            listView6.Items.Clear();
            await LoadOilAsync1();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listView6.SelectedItems.Count > 0)
            {
                Update_ForecastAsia update = new Update_ForecastAsia(sqlConnection, Convert.ToInt32(listView6.SelectedItems[0].SubItems[0].Text));
                update.Show();
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if (listView6.SelectedItems.Count > 0)
            {
                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                switch (res)
                {
                    case DialogResult.Yes:

                        SqlCommand deleteForecastAsiaCommand = new SqlCommand("DELETE FROM [Forecast_Table] WHERE [Id]=@Id", sqlConnection);
                        deleteForecastAsiaCommand.Parameters.AddWithValue("Id", Convert.ToInt32(listView6.SelectedItems[0].SubItems[0].Text));

                        try
                        {
                            await deleteForecastAsiaCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        listView6.Items.Clear();
                        await LoadOilAsync1();

                        break;
                }
            }
            else
            {
                MessageBox.Show("Ни одна запись не была выделена!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            string script = File.ReadAllText(@"SQLQuery2.sql");
            var sqlCmd = new SqlCommand(script, sqlConnection);
            await sqlCmd.ExecuteNonQueryAsync();
            await LoadForecast1Async();
            button12.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listView5.Items.Clear();
            button12.Enabled = true;
        }

        private async void toolStripButtonBuildAsiaOil_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;
            SqlCommand getBuildCommand = new SqlCommand("SELECT * FROM Asia_Table", sqlConnection);

            try
            {

                Axis ax = new Axis();
                ax.Title = "Дата";
                chart1.ChartAreas[0].AxisX = ax;
                Axis ay = new Axis();
                ay.Title = "Цена";
                chart1.ChartAreas[0].AxisY = ay;
                chart1.Visible = true;
                sqlReader = await getBuildCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    
                    this.chart1.Series["Бензол"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["Benzene"].ToString());
                    chart1.Series["Капролактам"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["Caprolactam"].ToString());
                    chart1.Series["Полиамид-6"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["Polyamid"].ToString());
                    chart1.Series["Нить полиамидная техническая"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["FPT"].ToString());
                    chart1.Series["Ткань кордная"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["CF"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }
        private void toolStripButtonClearAsiaOil_Click(object sender, EventArgs e)
        {
            this.chart1.Images.Clear();
            chart1.Visible = false;
        }

        private async void toolStripButtonBuildEurope_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;
            SqlCommand getBuildCommand = new SqlCommand("SELECT * FROM Europe_Table", sqlConnection);

            try
            {

                Axis ax = new Axis();
                ax.Title = "Дата";
                chart2.ChartAreas[0].AxisX = ax;
                Axis ay = new Axis();
                ay.Title = "Цена";
                chart2.ChartAreas[0].AxisY = ay;
                chart2.Visible = true;
                sqlReader = await getBuildCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {

                    this.chart2.Series["Бензол"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["Benzene"].ToString());
                    chart2.Series["Капролактам"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["Caprolactam"].ToString());
                    chart2.Series["Полиамид-6"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["Polyamid"].ToString());
                    chart2.Series["BCF"].Points.AddXY(sqlReader["Period"].ToString(), sqlReader["BCF"].ToString());                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private void toolStripButtonClearEurope_Click(object sender, EventArgs e)
        {
            this.chart2.Images.Clear();
            chart2.Visible = false;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {

                sqlConnection.Close();

            }
            Close();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }
    }

}

