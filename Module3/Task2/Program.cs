using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "80649";
            var intConverter = new IntConverter();

            try
            {
                Console.WriteLine(intConverter.ConvertStringToInt(str));
            }
            catch (ArgumentException a)
            {
                Console.WriteLine(a);
            }
            catch (OverflowException o)
            {
                Console.WriteLine(o);
            }
            

            Console.ReadKey();
        }
    }
}
