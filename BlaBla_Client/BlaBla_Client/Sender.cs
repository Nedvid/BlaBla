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

        public static Boolean changepass(string login, string oldpass, string newpass)
        {
            string oldpass_hash = SHA.hashBytePassHex(oldpass);
            string newpass_hash = SHA.hashBytePassHex(newpass);

            Connection.data = "0100|" + login + "|" + oldpass_hash + "|" + newpass_hash;

            if (Connection.send(Connection.data) == "0100|True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> search (string param)
        {
            Connection.data = "0111|" + param;
            string answer = Connection.send(Connection.data);
            List<string> wynik = seperate(answer);
            wynik.RemoveAt(0);

            return wynik;
        }

        public static Boolean add_friend(string login, string friend)
        {
            Connection.data = "1001|" + login + "|" + friend;

            if (Connection.send(Connection.data) == "1001|True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean remove_friend(string login, string friend)
        {
            Connection.data = "1010|" + login + "|" + friend;

            if (Connection.send(Connection.data) == "1010|True")
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
            List<string> List = seperate(answer);
            
            //wyczyszczenie pól statusu i ip
            foreach (var item in Program.Friends)
            {
                item.status = false;
                item.ip = "";
            }

            for (int i=1; i<List.Count; i++)
            {
                foreach (var item in Program.Friends)
                {
                    if (item.login == List[i])
                    {
                        item.ip = List[i + 1];
                        item.status = true;
                    }
                }
            }
        }

        public static void ChangeStatus ()
        {
            Connection.data = "1011|" + Program.login;

            Connection.send(Connection.data);
            
        }

        public static List<string> seperate(string txt)
        {
            string[] tmp = txt.Split('|');
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
