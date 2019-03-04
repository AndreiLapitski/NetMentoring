using System;

namespace ConsoleDotNetCoreApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //1
            string name = args[0];
            Console.WriteLine($"Hello, {name}!");
            //3
            Console.WriteLine(SpeakerClassLibrary.Speaker.SayHelloNow(name));
            Console.ReadKey();
        }
    }
}
