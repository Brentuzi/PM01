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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Postupinskii
{
    public partial class Zakaz : Form
    {
        string connectionString = "Data Source=C41213\\SQLEXPRESS;Initial Catalog=BD_Postupinskii;Integrated Security=True";
        bool log = false;
        public Zakaz(bool log)
        {
            InitializeComponent();
            this.log = log;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();


                string sqlQuery = @"
                SELECT        dbo.Zakaz.кодЗаказа, dbo.Materials.[Наименование материала], dbo.Zakaz.количество, dbo.Suppliers.фирма, dbo.Zakaz.датаЗаказа
FROM            dbo.Zakaz INNER JOIN
                         dbo.Materials ON dbo.Zakaz.товар = dbo.Materials.КодМатериала INNER JOIN
                         dbo.Suppliers ON dbo.Zakaz.поставщик = dbo.Suppliers.кодФирмы AND dbo.Materials.Поставщик = dbo.Suppliers.кодФирмы
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

        private void Zakaz_Load(object sender, EventArgs e)
        {
           
        }
    }
}
