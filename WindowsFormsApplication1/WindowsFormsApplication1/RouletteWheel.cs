using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace roulette
{
	class RouletteWheel
	{
        static Random rnd = new Random();
        static MD5 hash = MD5.Create();
		public static int TurnWheel()
		{
			return rnd.Next(36);
		}
		public static string Salt()
		{
			return RandomString(10);
		}
		public static string md5(string s)
		{
			return GetMd5Hash(hash, s);
		}

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        static string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[rnd.Next(_chars.Length)];
            }
            return new string(buffer);
        }
	}
}
