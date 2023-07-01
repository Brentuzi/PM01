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
using Excel = Microsoft.Office.Interop.Excel;
namespace Postupinskii
{
    public partial class Material : Form
    {
        bool log = false;
     string connectionString = "Data Source=C41213\\SQLEXPRESS;Initial Catalog=BD_Postupinskii;Integrated Security=True";
        public Material(bool log)
        {
            InitializeComponent();
            this.log = log;
            if (log == false)
            {
                this.Size = new Size(983, 518);
            } 



       
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
             
                connection.Open();

                
                string sqlQuery = @"
                SELECT        dbo.Materials.[Наименование материала], dbo.Materials.Количество, dbo.Materials.Цена, dbo.Suppliers.фирма, dbo.Materials.[Дата поступления], dbo.Materials.Остаток
FROM            dbo.Materials INNER JOIN
                         dbo.Suppliers ON dbo.Materials.Поставщик = dbo.Suppliers.кодФирмы
            ";

               
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                   
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        
                        dataGridView1.DataSource = table;
                    }
                }
            }
        }

        private void Material_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                connection.Open();
               
                string sqlQuery = @"
                SELECT        dbo.Materials.[Наименование материала], dbo.Materials.Количество, dbo.Materials.Цена, dbo.Suppliers.фирма, dbo.Materials.[Дата поступления], dbo.Materials.Остаток
FROM            dbo.Materials INNER JOIN
                         dbo.Suppliers ON dbo.Materials.Поставщик = dbo.Suppliers.кодФирмы
ORDER BY dbo.Materials.Цена ASC
            ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                   
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);                      
                        dataGridView1.DataSource = table;
                    }
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string sqlQuery = @"
                SELECT        dbo.Materials.[Наименование материала], dbo.Materials.Количество, dbo.Materials.Цена, dbo.Suppliers.фирма, dbo.Materials.[Дата поступления], dbo.Materials.Остаток
FROM            dbo.Materials INNER JOIN
                         dbo.Suppliers ON dbo.Materials.Поставщик = dbo.Suppliers.кодФирмы
ORDER BY dbo.Materials.Цена DESC
            ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                connection.Open();

                string sqlQuery;

               
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    sqlQuery = @"
                                    SELECT        dbo.Materials.[Наименование материала], dbo.Materials.Количество, dbo.Materials.Цена, dbo.Suppliers.фирма, dbo.Materials.[Дата поступления], dbo.Materials.Остаток
FROM            dbo.Materials INNER JOIN
                         dbo.Suppliers ON dbo.Materials.Поставщик = dbo.Suppliers.кодФирмы
                    WHERE dbo.Materials.[Наименование материала] LIKE @name
                    
                ";
                }
                else 
                {
                    sqlQuery = @"
                                    SELECT        dbo.Materials.[Наименование материала], dbo.Materials.Количество, dbo.Materials.Цена, dbo.Suppliers.фирма, dbo.Materials.[Дата поступления], dbo.Materials.Остаток
FROM            dbo.Materials INNER JOIN
                         dbo.Suppliers ON dbo.Materials.Поставщик = dbo.Suppliers.кодФирмы
                   
                ";
                }

               
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    
                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        command.Parameters.AddWithValue("@name", textBox1.Text + "%");
                    }

                  
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        
                        dataGridView1.DataSource = table;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();

            if (excelApp != null)
            {

                Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];


                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    excelWorksheet.Cells[1, i + 1] = dataGridView1.Columns[i].Name;
                }


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            excelWorksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {

                            excelWorksheet.Cells[i + 2, j + 1] = "";
                        }
                    }
                }


                excelApp.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddMat q1 = new AddMat();q1.ShowDialog();
        }
    }
}
