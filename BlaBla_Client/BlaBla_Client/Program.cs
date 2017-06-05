using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlaBla_Client.Forms;

namespace BlaBla_Client
{
    static class Program
    {
        public static string login { get; set; }
        public static Boolean status { get; set; }
        public static List<Friend> Friends { get; set; }

        [STAThread]
        static void Main()
        {
            Friends = new List<Friend>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Connect());
        }
    }
}
