using Sire.Data.Dto.UserMgt;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace Sire.Common
{
    public class EncryptDecrypt
    {
        public static void Main(string[] args)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                UserDto userdto = new UserDto();
                string hash = GetMd5HashWithMySecurityAlgo(md5Hash, userdto.Password);
                Console.WriteLine("This is MySecurityAlgo using MD5 hash of " + userdto.Password + " is: " + hash + ".");
                Console.WriteLine("Verifying the hash with MySecurityAlgo...");
                if (VerifyMd5HashWithMySecurityAlgo(md5Hash, userdto.Password, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
            }
        }
         public static string GetMd5HashWithMySecurityAlgo(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.  
            return sBuilder.ToString();
        }
        // Verify a hash against a string.  
        public static bool VerifyMd5HashWithMySecurityAlgo(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.  
            string hashOfInput = GetMd5Hash(md5Hash, input);
            // Create a StringComparer an compare the hashes.  
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            using MD5 md5Hashs= MD5.Create();
            byte[] data = md5Hashs.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
