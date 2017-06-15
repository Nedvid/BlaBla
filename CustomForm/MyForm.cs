using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomForm
{
    public partial class MyForm : Form
    {
        public Border border;
        public MyForm()
        {
            InitializeComponent();
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            // bạn có thể thay đổi dòng này để có các hiệu ứng màu sắc khác nhau.
            border = new Border(this, Color.FromArgb(117, 183, 59), Color.White, true, true);   
        }
    }
}
