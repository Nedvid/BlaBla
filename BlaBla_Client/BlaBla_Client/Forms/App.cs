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
using Ozeki.Media.MediaHandlers;
using Ozeki.VoIP;
using Ozeki.VoIP.SDK;
using System.Threading;
using System.Timers;


namespace BlaBla_Client.Forms
{
    public partial class App : Form
    {
        private Invite inv { get; set; }
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
       
        public App()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            this.CenterToScreen();

            inv = new Invite();
            Sender.ShowFriends(Program.login);
        }

     

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(Sender.logout(Program.login))
            {
                Connection.close();
                this.Close();
            }
            else
            {
                Connection.close();
                this.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            inv.invite_someone(textBox2.Text);
            richTextBox1.Text +=  "Calling to " + textBox2.Text;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           foreach (var item in Program.Friends)
            {
                if(listBox1.SelectedItem.ToString() == item.login)
                {
                    textBox2.Text = item.ip;
                }
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            Sender.UpdateOnline();
            listBox1.Items.Clear();

           
            string tmp = "DOSTĘPNI";

            listBox1.Items.Add(tmp);
            foreach (var item in Program.Friends)
            {
                if (item.status == true)
                {
                    listBox1.Items.Add(item.login);
                }
            }

            tmp = "NIEDOSTĘPNI";
            listBox1.Items.Add(tmp);
            foreach (var item in Program.Friends)
            {
                if (item.status == false)
                {

                    listBox1.Items.Add(item.login);
                }
            }
        }
    }
}
