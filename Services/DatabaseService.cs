using MongoDB.Driver;
using Web_Scraper_UI.Models;

namespace Web_Scraper_UI.Services
{
    public class DatabaseService
    {
        private readonly IMongoCollection<Article> _collection;
        public DatabaseService(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            _collection = db.GetCollection<Article>(collectionName);
        }

        public List<Article> GetArticlesByKeyword(string keyword)
        {
            var filter = Builders<Article>.Filter.Regex("Title", new MongoDB.Bson.BsonRegularExpression(keyword, "i")); 
            var articles = _collection.Find(filter).ToList();
            return articles;
        }
        public List<Article> GetFirst100Articles()
        {
            var articles = _collection.Find(_ => true).Limit(100).ToList();
            return articles;
        }
    }
}
