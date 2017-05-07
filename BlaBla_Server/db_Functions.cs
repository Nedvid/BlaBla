using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace BlaBla_Server
{
    class db_Functions
    {
        public static string checker (string com)
        {
            List<string> command = seperate(com);
            List<string> param = new List<string>();

            switch (command[0])
            {
                case "0001":
                    {
                        param.Add(command[1]);
                        param.Add(command[2]);
                        return "0001|"+LogIn(param);
                        break;
                    }
            }

            return "error";

        }

        public static List<string> seperate (string txt)
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

        public static void Register(List<string> param)
        {
            using (var db = new BlaBla_dbContext())
            {
                var newUser = new User();
                string login = param[0];

                int loginUnique = db.Users.Where(x => x.Login == login).Count();
                if (loginUnique == 0)
                {
                    newUser.Login = login;
                    newUser.Password = SHA.hashBytePassHex(param[1]);
                    newUser.Status = false;

                    db.Users.Add(newUser);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        Console.WriteLine("FAIL1");
                        //FAIL
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("FAIL2");
                    //FAIL
                    return;
                }
            }
            //OK
            Console.WriteLine("OK");

        }

        public static Boolean LogIn(List<string> param)
        {
            string login = param[0];
            string password = param[1];

            using (var db = new BlaBla_dbContext())
            {
                var userToEdit = db.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
                if (userToEdit != null)
                {
                    userToEdit.Status = true;
                    try
                    {
                        db.Entry(userToEdit).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        Console.WriteLine("FAIL1");
                        //FAIL
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("FAIL2");
                    //FAIL
                    return false;
                }
            }
            //OK
            Console.WriteLine("OK");
        }

        public static void LogOut(List<string> param)
        {
            string login = param[0];

            using (var db = new BlaBla_dbContext())
            {
                var userToLogOut = db.Users.Where(x => x.Login == login).FirstOrDefault();
                if (userToLogOut != null)
                {
                    userToLogOut.Status = false;
                    try
                    {
                        db.Entry(userToLogOut).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        Console.WriteLine("FAIL1");
                        //FAIL
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("FAIL2");
                    //FAIL
                    return;
                }
                //OK
                Console.WriteLine("OK");
            }
        }




    }
}
