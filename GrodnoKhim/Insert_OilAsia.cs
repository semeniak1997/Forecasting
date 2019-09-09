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
    public partial class Insert_ForecastAsia : Form
    {
        private SqlConnection sqlConnection = null;

        public Insert_ForecastAsia(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand insertAsiaCommand = new SqlCommand("INSERT INTO [Forecast_Table] (Period, OilPrise)VALUES(@Period, @OilPrise)", sqlConnection);

            try
            {
                insertAsiaCommand.Parameters.AddWithValue("Period", Convert.ToString(textBox1.Text));
                insertAsiaCommand.Parameters.AddWithValue("OilPrise", Convert.ToDouble(textBox2.Text));
                await insertAsiaCommand.ExecuteNonQueryAsync();
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
