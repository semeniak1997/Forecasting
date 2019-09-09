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
    public partial class Insert_Europe : Form
    {
        private SqlConnection sqlConnection = null;
        public Insert_Europe(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand insertEuropeCommand = new SqlCommand("INSERT INTO [Europe_Table] (Period, Oil, Benzene, Caprolactam, Polyamid, BCF)VALUES(@Period, @Oil, @Benzene, @Caprolactam, @Polyamid, @BCF)", sqlConnection);
            try
            {
                insertEuropeCommand.Parameters.AddWithValue("Period", Convert.ToString(textBox1.Text));
                insertEuropeCommand.Parameters.AddWithValue("Oil", Convert.ToDouble(textBox2.Text));
                insertEuropeCommand.Parameters.AddWithValue("Benzene", Convert.ToDouble(textBox3.Text));
                insertEuropeCommand.Parameters.AddWithValue("Caprolactam", Convert.ToDouble(textBox4.Text));
                insertEuropeCommand.Parameters.AddWithValue("Polyamid", Convert.ToDouble(textBox5.Text));
                insertEuropeCommand.Parameters.AddWithValue("BCF", Convert.ToDouble(textBox6.Text));
                await insertEuropeCommand.ExecuteNonQueryAsync();

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
