/**
 * 
 * MIT License
 * 
 * Copyright (c) 2018 Jagan Mohan Maddukuri
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 * */

namespace mjaganm.LearnDistributedSystems.BasicWebserver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    class BasicWebserver
    {
        private static Random rand = new Random();

        private const string AlphaNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GetRandomString(int length)
        {
            return new string(
                Enumerable.Repeat(
                    AlphaNum,
                    length)
                    .Select(s => s[rand.Next(s.Length)])
                    .ToArray());
        }

        public string GetRandomString(string length = "25")
        {
            int lengthint = 5;
            if (!Int32.TryParse(length, out lengthint))
            {
                Console.WriteLine("Unable to parse the given int string: {0} to Int32", length);
            }

            return GetRandomString(lengthint);
        }

        public string GetSHA256Hash(string content)
        {
            // Base condition
            if (string.IsNullOrEmpty(content))
            {
                return "EMPTYSTRING";
            }

            using (SHA256 sha256hash = SHA256.Create())
            {
                byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(content));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public int GetRandomInt(string start, string end)
        {
            int startint = 0;
            if (!Int32.TryParse(start, out startint))
            {
                Console.WriteLine("Unable to parse the given int string: {0} to Int32", start);
                return int.MinValue;
            }

            int endint = 0;
            if (!Int32.TryParse(end, out endint))
            {
                Console.WriteLine("Unable to parse the given int string: {0} to Int32", end);
                return int.MinValue;
            }

            return rand.Next(startint, endint);
        }
    }
}
