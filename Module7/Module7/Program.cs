using System;
using System.Collections.Generic;
using System.Linq;
using Module7.DAO;
using Module7.Entities;

namespace Module7
{
    class Program
    {
        static void Main(string[] args)
        {
            BookDAO dao = new BookDAO();
            List<Book> books = new List<Book>
            {
                new Book()
                {
                    Name = "Hobbit",
                    Author = "Tolkien",
                    Count = 5,
                    Genre = new string[]
                    {
                        "fantasy"
                    },
                    Year = 2014
                },

                new Book()
                {
                    Name = "Lord of the ring",
                    Author = "Tolkien",
                    Count = 3,
                    Genre = new string[]
                    {
                        "fantasy"
                    },
                    Year = 2015
                },

                new Book()
                {
                    Name = "Kolobok",
                    Count = 10,
                    Genre = new string[]
                    {
                        "kids"
                    },
                    Year = 2000
                },

                new Book()
                {
                    Name = "Repka",
                    Count = 10,
                    Genre = new string[]
                    {
                        "kids"
                    },
                    Year = 2000
                },

                new Book()
                {
                    Name = "Dyadya Stiopa",
                    Author = "Mihalkov",
                    Count = 1,
                    Genre = new string[]
                    {
                        "kids"
                    },
                    Year = 2001
                }
            };

            //1    
            //dao.InsertMany(books);
            //foreach (Book book in dao.GetAll())
            //{
            //    Console.WriteLine(book);
            //}

            //2a
            //IEnumerable<Book> booksWhereCountMoreThan = dao.SelectIfCountMoreThan(1);
            //foreach (Book book in booksWhereCountMoreThan)
            //{
            //    Console.WriteLine("Name={0};Count={1}", book.Name, book.Count);
            //}
            //2b
            //foreach (Book book in dao.GetAll().OrderBy(book => book.Name))
            //{
            //    Console.WriteLine(book.Name);
            //}
            //2c
            //foreach (Book book in dao.GetTop(3))
            //{
            //    Console.WriteLine(book.Name);
            //}
            //2d
            //Console.WriteLine("Number of books with count > 1 is " + dao.SelectIfCountMoreThan(1).ToList().Count);

            //3    
            //Book book1 = dao.GetByCount(SortBy.Descending);
            //Console.WriteLine("The book with max count({0}) - {1}", book1.Count, book1.Name);
            //Book book2 = dao.GetByCount(SortBy.Ascending);
            //Console.WriteLine("The book with min count({0}) - {1}", book2.Count, book2.Name);

            //4
            //foreach (string author in dao.GetListOfAuthors())
            //{
            //    Console.WriteLine(author);
            //}

            //5
            //foreach (Book book in dao.GetBooksWithoutAuthor())
            //{
            //    Console.WriteLine("{0}, author - {1}",
            //        book.Name,
            //        string.IsNullOrEmpty(book.Author) ? "Non" : book.Author);
            //}

            //6
            //foreach (Book book in dao.GetAll())
            //{
            //    Console.WriteLine("Name:{0};Count:{1}", book.Name, book.Count);
            //}
            //dao.IncrementTheCountOfEachBook();
            //Console.WriteLine();
            //foreach (Book book in dao.GetAll())
            //{
            //    Console.WriteLine("Name:{0};Count:{1}", book.Name, book.Count);
            //}

            //7
            //ShowGenres(dao.GetAll().ToList());
            //Console.WriteLine();
            //dao.AddNewGenreIfPointedGenreExist("fantasy", "favority");
            //ShowGenres(dao.GetAll().ToList());

            //8
            //dao.DeleteIfTheCountLessThan(3);

            //9
            //dao.Delete();

            //Console.ReadKey();
        }

        static void ShowGenres(List<Book> list)
        {
            foreach (Book book in list)
            {
                Console.Write(book.Name + ";");
                if (book.Genre.Length != 0)
                {
                    foreach (string genre in book.Genre)
                    {
                        Console.Write(genre + ";");
                    }
                }
                Console.Write("\n\r");
            }
        }
    }
}
