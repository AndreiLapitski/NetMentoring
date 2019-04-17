using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Module7.Entities
{
    public class Book
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("count")]
        public int Count { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("genre")]
        public string[] Genre { get; set; }

        public override string ToString()
        {
            string genre = null;
            foreach (string str in Genre)
            {
                genre += str + " ";
            }

            return string.Format("Name:{0};Author:{1};Count:{2};Year{3};Genre:{4}",
                Name,
                Author,
                Count,
                Year,
                genre);
        }
    }
}