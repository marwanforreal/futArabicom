using futArabicom.Data;
using futArabicom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Net.NetworkInformation;

namespace futArabicom.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            var data = _context.Players.ToList();

            return View("Index", data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player, IFormFile playerImage)
        {
            player.Pace = 86;
            player.Defending = 75;
            player.Shooting = 60;
            player.Passing = 69;
            player.Physical = 80;
            player.Dribbling = 59;

            if(playerImage != null)
            {
                var imageData = TransformImageData(playerImage);
                player.Image = imageData;
            }
            else
            {
                player.Image = new byte[0];
            }

            _context.Players.Add(player);
            _context.SaveChanges(); 
            return View();
        }

        private byte[] TransformImageData(IFormFile playerImage)
        {
            MemoryStream ms = new MemoryStream();
            playerImage.CopyTo(ms);
            var imageData = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return imageData;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Player player = _context.Players.SingleOrDefault(p => p.Id == id);

            if(player.Image != null)
            {
                var imageDataUrl = ExtractImageUrl(player);

                ViewBag.imageUrl = imageDataUrl;
            }

            return View("Details", player);
        }

        private string ExtractImageUrl(Player player)
        {
            var image = player.Image;
            string imageBase64 = Convert.ToBase64String(image);

            var url = string.Format("data:image/png; base64,{0}"
            , imageBase64);

            return url;
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var player = _context.Players.SingleOrDefault(p =>p.Id == id);

            if(player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        [HttpPost]
        public IActionResult Edit(Player player)
        {

            if (ModelState.IsValid)
            {
                _context.Players.Update(player);
                _context.SaveChanges();
                return View();
            }

            return View();
        }
    }
}
