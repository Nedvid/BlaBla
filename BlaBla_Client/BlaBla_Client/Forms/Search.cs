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
    public partial class Search : Form
    {
        //Grafa
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

        public Search()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
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
