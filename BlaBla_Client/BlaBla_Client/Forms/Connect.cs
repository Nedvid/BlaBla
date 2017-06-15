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
using BlaBla_Client;

namespace BlaBla_Client.Forms
{
    public partial class Connect : CustomForm.MyForm
    {

        public Connect()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void button_login_Click_1(object sender, EventArgs e)
        {

            Connection.ip_server = textbox_ip.Text;

            if (Connection.runserver())
            {
                MessageBox.Show("Połączono!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Login login_form = new Login();
                login_form.Show();
            }
            else
            {
                MessageBox.Show("Serwer nie jest uruchomiony", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }

        private void textbox_ip_Validating(object sender, CancelEventArgs e)
        {
            if (textbox_ip.Text == string.Empty)
            {
                MessageBox.Show("Wpisz adres serwera!");
            }

            if (!ValidateIPv4(textbox_ip.Text))
            {
                MessageBox.Show("Adres jest nieprawidłowy!");
            }
        }

        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
