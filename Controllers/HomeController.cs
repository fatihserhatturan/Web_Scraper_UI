using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web_Scraper_UI.Services;

namespace Web_Scraper_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScrapService _scrapService;

        public HomeController(ScrapService scrapService)
        {
            _scrapService = scrapService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetArticles(string keyword)
        {
            await _scrapService.StartScrapAsync(keyword);

            return RedirectToAction("Index"); // GetArticles tamamlandıktan sonra Index sayfasına yönlendir.
        }
    }
}
