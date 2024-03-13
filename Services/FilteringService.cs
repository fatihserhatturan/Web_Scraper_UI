using System.Security.Policy;
using System;
using Web_Scraper_UI.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Web_Scraper_UI.Services
{
    public class FilteringService
    {
        private readonly DatabaseService _databaseService;

        public FilteringService()
        {
            
        }
        public FilteringService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public List<Article> MainFilter(List<string> keywords, List<string> publishers, List<string> types, List<string> authors)
        {
            List<Article> _Keywords = null, _Publishers = null, _Types = null, _Authors = null;

            if (keywords != null && keywords.Any())
            {
                _Keywords = _databaseService.GetArticleByKeywords(keywords);
                // Burada yapılacak işlemler...
            }

            if (publishers != null && publishers.Any())
            {
                _Publishers = _databaseService.GetArticleByPublishers(publishers);
                // Burada yapılacak işlemler...
            }

            if (types != null && types.Any())
            {
                _Types = _databaseService.GetArticleByTypes(types);
                // Burada yapılacak işlemler...
            }

            if (authors != null && authors.Any())
            {
                _Authors = _databaseService.GetArticleByAuthors(authors);
                // Burada yapılacak işlemler...
            }

            List<string> activefilters = GetActiveFilters(_Keywords, _Publishers, _Types, _Authors);


            if(activefilters.Count > 1)
            {
                return MultipleFilter(_Keywords, _Publishers, _Types, _Authors);
            }
            if(activefilters.Count == 1)
            {
                return OnePropertyFilter(_Keywords, _Publishers, _Types, _Authors);
            }

            return new List<Article>();
        }

        public List<string> GetActiveFilters(List<Article> keywords, List<Article> publishers, List<Article> types, List<Article> authors)
        {
            var activeFilters = new List<string>();
            if (keywords != null)
                activeFilters.Add("_Keywords");
            if (publishers != null)
                activeFilters.Add("_Publishers");
            if (types != null)
                activeFilters.Add("_Types");
            if (authors != null)
                activeFilters.Add("_Authors");

            return activeFilters;
        }
        public List<Article> MultipleFilter(List<Article> keywords, List<Article> publishers, List<Article> types, List<Article> authors)
        {
            var matchingArticles = keywords
        .Concat(publishers ?? Enumerable.Empty<Article>())
        .Concat(types ?? Enumerable.Empty<Article>())
        .Concat(authors ?? Enumerable.Empty<Article>())
        .Where(article => article != null) 
        .GroupBy(article => article.Id)
        .Where(group => group.Count() > 1) 
        .SelectMany(group => group);

            return matchingArticles.ToList();
        }
        public List<Article> OnePropertyFilter(List<Article> keywords, List<Article> publishers, List<Article> types, List<Article> authors)
        {

            if (keywords != null)
                return keywords;
            if (publishers != null)
                return publishers;
            if (types != null)
                return types;
            if (authors != null)
                return authors;


            return new List<Article>();
        }
    }
}
