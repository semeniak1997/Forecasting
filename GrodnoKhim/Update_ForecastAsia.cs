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

namespace GrodnoKhim
{
    public partial class Update_ForecastAsia : Form
    {
        private SqlConnection sqlConnection = null;
        private int id;

        public Update_ForecastAsia(SqlConnection connection, int id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.id = id;
        }

        private async void Update_ForecastAsia_Load(object sender, EventArgs e)
        {
            SqlCommand getAsiaInfoCommand = new SqlCommand("SELECT [Period], [OilPrise] FROM [Forecast_Table] WHERE [Id]=@id", sqlConnection);
            getAsiaInfoCommand.Parameters.AddWithValue("Id", id);
            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getAsiaInfoCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(sqlReader["Period"]);
                    textBox2.Text = Convert.ToString(sqlReader["OilPrise"]);
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

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand updateForecastAsiaCommand = new SqlCommand("UPDATE [Forecast_Table] SET [Period]=@Period, [OilPrise]=@OilPrise WHERE [Id]=@Id", sqlConnection);
            updateForecastAsiaCommand.Parameters.AddWithValue("Id", id);                     
            try
            {
                updateForecastAsiaCommand.Parameters.AddWithValue("Period", Convert.ToString(textBox1.Text));
                updateForecastAsiaCommand.Parameters.AddWithValue("OilPrise", Convert.ToDouble(textBox2.Text));
                await updateForecastAsiaCommand.ExecuteNonQueryAsync();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
