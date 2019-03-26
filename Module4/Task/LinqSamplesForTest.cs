using System;
using System.Collections;
using System.Linq;
using Task.Data;

namespace Task
{
    public class LinqSamplesForTest
    {
        private DataSource dataSource = new DataSource();

        public IEnumerable Linq1(decimal x)
        {
            return dataSource.Customers
                .Where(c => c.Orders
                                .Sum(o => o.Total) > x);
        }

        public IEnumerable Linq2()
        {
            var list = from customer in dataSource.Customers
                from supplier in dataSource.Suppliers
                    .Where(supplier => customer.Country == supplier.Country && supplier.City == customer.City)
                select new
                {
                    customer.CompanyName,
                    supplier.SupplierName,
                    customer.Country,
                    supplier.City
                };

            return list;
        }

        public IEnumerable Linq3(decimal x)
        {
            return dataSource.Customers
                .Where(c => c.Orders.Any(o => o.Total > x));
        }

        public IEnumerable Linq4()
        {
            return dataSource.Customers
                .Where(customer => customer.Orders.Length > 0)
                .Select(customer => new
                {
                    companyName = customer.CompanyName,
                    firstOrderDate = customer.Orders.Min(o => o.OrderDate)
                });
        }

        public IEnumerable Linq6()
        {
            return dataSource.Customers.Where(
                customer => customer.PostalCode != null && customer.PostalCode.All(char.IsDigit)
                            || string.IsNullOrEmpty(customer.Region)
                            || !customer.Phone.StartsWith("("));
        }

        public IEnumerable Linq9()
        {
            return dataSource.Customers.GroupBy(
                customer => customer.City,
                (city, customers) => new
                {
                    city,
                    income = customers.Average(customer => customer.Orders.Sum(order => order.Total)),
                    intensivity = customers.Average(customer => customer.Orders.Length)
                });
        }

        public IEnumerable Linq10(string argument)
        {
            switch (argument)
            {
                case "Month":
                    return dataSource.Customers.SelectMany(customer => customer.Orders)
                        .GroupBy(order => order.OrderDate.ToString("MMM"),
                            (month, orders) => new
                            {
                                month,
                                countOfOrders = orders.Count()
                            }).OrderBy(arg => arg.month);

                case "Year":
                    return dataSource.Customers.SelectMany(c => c.Orders)
                        .GroupBy(order => order.OrderDate.ToString("yyyy"),
                            (year, orders) => new
                            {
                                year,
                                countOfOrders = orders.Count()
                            }).OrderBy(arg => arg.year);

                case "Year and Month":
                    return dataSource.Customers.SelectMany(customer => customer.Orders)
                        .GroupBy(order => new
                        {
                            year = order.OrderDate.ToString("yyyy"),
                            month = order.OrderDate.ToString("MM")

                        }, (dateTime, orders) => new
                        {
                            dateTime = $"{dateTime.year} - {dateTime.month}",
                            countOfOrders = orders.Count()
                        }).OrderBy(arg => arg.dateTime);

                default:
                    throw new ArgumentException("Incorrect argument");
            }
            
        }
    }
}
