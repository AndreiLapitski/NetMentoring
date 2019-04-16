using System;
using System.Reflection;
using CustomerLibrary;
using IoC;

namespace Module6
{
    class Program
    {
        static void Main(string[] args)
        {
            Container container1 = new Container();
            container1.AddType(typeof(CustomerBLL));
            container1.AddType(typeof(Logger));
            container1.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            Container container2 = new Container();
            container2.AddAssembly(Assembly.LoadFrom("CustomerLibrary.dll"));

            CustomerBLL customerBLL = (CustomerBLL) container1.CreateInstance(typeof(CustomerBLL));
            CustomerBLL2 customerBLL2 = container2.CreateInstance<CustomerBLL2>();

            Console.WriteLine("Works!");
            Console.ReadKey();
        }
    }
}
