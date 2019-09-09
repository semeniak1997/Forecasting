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
    public partial class Update_Europe : Form
    {
        private SqlConnection sqlConnection = null;
        private int id;
        public Update_Europe(SqlConnection connection, int id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.id = id;
        }

        private async void Update_Europe_Load(object sender, EventArgs e)
        {
            SqlCommand getEuropeInfoCommand = new SqlCommand("SELECT [Period], [Oil], [Benzene], [Caprolactam], [Polyamid], [BCF] FROM [Europe_Table] WHERE [Id]=@id", sqlConnection);
            getEuropeInfoCommand.Parameters.AddWithValue("Id", id);
            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getEuropeInfoCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(sqlReader["Period"]);
                    textBox2.Text = Convert.ToString(sqlReader["Oil"]);
                    textBox3.Text = Convert.ToString(sqlReader["Benzene"]);
                    textBox4.Text = Convert.ToString(sqlReader["Caprolactam"]);
                    textBox5.Text = Convert.ToString(sqlReader["Polyamid"]);
                    textBox6.Text = Convert.ToString(sqlReader["BCF"]);
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
            SqlCommand updateEuropeCommand = new SqlCommand("UPDATE [Europe_Table] SET [Period]=@Period, [Oil]=@Oil, [Benzene]=@Benzene, [Caprolactam]=@Caprolactam, [Polyamid]=@Polyamid, [BCF]=@BCF WHERE [Id]=@Id", sqlConnection);     

            try
            {
                updateEuropeCommand.Parameters.AddWithValue("Id", id);
                updateEuropeCommand.Parameters.AddWithValue("Period", Convert.ToString(textBox1.Text));
                updateEuropeCommand.Parameters.AddWithValue("Oil", Convert.ToDouble(textBox2.Text));
                updateEuropeCommand.Parameters.AddWithValue("Benzene", Convert.ToDouble(textBox3.Text));
                updateEuropeCommand.Parameters.AddWithValue("Caprolactam", Convert.ToDouble(textBox4.Text));
                updateEuropeCommand.Parameters.AddWithValue("Polyamid", Convert.ToDouble(textBox5.Text));
                updateEuropeCommand.Parameters.AddWithValue("BCF", Convert.ToDouble(textBox6.Text));
                await updateEuropeCommand.ExecuteNonQueryAsync();
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
