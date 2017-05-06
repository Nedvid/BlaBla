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


        static void Main(string[] args)
        {

            Connection c = new Connection();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

