using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlaBla_Client.Forms
{
    public partial class Talk : CustomForm.MyForm
    {
        private Timer timer;
        private Stopwatch sw;
        public  string user { get; set; }
        public Boolean OnCall { get; set; }

        public Talk()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.OnCall = true;

            timer = new Timer();
            timer.Interval = (1000);
            timer.Tick += new EventHandler(timer_Tick);
            sw = new Stopwatch();
            timer.Start();
            sw.Start();

            


            label2.Text = user;          
        }

        private void timer_Tick(object sender, EventArgs e)
        { 
            label3.Text = sw.Elapsed.ToString(@"hh\:mm\:ss");
            Application.DoEvents();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OnCall = false;
            this.Hide();
        }
    }
}
