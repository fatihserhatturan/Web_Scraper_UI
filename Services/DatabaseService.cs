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
        public List<Article> GetAllArticles()
        {
            var articles = _collection.Find(_ => true).ToList();
            return articles;
        }

        public Article GetArticleById(string id)
        {
            var article = _collection.Find(x=>x.Id == id).SingleOrDefault();

            return article;
        }

        public List<Article> OrderByDescendingDate()
        {
            var sortedArticles = _collection.Find(a => a.Summary != null)
                                   .SortByDescending(a => a.Date)
                                   .ToList();
            return sortedArticles;
        }

        public List<Article> OrderByAscendingDate()
        {
            var sortedArticles = _collection.Find(a => a.Summary != null)
                                   .SortBy(a => a.Date)
                                   .ToList();
            return sortedArticles;
        }

    }
}
