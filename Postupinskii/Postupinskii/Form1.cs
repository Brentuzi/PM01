using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Postupinskii
{
    public partial class Form1 : Form
    {
        bool log = false;
        public Form1(bool log)
        {
            InitializeComponent();
            this.log = log;
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (log == true) { label2.Visible = true; }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (log == false)
            {
                Auth q1 = new Auth(log);
                this.Hide();
                q1.ShowDialog();

            }
            else
            {
                MessageBox.Show("Повторная авторизация не требуеться", "авторизованы", MessageBoxButtons.OK);

            }
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Material q1 = new Material(log); q1.ShowDialog();
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zakaz q1 = new Zakaz(log);q1.ShowDialog();
        }

        private void складToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void контактыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contact q1 = new contact(); q1.Show();
        }
    }
}
