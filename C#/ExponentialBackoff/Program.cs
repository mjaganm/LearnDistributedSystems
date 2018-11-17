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

namespace ExponentialBackoff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
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

        private static int PowerOfTwo(int power)
        {
            if (power <= 0)
            {
                return 1;
            }

            if (power > 30)
            {
                return PowerOfTwo(30);
            }

            int result = 1;
            for (int i = power; i > 0; i--)
            {
                result *= 2;
            }

            return result;
        }

        static void Main(string[] args)
        {
            List<string> listOfDocs = new List<string>();

            int numOfExceptions = 0;
            for (int i = 0; i < 999999; i++)
            {
                try
                {
                    listOfDocs.Add(GetRandomString(9999999));
                }
                catch(Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Exception: {0}", ex);

                    // Exponential backoff
                    int waitTimeInMilliSeconds = PowerOfTwo(numOfExceptions) * 10;
                    numOfExceptions++;
                    Console.WriteLine("Waiting for {0} milliseconds", waitTimeInMilliSeconds);
                    Thread.Sleep(waitTimeInMilliSeconds);
                }
            }

            Console.WriteLine("Done filling up memory!");
            Console.ReadKey();
        }
    }
}
