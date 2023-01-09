using futArabicom.Data;
using futArabicom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace futArabicom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            /*Trending players under the search bar list*/
            var trendingPlayers = _context.Players.Take(6).ToList();

            return View(trendingPlayers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search(string query)
        {
            var players = _context.Players.ToList(); 

            if(query == null)
            {
                query = "Marwan";
            }

            return View("Search", players.FindAll(p => p.Name.Contains(query))); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}