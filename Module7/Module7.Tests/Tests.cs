using System.Collections.Generic;
using System.Linq;
using Module7.DAO;
using Module7.Entities;
using NUnit.Framework;

namespace Module7.Tests
{
    [TestFixture]
    public class Tests
    {
        private BookDAO _dao;
        private List<Book> _books;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _dao = new BookDAO();
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            _books = new List<Book>
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
            _dao.InsertMany(_books);
        }

        [Test]
        public void Test1()
        {       
            // Assert
            Assert.AreEqual(5, _dao.GetAll().Count());
        }

        [Test]
        public void Test2And2AAnd2D()
        {
            // Assert
            Assert.AreEqual(4, _dao.SelectIfCountMoreThan(1).Count());
        }

        [Test]
        public void Test2C()
        {
            // Assert
            Assert.AreEqual(3, _dao.GetTop(3).Count());
            
        }

        [TestCase("Kolobok", SortBy.Descending)]
        [TestCase("Dyadya Stiopa", SortBy.Ascending)]
        [Test]
        public void Test3(string expectedName, SortBy parameter)
        {
            // Act
            Book book = _dao.GetByCount(parameter);

            // Assert
            Assert.AreEqual(expectedName, book.Name);
        }

        [Test]
        public void Test4()
        {
            // Assert
            Assert.AreEqual(2, _dao.GetListOfAuthors().Count());
        }

        [Test]
        public void Test5()
        {
            // Assert
            Assert.AreEqual(2, _dao.GetBooksWithoutAuthor().Count());
        }

        [TestCase(new int[]{ 6, 4, 11, 11, 2})]
        [Test]
        public void Test6(int[] countsAfterIncrement)
        {
            // Act
            _dao.IncrementTheCountOfEachBook();
            IEnumerable<Book> allBooksAfterIncrement = _dao.GetAll();
            List<int> countsBeforeIncrement = allBooksAfterIncrement.Select(book => book.Count).ToList();

            // Assert
            Assert.AreEqual(true, countsBeforeIncrement.SequenceEqual(countsAfterIncrement));
        }

        [TestCase("fantasy", "favority", 2)]
        [Test]
        public void Test7(string existingGenre, string additionalGenre, int expectedResult)
        {
            // Act
            _dao.AddNewGenreIfPointedGenreExist(existingGenre, additionalGenre);
            int count = _dao.GetAll().Count(book => book.Genre.Any(genre => genre.Equals(existingGenre)));

            // Assert
            Assert.AreEqual(expectedResult, count);
        }

        [TestCase("fantasy", "favority")]
        [Test]
        public void Test7B(string existingGenre, string additionalGenre)
        {
            // Arrange
            _dao.AddNewGenreIfPointedGenreExist(existingGenre, additionalGenre);

            // Act
            _dao.AddNewGenreIfPointedGenreExist(existingGenre, additionalGenre);
            bool result = false;
            foreach (Book book in _dao.GetAll())
            {
                int countOfAdditionalGenre = book.Genre.Count(genre => genre == additionalGenre);

                result = countOfAdditionalGenre > 1;

                if (result)
                {
                    break;
                }
            }

            // Assert
            Assert.AreEqual(false, result);
        }

        [TestCase(3, 4)]
        [Test]
        public void Test8(int number, int expectedCount)
        {
            // Act
            _dao.DeleteIfTheCountLessThan(number);

            // Assert
            Assert.AreEqual(expectedCount, _dao.GetAll().Count());
        }

        [Test]
        public void Test9()
        {
            // Act
            _dao.Delete();

            // Assert
            Assert.AreEqual(0, _dao.GetAll().Count());
        }

        [TearDown]
        public void ClearCollection()
        {
            _dao.Delete();
        }
    }
}