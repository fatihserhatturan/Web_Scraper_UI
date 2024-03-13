using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

        public List<string> GetAuthors()
        {
            var authors = _collection.Distinct<string>("Author", "{}").ToList();
            var filteredAuthors = new List<string>();

            foreach (var author in authors)
            {
                if (!string.IsNullOrEmpty(author) && author.Length >= 10 && author.Trim().Length<=20)
                {
                    filteredAuthors.Add(author.Trim());
                }
            }

            return filteredAuthors;
        }

        public List<string> GetArticleType()
        {
            var types = _collection.Distinct<string>("Type", "{}").ToList();
            var filteredTypes = new List<string>();

            foreach ( var type in types)
            {
                if(!string.IsNullOrEmpty(type) && type.Length >= 10 && type.Trim().Length <= 20)
                {
                    filteredTypes.Add(type.Trim());
                }
            }

            return filteredTypes;
        }

        public List<string> GetPublisher()
        {
            var publishers = _collection.Distinct<string>("Publisher", "{}").ToList();
            var filteredPublishers = new List<string>();

            foreach (var publisher in publishers)
            {
                if (!string.IsNullOrEmpty(publisher) && publisher.Length >= 10 && publisher.Trim().Length <= 20)
                {
                    filteredPublishers.Add(publisher.Trim());
                }
            }

            return filteredPublishers;
        }
        public List<string> GetKeywords()
        {
            var keywords = _collection.Distinct<string>("KeyWords", "{}").ToList();
            var filteredKeywords = new List<string>();

            foreach (var keyword in keywords)
            {
                if (keyword != null)
                {
                    var splitKeywords = keyword.Split(',').Select(k => k.Trim()).ToList();
                    foreach(var splitKeyword in splitKeywords)
                    {
                        if(splitKeyword.Length <= 20)
                        {
                            filteredKeywords.Add(splitKeyword);
                        }
                        
                    }
                    
                }
            }

            return filteredKeywords;
        }

        public List<Article> GetArticleByKeywords(List<string> keywords)
        {
            var filter = Builders<Article>.Filter.In("Keywords", keywords);
            var matchedArticles = _collection.Find(filter).ToList();
            return matchedArticles;
        }
        public List<Article> GetArticleByPublishers(List<string> publishers)
        {
            var filter = Builders<Article>.Filter.In("Publisher", publishers);
            var matchedArticles = _collection.Find(filter).ToList();
            return matchedArticles;
        }

        public List<Article> GetArticleByTypes(List<string> types)
        {
            var filter = Builders<Article>.Filter.In("Type", types);
            var matchedArticles = _collection.Find(filter).ToList();
            return matchedArticles;
        }

        public List<Article> GetArticleByAuthors(List<string> authors)
        {
            var filter = Builders<Article>.Filter.In("Author", authors);
            var matchedArticles = _collection.Find(filter).ToList();
            return matchedArticles;
        }

    }
}
