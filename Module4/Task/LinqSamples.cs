// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using SampleSupport;
using Task.Data;

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{
		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("Give a list of all customers whose total turnover more than X")]
		public void Linq1()
		{
            decimal X = 10000;

            var customers = dataSource.Customers
                .Where(c => c.Orders
                                .Sum(o => o.Total) > X);

            foreach (Customer customer in customers)
            {
                Console.WriteLine(@"Company name = {0};  Sum of orders = {1}",
                    customer.CompanyName,
                    customer.Orders.Sum(o => o.Total));
            }
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("Make a list of suppliers in the same country and city for each customer")]
		public void Linq2()
		{            
            Console.WriteLine(@"Without grouping");
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

            foreach (var item in list)
            {
                Console.WriteLine(@"Company name = {0};     supplier name = {1};Country = {2};City = {3}",
                    item.CompanyName,
                    item.SupplierName,
                    item.Country,
                    item.City);
            }

            Console.WriteLine();
            Console.WriteLine(@"With grouping");
            foreach (var item in list.GroupBy(item => new { item.Country, item.City }))
            {
                Console.WriteLine(@"{0}, {1}",
                    item.Key.Country,
                    item.Key.City);
                foreach (var i in item)
                {
                    Console.WriteLine("\tCompany name = {0};     supplier name = {1};Country = {2};City = {3}",
                        i.CompanyName,
                        i.SupplierName,
                        i.Country,
                        i.City);
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 3")]
        [Description("All customers who have had orders greater than the sum of X")]
        public void Linq3()
        {
            decimal X = 10000;

            var customers = dataSource.Customers
                .Where(c => c.Orders.Any(o => o.Total > X));

            foreach (Customer customer in customers)
            {
                Console.WriteLine("Company name = " + customer.CompanyName);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("All customers with their first order's month and year")]
        public void Linq4()
        {
            var list = dataSource.Customers
                .Where(customer => customer.Orders.Length > 0)
                .Select(customer => new
                {
                    companyName = customer.CompanyName,
                    firstOrderDate = customer.Orders.Min(o => o.OrderDate)
                });

            foreach (var item in list)
            {
                Console.WriteLine("Company name = {0};Firs order's date = {1}",
                    item.companyName,
                    item.firstOrderDate);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 5")]
        [Description("Task 4 + order by year, month, sum of orders, company name")]
        public void Linq5()
        {
            var list = dataSource.Customers
                .Where(customer => customer.Orders.Length > 0)
                .Select(customer => new
                {
                    companyName = customer.CompanyName,
                    firstOrderDate = customer.Orders.Min(order => order.OrderDate),
                    sumOfOrders = customer.Orders.Sum(order => order.Total)
                })
                .OrderBy(item => item.firstOrderDate)
                .ThenByDescending(item => item.sumOfOrders)
                .ThenBy(item => item.companyName);

            foreach (var item in list)
            {
                ObjectDumper.Write(item);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("All customers who don't have a region or have an incorrect postal code format or incorrect operator's code")]
        public void Linq6()
        {
            var list = dataSource.Customers.Where(
                    customer => customer.PostalCode != null && customer.PostalCode.All(char.IsDigit)
                    || string.IsNullOrEmpty(customer.Region)
                    || !customer.Phone.StartsWith("("));

            foreach (var item in list)
            {
                Console.WriteLine("Company name={0};Postal code={1};Region={2};Phone={3}",
                    item.CompanyName,
                    item.PostalCode,
                    item.Region,
                    item.Phone);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 7")]
        [Description("Group products by categories then by units in stock then order by price")]
        public void Linq7()
        {
            var list = dataSource.Products
                .GroupBy(product => product.Category, (category, product) => new
                {
                    category,
                    unitsInStock = product.GroupBy(item => item.UnitsInStock, (count, products) => new
                    {
                        count,
                        products = products.OrderByDescending(item => item.UnitPrice).ToList()
                    }).ToList()
                }).ToList();

            foreach (var item in list)
            {
                ObjectDumper.Write(item.category);
                foreach (var innerItem in item.unitsInStock)
                {
                    foreach (var product in innerItem.products)
                    {
                        ObjectDumper.Write(product);
                    }
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 8")]
        [Description("Group products by group like 'cheap', 'average', 'expensive'")]
        public void Linq8()
        {
            decimal average = 50;
            decimal expensive = 100;

            var list = dataSource.Products
                .GroupBy(product => product.UnitPrice < average ? "cheap" :
                         product.UnitPrice >= average && product.UnitPrice < expensive ? "average" :
                         "expensive",
                    (category, products) => new
                    {
                        category,
                        products = products.OrderBy(product => product.UnitPrice)
                    });

            foreach (var item in list)
            {
                ObjectDumper.Write(item.category);
                foreach (var product in item.products)
                {
                    ObjectDumper.Write(product);
                }
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 9")]
        [Description("Count average order's sum of each city and average intensity of clients")]
        public void Linq9()
        {
            var list = dataSource.Customers.GroupBy(
                customer => customer.City,
                (city, customers) => new
            {
                city,
                income = customers.Average(customer => customer.Orders.Sum(order => order.Total)),
                intensivity = customers.Average(customer => customer.Orders.Length)
            });

            foreach (var item in list)
            {
                ObjectDumper.Write(item);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 10")]
        [Description("Show activity statistic of clients by month, by year and by year and month")]
        public void Linq10()
        {
            var list = dataSource.Customers.SelectMany(customer => customer.Orders)
                .GroupBy(order => order.OrderDate.ToString("MMM"),
                    (month, orders) => new
                {
                    month,
                    countOfOrders = orders.Count()
                }).OrderBy(arg => arg.month);

            Console.WriteLine("Month");
            foreach (var item in list)
            {
                ObjectDumper.Write(item);
            }


            var list2 = dataSource.Customers.SelectMany(c => c.Orders)
                .GroupBy(order => order.OrderDate.ToString("yyyy"),
                    (year, orders) => new
                {
                    year,
                    countOfOrders = orders.Count()
                }).OrderBy(arg => arg.year);

            Console.WriteLine();
            Console.WriteLine("Year");
            foreach (var item in list2)
            {
                ObjectDumper.Write(item);
            } 


            var list3 = dataSource.Customers.SelectMany(customer => customer.Orders)
                .GroupBy(order => new
                {
                    year = order.OrderDate.ToString("yyyy"),
                    month = order.OrderDate.ToString("MM")

                }, (dateTime, orders) => new
                    {
                        dateTime = $"{dateTime.year} - {dateTime.month}",
                        countOfOrders = orders.Count()
                    }).OrderBy(arg => arg.dateTime);

            Console.WriteLine();
            Console.WriteLine("Year and Month");
            foreach (var item in list3)
            {
                ObjectDumper.Write(item);
            }                       
        }
	}
}
