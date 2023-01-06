using futArabicom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace futArabicom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search(string query)
        {
            List<Player> players = new();

            players.Add(new Player(1, "Marwan", "Real Madrid"));


            return View("Search", players.FindAll(p => p.Name.Contains(query))); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}