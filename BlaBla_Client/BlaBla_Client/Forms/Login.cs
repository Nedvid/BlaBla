using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace BlaBla_Client.Forms
{
    public partial class Login : CustomForm.MyForm
    {

        public Login()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void link_register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register register_form = new Register();
            register_form.Show();
        }

        private void label_text_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Connection.close();
            this.Close();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if(Sender.login(textbox_login.Text, textbox_password.Text))
            {
                MessageBox.Show("Zalogowano!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Hide();
                Program.login = textbox_login.Text;
                Program.status = true;
                App app_form = new App();
                app_form.Show();
            }
            else
            {
                MessageBox.Show("Nie zalogowano!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


