using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlaBla_Client.Forms
{
    public partial class Settings : CustomForm.MyForm
    {

        public Settings()
        {
            InitializeComponent();
            this.CenterToScreen();

            listBox1.Items.Clear();

            foreach (var item in Program.Friends)
            {
                listBox1.Items.Add(item.login);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (Sender.changepass(Program.login, textbox_oldpass.Text, textbox_newpass.Text))
            {
                MessageBox.Show("Hasło Zmienione", "Succes", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Hide();

            }
            else
            {
                MessageBox.Show("Błąd!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Sender.remove_friend(Program.login, listBox1.SelectedItem.ToString()))
            {
                Program.Friends = new List<Friend>();
                Sender.ShowFriends(Program.login);

                listBox1.Items.Clear();

                foreach (var item in Program.Friends)
                {
                    listBox1.Items.Add(item.login);
                }
                MessageBox.Show("Usunięto!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show("Błąd!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
