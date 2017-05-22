using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace BlaBla_Server
{
    class UserData
    {
        public UserData()
        {

        }

        public string login { get; set; }
        public string ip_address { get; set; }
        public Boolean status { get; set; } //dostepny 1
    }
}
