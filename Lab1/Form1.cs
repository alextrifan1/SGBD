using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        SqlConnection cs = new SqlConnection("Data Source=DESKTOP-3AU86S9\\SQLEXPRESS;Initial Catalog=SGBDLAB;Integrated Security=True");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            try
            {
                da.SelectCommand = new SqlCommand("SELECT * FROM Echipa", cs);
                ds.Clear();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedValue = textBox1.Text;
            try
            {
                SqlDataAdapter da2 = new SqlDataAdapter();
                DataSet ds2 = new DataSet();
                da2.SelectCommand = new SqlCommand("SELECT * FROM ModelMasina WHERE idEchipa = @selectedValue", cs);
                da2.SelectCommand.Parameters.AddWithValue("@selectedValue", selectedValue); // Add parameter
                ds2.Clear();
                da2.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idMasina = textBox2.Text;
            string idEchipa = textBox3.Text;
            string numeMasina = textBox4.Text;
            string idFurnizorMasina = textBox5.Text;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand("UPDATE ModelMasina SET numeMasina = @numeMasina, idFurnizorMotor = @idFurnizorMotor WHERE idMasina = @idMasina", cs);
                command.Parameters.AddWithValue("@idEchipa", idEchipa);
                command.Parameters.AddWithValue("@numeMasina", numeMasina);
                command.Parameters.AddWithValue("@idFurnizorMotor", idFurnizorMasina);
                command.Parameters.AddWithValue("@idMasina", idMasina);

                cs.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Row updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string idMasina = textBox2.Text;
            string idEchipa = textBox3.Text;
            string numeMasina = textBox4.Text;
            string idFurnizorMasina = textBox5.Text;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand("DELETE FROM ModelMasina WHERE idMasina = @idMasina", cs);
                command.Parameters.AddWithValue("@idMasina", idMasina);

                cs.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Row deleted successfully!");
                }
                else
                {
                    MessageBox.Show("No rows were deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string idMasina = textBox6.Text;
            string idEchipa = textBox7.Text;
            string numeMasina = textBox8.Text;
            string idFurnizorMotor = textBox9.Text;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand("INSERT INTO ModelMasina VALUES(@idMasina,@idEchipa,@numeMasina,@idFurnizorMotor)",cs);
                command.Parameters.AddWithValue("@idEchipa", idEchipa);
                command.Parameters.AddWithValue("@numeMasina", numeMasina);
                command.Parameters.AddWithValue("@idFurnizorMotor", idFurnizorMotor);
                command.Parameters.AddWithValue("@idMasina", idMasina);

                cs.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
            }
        }
    }
}
