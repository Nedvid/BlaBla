using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BlaBla_Server
{
    class SHA
    {
        public static string hashBytePassHex(string password)
        {
            byte[] bytePasswd = Encoding.Default.GetBytes(password);
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytePasswd = sha256.ComputeHash(bytePasswd); //256-bits
                string hashBytePasswdHex = BitConverter.ToString(hashBytePasswd).Replace("-", string.Empty);//256 bit hash password
                return hashBytePasswdHex;
            }
        }

    }
}
