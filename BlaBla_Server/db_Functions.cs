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
        static Dictionary<int, Delegate> Dictionary = new Dictionary<int, Delegate>(); 

        static void AddToDictionary()
        {
            Dictionary[0] = new Action<List<string>>(db_Functions.Register);
            Dictionary[1] = new Action<List<string>>(db_Functions.LogIn);
            Dictionary[2] = new Action<List<string>>(db_Functions.LogOut);
            //Dictionary[3] = new Action<List<string>>(db_Functions.AccDel);
            //Dictionary[4] = new Action<List<string>>(db_Functions.PassChng);
            //Dictionary[8] = new Action<List<string>>(db_Functions.AddFriend);
            //Dictionary[9] = new Action<List<string>>(db_Functions.DelFriend);
            //Dictionary[11] = new Action<List<string>>(db_Functions.CallState);
            //Dictionary[15] = new Action<List<string>>(db_Functions.Iam);
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

        public static void LogIn(List<string> param)
        {
            string login = param[0];
            string password = SHA.hashBytePassHex(param[1]);

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
