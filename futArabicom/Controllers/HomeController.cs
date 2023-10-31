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
            var trendingPlayers = _context.Players.OrderByDescending(p => p.Id).Take(12).ToList();

            List<string> playerImagesUrls = new();

            foreach (Player player in trendingPlayers){
                playerImagesUrls.Add(ExtractImageUrl(player));
            }

            ViewBag.imageUrls = playerImagesUrls;

            return View(trendingPlayers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AutoComplete(string prefix)
        {
            // var pages = _context.Players.Where(p => p.Name.ToLower().StartsWith(prefix.ToLower())).Select(p => p.Name).ToList();

            //return Json(pages);
            var players = _context.Players
                    .Where(p => p.Name.ToLower().StartsWith(prefix.ToLower()))
                     .Select(p => new
                        {
                            Id = p.Id,
                            Name = p.Name
                        })
                    .ToList();

            return Json(players);
        }

        public IActionResult Search(string query)
        {
            if (query == null)
            {
                query = "Marwan";
            }

            var players = _context.Players.Where(p => p.Name.Contains(query)).ToList();

            List<string> playerImagesUrls = new();

            foreach (Player player in players)
            {
                playerImagesUrls.Add(ExtractImageUrl(player));
            }

            ViewBag.imageUrls = playerImagesUrls;

            return View("Search", players); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string ExtractImageUrl(Player player)
        {
            var image = player.Image;
            string imageBase64 = Convert.ToBase64String(image);

            var url = string.Format("data:image/png; base64,{0}"
            , imageBase64);

            return url;
        }
    }
}