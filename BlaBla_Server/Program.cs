using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlaBla_Server
{
    class Program
    {
        public static void display_users()
        {
            using (var db = new BlaBla_dbContext())
            {
                // Display all Blogs from the database 
                var query = from b in db.Users
                            orderby b.Login
                            select b;

                Console.WriteLine("______________________USERS______________________");
                Console.WriteLine("Id \tLogin \tStatus \tHaslo ");

                foreach (var item in query)
                {
                    Console.WriteLine(item.Id_User + "\t" + item.Login + "\t" + item.Status + "\t" + item.Password);
                }
            }
        }

        public static void register(string login, string haslo)
        {
            List<string> user = new List<string>();
            user.Add(login);
            user.Add(haslo);

            db_Functions.Register(user);
        }

        public static void login(string login, string haslo)
        {
            List<string> user = new List<string>();
            user.Add(login);
            user.Add(haslo);

            db_Functions.LogIn(user);
        }

        public static void logout(string login)
        {
            List<string> user = new List<string>();
            user.Add(login);

            db_Functions.LogOut(user);
        }

        public static void addFriend (string login1, string login2)
        {
            List<string> cmd = new List<string>();
            cmd.Add(login1);
            cmd.Add(login2);

            Console.WriteLine(db_Functions.AddFriend(cmd));
        }

        public static void showFriends(string login1)
        {
            List<string> cmd = new List<string>();
            cmd.Add(login1);

            Console.WriteLine(db_Functions.ShowFriends(cmd));
        }

        public static void addcall (string caller, string receicer, string duration)
        {
            List<string> cmd = new List<string>();
            cmd.Add(caller);
            cmd.Add(receicer);
            cmd.Add(duration);

            Console.WriteLine(db_Functions.addVoiceHistory(cmd));
        }

        public static void showhistory(string login)
        {
            string calls = db_Functions.ShowHistory(login);
            Console.WriteLine(calls);
           
        }

        static void Main(string[] args)
        {

            db_Functions.update_users();
            Connection c = new Connection();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

