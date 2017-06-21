using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlaBla_Client.Forms
{
    public partial class History : CustomForm.MyForm
    {
        public History()
        {
            InitializeComponent();

            List<string> string_data = Sender.History(Program.login);

            if(string_data.Count>2)
            {
                for (int i = 0; i < string_data.Count; i = i + 4)
                {
                    this.dataGridView1.Rows.Add(string_data[i], string_data[i + 1], string_data[i + 2], string_data[i + 3]);
                }
            }
            
            
        }

    }
}
