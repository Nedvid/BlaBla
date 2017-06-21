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
        private static List<UserData> users = new List<UserData>();

        public static string checker(string com)
        {
            List<string> command = seperate(com);
            List<string> param = new List<string>();


            switch (command[0])
            {
                case "0001": //logowanie
                    {
                        param.Add(command[1]);
                        param.Add(command[2]);

                        AddUser(command[1], command[3]);
                        update_users();
                        return "0001|" + LogIn(param);
                    }
                case "0010": //rejestracja
                    {
                        param.Add(command[1]);
                        param.Add(command[2]);
                        return "0010|" + Register(param);
                    }
                case "0011": //wylogowanie
                    {
                        param.Add(command[1]);
                        return "0011|" + LogOut(param);
                    }
                case "1001": //dodanie znajomego
                    {
                        param.Add(command[1]);
                        param.Add(command[2]);
                        return "1001|" + AddFriend(param);
                    }
                case "1010": //usunięcie kontaktu
                    {
                        param.Add(command[1]);
                        param.Add(command[2]);
                        return "1010|" + RemoveFriend(param);
                    }
                case "0100": //zmiana hasła
                    {
                        param.Add(command[1]);
                        param.Add(command[2]);
                        param.Add(command[3]);
                        return "0100|" + ChangePass(param);
                    }
                case "0101": //lista znajomych
                    {
                        param.Add(command[1]);
                        return "0101" + ShowFriends(param);
                    }
                case "1111": //lista online
                    {
                        return "1111" + UpdateOnline();
                    }
                case "1011": //zmiana statusu
                    {
                        ChangeStatus(command[1]);
                        return "1011| True";
                    }
                case "0111": //search
                    {
                        return "0111" + Search(command[1]);
                    }
                case "0110": //info o połączeniu
                    {
                        param.Add(command[1]); //kto
                        param.Add(command[2]); //do kogo
                        param.Add(command[3]); //czas
                        return "0110" + addVoiceHistory(param);
                    }
                case "1000": //historia połączeń
                    {
                        param.Add(command[1]);
                        return "1000" + ShowHistory(param[0]);
                    }
            }

            return "error";

        }

        //parsowanie komunikatu
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

        //rejestracja
        public static Boolean Register(List<string> param)
        {
            using (var db = new BlaBla_dbContext())
            {
                var newUser = new User();
                string login = param[0];

                int loginUnique = db.Users.Where(x => x.Login == login).Count();
                if (loginUnique == 0)
                {
                    newUser.Login = login;
                    newUser.Password = param[1];
                    newUser.Status = false;

                    db.Users.Add(newUser);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        //FAIL
                        return false;
                    }
                }
                else
                {
                    //FAIL
                    return false;
                }
            }
            //OK
            return true;

        }

        //logowanie
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
                        //Console.WriteLine("FAIL1");
                        //FAIL
                        return false;
                    }
                }
                else
                {
                    //Console.WriteLine("FAIL2");
                    //FAIL
                    return false;
                }
            }
            //OK
            //Console.WriteLine("OK");
        }

        //wylogowanie
        public static string LogOut(List<string> param)
        {
            string login = param[0];
            UserData u = new UserData();
            u.login = login;

            RemoveUser(login);
            update_users();

            return "true";

        }

        //dodawanie znajomego
        public static Boolean AddFriend(List<string> param)
        {
            string login1 = param[0]; //login1
            string login2 = param[1]; //login2
            using (var db = new BlaBla_dbContext())
            {
                var acquaintance = new Friendship();
                //find users IDs
                var userID1 = db.Users.Where(x => x.Login == login1).Select(x => x.Id_User).FirstOrDefault();
                if (userID1 != 0)
                {
                    var userID2 = db.Users.Where(x => x.Login == login2).Select(x => x.Id_User).FirstOrDefault();
                    if (userID2 != 0)
                    {
                        var acq = db.Friendships.Where(x => x.Id_User1 == userID1 && x.Id_User2 == userID2).FirstOrDefault();
                        if (acq == null)
                        {
                            acquaintance.Id_User1 = userID1;
                            acquaintance.Id_User2 = userID2;
                            try
                            {
                                db.Friendships.Add(acquaintance);
                                db.SaveChanges();
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
                    else
                    {
                        Console.WriteLine("FAIL3");
                        //FAIL
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("FAIL4");
                    //FAIL
                    return false;
                }
            }
            //OK
            return true;
        }

        //usuwanie znajomego
        public static Boolean RemoveFriend(List<string> param)
        {
            string login1 = param[0]; //login1
            string login2 = param[1]; //login2

            using (var db = new BlaBla_dbContext())
            {
                var id_user1 = db.Users.Where(x => x.Login == login1).Select(x => x.Id_User).FirstOrDefault();
                var id_user2 = db.Users.Where(x => x.Login == login2).Select(x => x.Id_User).FirstOrDefault();
                Friendship friendship;
                Friendship friendship2;
                int tmp = 0;

                friendship = db.Friendships.Where(x => x.Id_User1 == id_user1 && x.Id_User2== id_user2).FirstOrDefault();
                if (friendship != null)
                {
                    db.Friendships.Remove(friendship);
                    db.SaveChanges();
                    tmp++;
                }

                friendship2 = db.Friendships.Where(x => x.Id_User1 == id_user2 && x.Id_User2 == id_user1).FirstOrDefault();
                if (friendship2 != null)
                {
                    db.Friendships.Remove(friendship2);
                    db.SaveChanges();
                    tmp++;
                }

                if (tmp > 0)
                    return true;
               
            }
            return false;
        }

        //lista znajomych
        public static string ShowFriends(List<string> param)
        {
            string login1 = param[0]; //login1
            string friends = "";
            using (var db = new BlaBla_dbContext())
            {
                var id_user1 = db.Users.Where(x => x.Login == login1).Select(x => x.Id_User).FirstOrDefault();
                List<Friendship> acquainstance;

                //sprawdzenie pierwszej połówki
                acquainstance = db.Friendships.Where(x => x.Id_User1 == id_user1).ToList();
                if (acquainstance != null)
                {
                    foreach (var item in acquainstance)
                    {
                        var login2 = db.Users.Where(x => x.Id_User == item.Id_User2).Select(x => x.Login).FirstOrDefault();
                        friends += "|" + login2.ToString();
                    }
                }

                //sprawdzenie drugiej połówki
                acquainstance = db.Friendships.Where(x => x.Id_User2 == id_user1).ToList();
                if (acquainstance != null)
                {
                    foreach (var item in acquainstance)
                    {
                        var login2 = db.Users.Where(x => x.Id_User == item.Id_User1).Select(x => x.Login).FirstOrDefault();
                        friends += "|" + login2.ToString();
                    }
                }

            }
            return friends;
        }

        //zmiana hasła
        public static Boolean ChangePass(List<string> param)
        {
            string login = param[0];
            string oldpass = param[1];
            string newpass = param[2];

            //sprawdzenie czy haslo stare poprawne
            using (var db = new BlaBla_dbContext())
            {
                var user = db.Users.Where(x => x.Login == login && x.Password == oldpass).FirstOrDefault();

                if (user != null)
                {
                    user.Password = newpass;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        //dodanie info o połączeniu
        public static Boolean addVoiceHistory(List<string> param)
        {
            string caller = param[0];
            string receiver = param[1];
            TimeSpan duration = TimeSpan.Parse(param[2]);
            DateTime date = DateTime.Now;

            using (var db = new BlaBla_dbContext())
            {
                VoiceHistory vh = new VoiceHistory();
                vh.Id_Caller = db.Users.Where(x => x.Login == caller).Select(x => x.Id_User).FirstOrDefault();
                vh.Id_Receiver = db.Users.Where(x => x.Login == receiver).Select(x => x.Id_User).FirstOrDefault();
                vh.Duration = duration;
                vh.CallDate = date;
                db.VoiceHistories.Add(vh);
                db.SaveChanges();
            }
            return true;
        }

        //historia połączeń
        public static string ShowHistory(string login)
        {
            string calls_string = "";

            using (var db = new BlaBla_dbContext())
            {
                var id_user = db.Users.Where(x => x.Login == login).Select(x => x.Id_User).FirstOrDefault();
                List<VoiceHistory> calls;

                calls = db.VoiceHistories.Where(x => x.Id_Caller == id_user || x.Id_Receiver==id_user).ToList();
                if (calls != null)
                {
                    foreach (var item in calls)
                    {
                        string caller = db.Users.Where(x => x.Id_User == item.Id_Caller).Select(x => x.Login).FirstOrDefault();
                        string receiver = db.Users.Where(x => x.Id_User == item.Id_Receiver).Select(x => x.Login).FirstOrDefault();

                        calls_string += "|" + caller;
                        calls_string += "|" + receiver;
                        calls_string += "|" + item.Duration;
                        calls_string += "|" + String.Format("{0:dd/MM/yy}", item.CallDate);
                    }
                }

            }
            return calls_string;
        }

        //update online
        public static string UpdateOnline()
        {
            string online = "";
            foreach (var item in users)
            {
                if (item.status == true)
                {
                    online += "|" + item.login + "|" + item.ip_address;
                }
            }
            return online;
        }

        //search
        public static string Search(string search)
        {
            string wynik = "";

            using (var db = new BlaBla_dbContext())
            {
                var list_user = db.Users.ToList();

                foreach (var item in list_user)
                {
                    if (item.Login.Contains(search))
                    {
                        wynik += "|" + item.Login;  
                    }
                }
            }

            return wynik;
        }

        //dodanie użytkownika do listy po zalogowaniu
        public static void AddUser(string login, string ip)
        {
            if (!check(login))
            {
                UserData item = new UserData();
                item.login = login;
                item.ip_address = ip;
                item.status = true;
                users.Add(item);
            }
        }

        //zmiana statusu
        public static void ChangeStatus(string login)
        {
            foreach(var item in users)
            {
                if (item.login == login)
                {
                    if (item.status == true)
                        item.status = false;
                    else 
                        item.status = true;
                }
            }
        }

        //usuwanie użytkownika z listy
        public static void RemoveUser(string login)
        {
            int tmp = 0;

            foreach (var item in users)
            {
                if (item.login == login)
                {
                    users.RemoveAt(tmp);
                    break;
                }
                tmp++;
            }
        }

        //sprawdzenie czy użytkonik już jest na liście
        public static bool check(string login)
        {
            foreach (var item in users)
            {
                if (item.login == login)
                {
                    return true;
                }
            }
            return false;
        }

        public static void update_users()
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;

            int tmp = 0;
            foreach (var item in users)
            {
                tmp++;
            }

            Console.SetCursorPosition(0, 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("USERS: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(tmp);
            Console.ResetColor();

            Console.SetCursorPosition(left, top);
        }
    }




}
