using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Postupinskii
{
    public partial class AddMat : Form
    {
        string connectionString = "Data Source=C41213\\SQLEXPRESS;Initial Catalog=BD_Postupinskii;Integrated Security=True";
        bool log;

        public AddMat()
        {
            InitializeComponent();

        }

        private void AddMat_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_PostupinskiiDataSet1.Suppliers". При необходимости она может быть перемещена или удалена.
            this.suppliersTableAdapter.Fill(this.bD_PostupinskiiDataSet1.Suppliers);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_PostupinskiiDataSet.Materials". При необходимости она может быть перемещена или удалена.
            this.materialsTableAdapter.Fill(this.bD_PostupinskiiDataSet.Materials);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = @"
    INSERT INTO dbo.Materials ([Наименование материала], [Количество], [Цена], [Поставщик],[Дата поступления], [Остаток]) 
    VALUES (@1, @2, @3, @6, @4,@5)
";
                int a = 0;
                if (comboBox2.Text == "Иванов С.С.")
                {
                    a = 1;
                }
                else if (comboBox2.Text == "Федосеев А.Н.")
                {

                    a = 2;
                }
                else if (comboBox2.Text == "Бургун И.П.")
                {
                    a = 3;
                }
                else if (comboBox2.Text == "Петров Т.А.")
                {
                    a = 4;
                }

                else { 
                    a = 5; 
                }


                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    command.Parameters.AddWithValue("@1", comboBox1.Text);
                    command.Parameters.AddWithValue("@2", int.Parse(numericUpDown1.Text));
                    command.Parameters.AddWithValue("@3", float.Parse(numericUpDown1.Text));
                    command.Parameters.AddWithValue("@4", Convert.ToDateTime(dateTimePicker1.Text));
                    command.Parameters.AddWithValue("@5", int.Parse(numericUpDown3.Text));
                    command.Parameters.AddWithValue("@6", a);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Данные", "Добавлены", MessageBoxButtons.OK);
                }
            }
            this.Hide();
            Material q1 = new Material(log);
        }
    }
}
