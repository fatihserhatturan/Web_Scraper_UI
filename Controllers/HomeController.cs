using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ScrapService scrapService, DatabaseService databaseService)
        {
            _scrapService = scrapService;
            _databaseService = databaseService;
        }

        public IActionResult Index(int page =1)
        {
            var articles = _databaseService.GetAllArticles().ToPagedList(page,10);
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
    }
}
