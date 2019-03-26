using System.Collections;
using NUnit.Framework;

namespace Task.Tests
{
    [TestFixture]
    public class Tests
    {
        private LinqSamplesForTest _samples;

        [OneTimeSetUp]
        public void Setup()
        {
            // Arrange
            _samples = new LinqSamplesForTest();
        }

        [Test]
        public void Linq1()
        {
            // Act
            IEnumerable customers = _samples.Linq1(10000);

            int counter = 0;
            foreach (var customer in customers)
            {
                counter++;
            }

            // Assert
            Assert.AreEqual(38, counter);
        }

        [Test]
        public void Linq2()
        {
            // Act
            IEnumerable list = _samples.Linq2();

            string companyName = "Alfreds Futterkiste";
            string supplierName1 = "Test";
            string supplierName2 = "Heli Süßwaren GmbH & Co. KG";
            
            int counter = 0;
            foreach (object item in list)
            {
                string currentItem = item.ToString();
                if (currentItem.Contains(companyName) &&
                    (currentItem.Contains(supplierName1) ||
                     currentItem.Contains(supplierName2)))
                {
                    counter++;
                }
            }

            //Assert
            Assert.AreEqual(2, counter);
        }

        [Test]
        public void Linq3()
        {
            // Act
            IEnumerable countOfCustomers = _samples.Linq3(10000);

            int counter = 0;
            foreach (var customer in countOfCustomers)
            {
                counter++;
            }

            // Assert
            Assert.AreEqual(7, counter);
        }

        [Test]
        public void Linq4()
        {
            // Act
            IEnumerable list = _samples.Linq4();
            string dateTime = "8/25/1997 12:00:00 AM";
            bool finded = false;

            foreach (var item in list)
            {
                if (item.ToString().Contains("8/25/1997 12:00:00 AM"))
                {
                    finded = true;
                }
            }

            // Assert
            Assert.AreEqual(true,finded);
        }

        [Test]
        public void Linq6()
        {
            // Act
            IEnumerable list = _samples.Linq6();

            int counter = 0;
            foreach (var item in list)
            {
                counter++;
            }

            // Assert
            Assert.AreEqual(78,counter);
        }

        [Test]
        public void Linq9()
        {
            // Act
            IEnumerable list = _samples.Linq9();
            string city = "Berlin";
            string income = "4273.00";
            string intensivity = "6";

            bool finded = false;
            foreach (var item in list)
            {
                if (item.ToString().Contains(city) &&
                    item.ToString().Contains(income) &&
                    item.ToString().Contains(intensivity))
                {
                    finded = true;
                }
            }

            // Assert
            Assert.AreEqual(true, finded);
        }

        [Test]
        public void Linq10Month()
        {
            // Act
            IEnumerable list = _samples.Linq10("Month");

            bool result = false;

            foreach (var v in list)
            {
                if (v.ToString().Contains("Apr") &&
                    v.ToString().Contains("countOfOrders = 105"))
                {
                    result = true;
                }
            }

            //Assert
            Assert.AreEqual(true,result);
        }

        [Test]
        public void Linq10Year()
        {
            // Act
            IEnumerable list = _samples.Linq10("Year");

            bool result = false;

            foreach (var v in list)
            {
                if (v.ToString().Contains("1996") &&
                    v.ToString().Contains("countOfOrders = 152"))
                {
                    result = true;
                }
            }


            //Assert
            Assert.AreEqual(true,result);
        }

        [Test]
        public void Linq10YearAndMonth()
        {
            // Act
            IEnumerable list = _samples.Linq10("Year and Month");

            bool result = false;

            foreach (var v in list)
            {
                if (v.ToString().Contains("1996 - 07") &&
                    v.ToString().Contains("countOfOrders = 22"))
                {
                    result = true;
                }
            }

            //Assert
            Assert.AreEqual(true,result);
        }
    }
}