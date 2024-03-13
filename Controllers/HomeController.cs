using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Web_Scraper_UI.Models;
using Web_Scraper_UI.Services;
using X.PagedList;

namespace Web_Scraper_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScrapService _scrapService;
        private readonly DatabaseService _databaseService;
        private readonly FilteringService _filteringService;

        public HomeController(ScrapService scrapService, DatabaseService databaseService,FilteringService filteringService )
        {
            _scrapService = scrapService;
            _databaseService = databaseService;
            _filteringService = filteringService;
        }

        public IActionResult Index(int page =1)
        {
            var articles = _databaseService.GetAllArticles().ToPagedList(page,10);
            var authors = _databaseService.GetAuthors();
            var types = _databaseService.GetArticleType();
            var publisers = _databaseService.GetPublisher();
            var keywords = _databaseService.GetKeywords();

            ViewBag.Types = types;
            ViewBag.Authors = authors;
            ViewBag.Publishers = publisers;
            ViewBag.Keywords = keywords;
            return View(articles);
        }

        public async Task<IActionResult> GetArticles(string keyword)
        {
            await _scrapService.StartScrapAsync(keyword);
            var articles = _databaseService.GetArticlesByKeyword(keyword);

            return Json(articles);
        }

        public async Task<IActionResult> GetFirst100Articles()
        {
            List<Article> articles = _databaseService.GetAllArticles();
            return Json(articles);
        }

        [HttpPost]
        public IActionResult GetArticlesBlabla(string Id)
        {
            var article = _databaseService.GetArticleById(Id);
            return View(article);
        }
        [HttpPost]
        public IActionResult GetArticlesByDateDescending()
        {
            var articles = _databaseService.OrderByDescendingDate(); 
            return Json(articles);
        }
        [HttpPost]
        public IActionResult GetArticlesByDateAscending()
        {
            var articles = _databaseService.OrderByAscendingDate();
            return Json(articles);
        }

        [HttpGet]
        public IActionResult GetAuthor()
        {
            var authors = _databaseService.GetAuthors();
            return Json(authors);
        }

        [HttpPost]
        public IActionResult GetFilters(List<string> keywords, List<string> publishers, List<string> types, List<string> authors)
        {
            var result = _filteringService.MainFilter(keywords, publishers, types, authors);

            Console.WriteLine(result);

            return Json(result);
        }
    }
}
