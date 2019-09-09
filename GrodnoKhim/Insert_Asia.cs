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
    public partial class Insert_Asia : Form
    {
        private SqlConnection sqlConnection = null;
        public Insert_Asia(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand insertAsiaCommand = new SqlCommand("INSERT INTO [Asia_Table] (Period, Oil, Benzene, Caprolactam, Polyamid, FPT, CF)VALUES(@Period, @Oil, @Benzene, @Caprolactam, @Polyamid, @FPT, @CF)", sqlConnection);
            try
            {
                insertAsiaCommand.Parameters.AddWithValue("Period", Convert.ToString(textBox1.Text));
                insertAsiaCommand.Parameters.AddWithValue("Oil", Convert.ToDouble(textBox2.Text));
                insertAsiaCommand.Parameters.AddWithValue("Benzene", Convert.ToDouble(textBox3.Text));
                insertAsiaCommand.Parameters.AddWithValue("Caprolactam", Convert.ToDouble(textBox4.Text));
                insertAsiaCommand.Parameters.AddWithValue("Polyamid", Convert.ToDouble(textBox5.Text));
                insertAsiaCommand.Parameters.AddWithValue("FPT", Convert.ToDouble(textBox6.Text));
                insertAsiaCommand.Parameters.AddWithValue("CF", Convert.ToDouble(textBox7.Text));
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
