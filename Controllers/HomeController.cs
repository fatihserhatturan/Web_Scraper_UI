using Microsoft.AspNetCore.Mvc;
using Web_Scraper_UI.Services;

namespace Web_Scraper_UI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
     
        public  IActionResult GetArticles(string keyword)
        {
            ScrapService scrapService = new ScrapService();
            scrapService.StartScrap(keyword);


            return View();
        }
    }
}
