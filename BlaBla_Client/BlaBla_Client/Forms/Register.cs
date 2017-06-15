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

namespace BlaBla_Client.Forms
{
    public partial class Register : CustomForm.MyForm
    {

        public Register()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void texbox_password_again_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Connection.close();
            this.Close();
        }

        private void link_login_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login_form = new Login();
            login_form.Show();
        }

        private void label_text_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (Sender.register(textbox_login.Text, textbot_password.Text))
            {
                MessageBox.Show("Zarejestrowano!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Hide();
                Login login_form = new Login();
                login_form.Show();
            }
            else
            {
                MessageBox.Show("Taki użytkownik istnieje już w bazie.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
