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
    public partial class Auth : Form
    {
        bool log = false;
        public Auth(bool log)
        {
            InitializeComponent();
            this.log = log;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string login, password;

            login = Login.Text;
            password = Pass.Text;

            if (login == "IIPetrov2021@mail.ru" && password == "isP1")
            {
                log = true;
                MessageBox.Show("Успешно", "авторизованы", MessageBoxButtons.OK);
                this.Hide();
                Form1 q1 = new Form1(log);
                q1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверный пароль", "Попробуйте еще раз", MessageBoxButtons.OK);
            }
        }
    }
}
