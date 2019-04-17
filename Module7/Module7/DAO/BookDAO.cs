using System.Collections.Generic;
using System.Linq;
using Module7.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Module7.DAO
{
    public enum SortBy { Ascending, Descending }
    public class BookDAO
    {
        private MongoClient _client;
        private IMongoCollection<Book> _bookCollection;

        public BookDAO()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = _client.GetDatabase("mydb");
            _bookCollection = database.GetCollection<Book>("book");
        }

        public void InsertMany(IEnumerable<Book> books)
        {
            _bookCollection.InsertMany(books);
        }

        public IEnumerable<Book> GetAll()
        {            
            return _bookCollection.AsQueryable().ToList();
        }

        public void Delete()
        {
            _bookCollection.Database.DropCollection("book");
        }

        public IEnumerable<Book> SelectIfCountMoreThan(int number)
        {
            return _bookCollection.FindSync(x => x.Count > number).ToList();
        }

        public IEnumerable<Book> GetTop(int number)
        {
            return _bookCollection.Find(new BsonDocument()).Limit(number).ToList();;
        }

        public Book GetByCount(SortBy parameter)
        {
            FindOptions<Book, Book> options;
            if (parameter == SortBy.Descending)
            {
                options = new FindOptions<Book, Book>()
                {
                    Limit = 1,
                    Sort = Builders<Book>.Sort.Descending(x => x.Count)
                };
            }
            else
            {
                options = new FindOptions<Book, Book>()
                {
                    Limit = 1,
                    Sort = Builders<Book>.Sort.Ascending(x => x.Count)
                };
            }

            return _bookCollection.FindSync(FilterDefinition<Book>.Empty, options).First(); ;
        }

        public IEnumerable<string> GetListOfAuthors()
        {
            return _bookCollection.AsQueryable().Where(book => book.Author != null).GroupBy(book => book.Author).Select(x => x.Key);
        }

        public IEnumerable<Book> GetBooksWithoutAuthor()
        {
            return _bookCollection.AsQueryable().Where(book => book.Author == null).ToList();
        }

        public void IncrementTheCountOfEachBook()
        {
            foreach (Book book in GetAll())
            {
                ObjectId id = book.Id;
                int count = book.Count;
                count++;
                UpdateDefinition<Book> updatedCount = Builders<Book>.Update.Set(x => x.Count, count);
                
                _bookCollection.UpdateMany(currentBook => currentBook.Id == id, updatedCount);
            }                       
        }

        public void DeleteIfTheCountLessThan(int number)
        {
            _bookCollection.DeleteMany(x => x.Count < number);
        }

        public void AddNewGenreIfPointedGenreExist(string existingGenre, string newGenre)
        {
            foreach (Book book in _bookCollection.AsQueryable().Where(book => book.Genre.Any(genre => genre == existingGenre)))
            {
                ObjectId id = book.Id;
                List<string> genres = book.Genre.ToList();

                if (genres.Contains(existingGenre) && !genres.Contains(newGenre))
                {
                    genres.Add(newGenre);
                }

                UpdateDefinition<Book> updatedGenre = Builders<Book>.Update.Set(x => x.Genre, genres.ToArray());
                _bookCollection.UpdateOne(currentBook => currentBook.Id == id, updatedGenre);
            }
        }
    }
}
