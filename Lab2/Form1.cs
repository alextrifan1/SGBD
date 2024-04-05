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
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab1
{
    public partial class Form1 : Form
    {
        SqlConnection cs = new SqlConnection("Data Source=DESKTOP-3AU86S9\\SQLEXPRESS;Initial Catalog=SGBDLAB;Integrated Security=True");

        public static string parentTable = ConfigurationManager.AppSettings.Get("parentTable");
        public static string childTable = ConfigurationManager.AppSettings.Get("childTable");
        public static string parentPrimaryKey = ConfigurationManager.AppSettings.Get("parentPrimaryKey");
        public static string childForeignKey = ConfigurationManager.AppSettings.Get("childForeignKey");
        public static string childPrimaryKey = ConfigurationManager.AppSettings.Get("childPrimaryKey");

        DataSet ds = new DataSet();

        SqlDataAdapter parentDataAdapter = new SqlDataAdapter();
        SqlDataAdapter childDataAdapter = new SqlDataAdapter();

        BindingSource parentBindingSource = new BindingSource();
        BindingSource childBindingSource = new BindingSource();

        //SqlCommandBuilder parentBuilder = new SqlCommandBuilder();
        //SqlCommandBuilder childBuilder = new SqlCommandBuilder();

        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }
        //afisare tabel 1
        private void InitializeData()
        {
            try
            {
                parentDataAdapter = new SqlDataAdapter("SELECT * FROM " + parentTable, cs);
                childDataAdapter = new SqlDataAdapter("SELECT * FROM " + childTable, cs);

                SqlCommandBuilder parentBuilder = new SqlCommandBuilder(parentDataAdapter);
                SqlCommandBuilder childBuilder = new SqlCommandBuilder(childDataAdapter);

                //ds.Clear();
                parentDataAdapter.Fill(ds, parentTable);
                childDataAdapter.Fill(ds, childTable);

                DataColumn parentColumn = ds.Tables[parentTable].Columns[parentPrimaryKey];
                DataColumn childColumn = ds.Tables[childTable].Columns[childForeignKey];

                DataRelation relation = new DataRelation("fk_parent_child", parentColumn, childColumn);
                ds.Relations.Add(relation);

                parentBindingSource.DataSource = ds;
                parentBindingSource.DataMember = parentTable;

                childBindingSource.DataSource = parentBindingSource;
                childBindingSource.DataMember = "fk_parent_child";

                dataGridView1.DataSource = parentBindingSource;
                dataGridView2.DataSource = childBindingSource;

                //dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                parentDataAdapter.Update(ds, parentTable);
                childDataAdapter.Update(ds, childTable);
                MessageBox.Show("Updated with succes!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cs.Close();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        { 
            InitializeData(); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //butonul pentru afisare din tabela parinte in tabela fiu
        private void button1_Click(object sender, EventArgs e){}

        private void panel1_Paint(object sender, PaintEventArgs e){}

        //buton de update
        private void button2_Click(object sender, EventArgs e)
        {
            if (childTable == "ModelMasina")
            {
                string idMasina = textBox2.Text;
                string idEchipa = textBox3.Text;
                string numeMasina = textBox4.Text;
                string idFurnizorMotor = textBox5.Text;
                try
                {
                    // parcurgem toate randurile din tabelul copil
                    DataRow rowToUpdate = null;
                    foreach (DataRow row in ds.Tables[childTable].Rows)
                    {
                        if (row[childPrimaryKey].ToString() == idMasina) // daca am gasit randul de sters il stergem si ne oprim
                        {
                            rowToUpdate = row;
                            break;
                        }
                    }

                    if (rowToUpdate != null)
                    {
                        rowToUpdate[childForeignKey] = idEchipa;
                        rowToUpdate["numeMasina"] = numeMasina;
                        rowToUpdate["idFurnizorMotor"] = idFurnizorMotor;

                        // Update the database using the data adapter to reflect the changes
                        childDataAdapter.Update(ds, childTable);

                        // Commit the changes to the DataSet
                        ds.AcceptChanges();

                        MessageBox.Show("Row updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No row found to update.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            if (childTable == "CascaSofer")
            {
                string idCasca = textBox2.Text;
                string producator = textBox3.Text;
                string culoarePrimara = textBox4.Text;
                string idSofer = textBox5.Text;
                try
                {
                    // parcurgem toate randurile din tabelul copil
                    DataRow rowToUpdate = null;
                    foreach (DataRow row in ds.Tables[childTable].Rows)
                    {
                        if (row[childPrimaryKey].ToString() == idCasca) // daca am gasit randul de sters il stergem si ne oprim
                        {
                            rowToUpdate = row;
                            break;
                        }
                    }

                    if (rowToUpdate != null)
                    {
                        rowToUpdate[childForeignKey] = idSofer;
                        rowToUpdate["producator"] = producator;
                        rowToUpdate["culoarePrimara"] = culoarePrimara;
                        rowToUpdate["idSofer"] = idSofer;

                        // Update the database using the data adapter to reflect the changes
                        childDataAdapter.Update(ds, childTable);

                        // Commit the changes to the DataSet
                        ds.AcceptChanges();

                        MessageBox.Show("Row updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No row found to update.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }   
            }
        }

        //buton de delete
        private void button3_Click(object sender, EventArgs e)
        {
            string idToBeDeleted = textBox2.Text;
            try
            {
                // parcurgem toate randurile din tabelul copil
                DataRow rowToDelete = null;
                foreach (DataRow row in ds.Tables[childTable].Rows)
                {
                    if (row[childPrimaryKey].ToString() == idToBeDeleted) // daca am gasit randul de sters il stergem si ne oprim
                    {
                        rowToDelete = row;
                        break;
                    }
                }

                if (rowToDelete != null)
                {
                    // stergem randul din data set
                    rowToDelete.Delete();

                    // Update the database using the data adapter to reflect the changes
                    childDataAdapter.Update(ds, childTable);

                    // Commit the changes to the DataSet
                    ds.AcceptChanges();

                    MessageBox.Show("Row deleted successfully!");
                }
                else
                {
                    MessageBox.Show("No row found to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //buton de add
        private void button4_Click(object sender, EventArgs e)
        {
            if (childTable == "ModelMasina")
            {
                string idMasina = textBox2.Text;
                string idEchipa = textBox3.Text;
                string numeMasina = textBox4.Text;
                string idFurnizorMotor = textBox5.Text;
                try
                {
                    DataRow newRow = ds.Tables[childTable].NewRow();
                    newRow[childPrimaryKey] = idMasina;
                    newRow[childForeignKey] = idEchipa;
                    newRow["numeMasina"] = numeMasina;
                    newRow["idFurnizorMotor"] = idFurnizorMotor;

                    ds.Tables[childTable].Rows.Add(newRow);

                    childDataAdapter.Update(ds, childTable);

                    ds.AcceptChanges();

                    MessageBox.Show("Row added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            if (childTable == "CascaSofer")
            {
                string idCasca = textBox2.Text;
                string producator = textBox3.Text;
                string culoarePrimara = textBox4.Text;
                string idSofer = textBox5.Text;
                try
                {
                    DataRow newRow = ds.Tables[childTable].NewRow();
                    newRow[childPrimaryKey] = idCasca;
                    newRow[childForeignKey] = idCasca;
                    newRow["producator"] = producator;
                    newRow["culoarePrimara"] = culoarePrimara;
                    newRow["idSofer"] = idSofer;

                    ds.Tables[childTable].Rows.Add(newRow);

                    childDataAdapter.Update(ds, childTable);

                    ds.AcceptChanges();

                    MessageBox.Show("Row added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
