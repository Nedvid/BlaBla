using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BlaBla_Client
{
    class Sender
    {

        public static Boolean login (string login, string password)
        {
            string hash = SHA.hashBytePassHex(password);
            string ip = GetLocalIPAddress();
            Connection.data = "0001|" + login + "|" + hash + "|" + ip;

            if (Connection.send(Connection.data) == "0001|True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean register (string login, string password)
        {
            string hash = SHA.hashBytePassHex(password);
            Connection.data = "0010|" + login + "|" + hash;

            if (Connection.send(Connection.data) == "0010|True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean logout (string login)
        {
            Connection.data = "0011|" + login;

            if (Connection.send(Connection.data) == "0011|True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ShowFriends (string login)
        {
            Connection.data = "0101|" + login ;
            string answer = Connection.send(Connection.data);
            List<string> friends = seperate(answer);
            friends.RemoveAt(0);
            

            foreach(var item in friends)
            {
                Friend tmp = new Friend();
                tmp.login = item.ToString();
                Program.Friends.Add(tmp);
            }
        }

        public static void UpdateOnline ()
        {
            Connection.data = "1111|";
            string answer = Connection.send(Connection.data);
            List<string> all = seperate(answer);
            all.RemoveAt(0);
            List<string> online = new List<string>();
            List<string> ip = new List<string>();
            int tmp = 0;

            foreach (var item in all)
            {
                if (tmp%2==0)
                {
                    online.Add(item);
                }
                else
                {
                    ip.Add(item);
                }
                tmp++;
            }

            tmp = 0;
            foreach (var item in online)
            {
                foreach (var item2 in Program.Friends)
                {
                    if (item.ToString()==item2.login)
                    {
                        item2.status = true;
                        item2.ip = ip[tmp];
                        
                    }
                }
                tmp++;
            }
        }

        public static List<string> seperate(string txt)
        {
            char[] separators = { '|' };
            string[] tmp = txt.Split(separators);
            List<string> command = new List<string>();
            foreach (var item in tmp)
            {
                command.Add(item);
            }

            return command;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
