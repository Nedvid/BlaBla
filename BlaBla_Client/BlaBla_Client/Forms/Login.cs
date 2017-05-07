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
    public partial class Login : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
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
            string hash = SHA.hashBytePassHex(textbox_password.Text);
            Connection.data ="0001|"+textbox_login.Text+ "|" + hash;
            Connection.send(Connection.data);
            Thread.Sleep(2000);
            if(Connection.sDataIncomming=="0001|True")
            {
                MessageBox.Show("Zalogowano!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show("Nie zalogowano!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


