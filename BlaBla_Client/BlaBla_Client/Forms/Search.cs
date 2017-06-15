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
    public partial class Search : CustomForm.MyForm
    {

        public Search()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Szukaj_Click(object sender, EventArgs e)
        {
            List<string> wynik = Sender.search(textBox1.Text);
            listBox1.Items.Clear();
            foreach (var item in wynik)
            {
                listBox1.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && Sender.add_friend(Program.login, listBox1.SelectedItem.ToString()))
            {
                Program.Friends = new List<Friend>();
                Sender.ShowFriends(Program.login);
                MessageBox.Show("Dodano do kontaktów!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show("Błąd!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
