using System;
using System.Text;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Without exceptions handling
            //Console.WriteLine("Write a string, if you have finished - just write 'stop' and press Enter");
            //Console.WriteLine();
            //StringBuilder sb = new StringBuilder();

            //while (!sb.Equals("stop"))
            //{
            //    if (string.IsNullOrEmpty(sb.ToString()) ||
            //        string.IsNullOrWhiteSpace(sb.ToString()))
            //    {
            //        sb.Clear();
            //        sb.Append("Please enter anything");
            //        Console.WriteLine(sb);
            //        sb.Clear();
            //    }
            //    else
            //    {
            //        Console.WriteLine(sb[0]);
            //        sb.Clear();
            //    }

            //    sb.Append(Console.ReadLine());
            //}


            //With exception handling
            Console.WriteLine("Write a string, if you have finished - just write 'stop' and press Enter");
            Console.WriteLine();
            StringBuilder sb = new StringBuilder();
            while (!sb.Equals("stop"))
            {
                try
                {
                    sb.Append(Console.ReadLine());
                    if (sb.Equals("stop"))
                    {
                        continue;
                    }

                    Console.WriteLine(sb[0]);
                    sb.Clear();
                }
                catch (IndexOutOfRangeException indexOutOfRangeException)
                {
                    var ex = new EmptyStringException("You enter an empty string, please try one more time", indexOutOfRangeException);
                    Console.WriteLine("{0} - {1}", ex.GetType(), ex.Message);
                }
            }
        }
    }
}
