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
    public partial class Update_Asia : Form
    {
        private SqlConnection sqlConnection = null;
        private int id;

        public Update_Asia(SqlConnection connection, int id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.id = id;
        }

        private async void Update_Asia_Load(object sender, EventArgs e)
        {
            SqlCommand getAsiaInfoCommand = new SqlCommand("SELECT [Period], [Oil], [Benzene], [Caprolactam], [Polyamid], [FPT], [CF] FROM [Asia_Table] WHERE [Id]=@id", sqlConnection);
            getAsiaInfoCommand.Parameters.AddWithValue("Id", id);
            SqlDataReader sqlReader = null;

            try
            {
                sqlReader = await getAsiaInfoCommand.ExecuteReaderAsync();
                while(await sqlReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(sqlReader["Period"]);
                    textBox2.Text = Convert.ToString(sqlReader["Oil"]);
                    textBox3.Text = Convert.ToString(sqlReader["Benzene"]);
                    textBox4.Text = Convert.ToString(sqlReader["Caprolactam"]);
                    textBox5.Text = Convert.ToString(sqlReader["Polyamid"]);
                    textBox6.Text = Convert.ToString(sqlReader["FPT"]);
                    textBox7.Text = Convert.ToString(sqlReader["CF"]);

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
            SqlCommand updateAsiaCommand = new SqlCommand("UPDATE [Asia_Table] SET [Period]=@Period, [Oil]=@Oil, [Benzene]=@Benzene, [Caprolactam]=@Caprolactam, [Polyamid]=@Polyamid, [FPT]=@FPT, [CF]=@CF WHERE [Id]=@Id", sqlConnection);           

            try
            {
                updateAsiaCommand.Parameters.AddWithValue("Id", id);
                updateAsiaCommand.Parameters.AddWithValue("Period", Convert.ToString(textBox1.Text));
                updateAsiaCommand.Parameters.AddWithValue("Oil", Convert.ToDouble(textBox2.Text));
                updateAsiaCommand.Parameters.AddWithValue("Benzene", Convert.ToDouble(textBox3.Text));
                updateAsiaCommand.Parameters.AddWithValue("Caprolactam", Convert.ToDouble(textBox4.Text));
                updateAsiaCommand.Parameters.AddWithValue("Polyamid", Convert.ToDouble(textBox5.Text));
                updateAsiaCommand.Parameters.AddWithValue("FPT", Convert.ToDouble(textBox6.Text));
                updateAsiaCommand.Parameters.AddWithValue("CF", Convert.ToDouble(textBox7.Text));
                await updateAsiaCommand.ExecuteNonQueryAsync();
                Close();
            }
            catch( Exception ex)
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
