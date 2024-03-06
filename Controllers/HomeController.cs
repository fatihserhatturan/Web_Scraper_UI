using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web_Scraper_UI.Models;
using Web_Scraper_UI.Services;

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

        public IActionResult Index()
        {
            List<Article> articles = _databaseService.GetFirst100Articles();
            return View(articles);
        }

        public async Task<IActionResult> GetArticles(string keyword)
        {
            await _scrapService.StartScrapAsync(keyword);
            List<Article> articles = _databaseService.GetArticlesByKeyword(keyword);

            return Json(articles);
        }

        public async Task<IActionResult> GetFirst100Articles()
        {
            List<Article> articles = _databaseService.GetFirst100Articles();
            return Json(articles);
        }
    }
}
