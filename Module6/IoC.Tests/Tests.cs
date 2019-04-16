using System.Reflection;
using CustomerLibrary;
using IoC;
using NUnit.Framework;

namespace Ioc.Tests
{
    public class Tests
    {
        [Test]
        public void CreateCustomerBLLInstance()
        {
            // Arrange
            Container container = new Container();
            container.AddAssembly(Assembly.LoadFrom("CustomerLibrary.dll"));
            
            // Act
            bool result = container.CreateInstance(typeof(CustomerBLL)) is CustomerBLL;

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateCustomerBLL2Instance()
        {
            // Arrange
            Container container = new Container();
            container.AddAssembly(Assembly.LoadFrom("CustomerLibrary.dll"));
            
            // Act
            bool result = container.CreateInstance<CustomerBLL2>() is CustomerBLL2;;

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}