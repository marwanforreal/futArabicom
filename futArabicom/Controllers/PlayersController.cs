using futArabicom.Data;
using futArabicom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Drawing;
using System.Net.NetworkInformation;
using futArabicom.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

            List<string> playerImagesUrls = new();

            foreach (Player player in data)
            {
                playerImagesUrls.Add(ExtractImageUrl(player));
            }

            ViewBag.imageUrls = playerImagesUrls;

            return View("Index", data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
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
            var currentPlayer = _context.Players.SingleOrDefault(p => p.Id == id);

            PlayerDetailsViewModel viewModel = new PlayerDetailsViewModel()
            {
                Player = currentPlayer,
                Comments = _context.Comments.Where(p => p.Player == currentPlayer).Include(user => user.User).ToList()
            };

            //Player player = _context.Players.SingleOrDefault(p => p.Id == id);

            if(viewModel.Player.Image != null)
            {
                var imageDataUrl = ExtractImageUrl(viewModel.Player);

                ViewBag.imageUrl = imageDataUrl;
            }

            return View("Details", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> addComment(PlayerDetailsViewModel pageModel)
        {
            var currentUserName = User.Identity.Name;

            var currentUserObject = _context.Users.FirstOrDefault(x => x.UserName == currentUserName);

            var currentPlayerObject = _context.Players.FirstOrDefault(x => x.Id == pageModel.PlayerId);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return View();
            }

            try
            {
                _context.Comments.Add(new Comment
                {
                    Content = pageModel.NewComment.Content,
                    User = currentUserObject,
                    Player = currentPlayerObject
                });
            }
            catch (Exception)
            {
                throw;
            }

            await _context.SaveChangesAsync();

            var fillComments = _context.Comments.Where(p => p.Player == currentPlayerObject).Include(user => user.User).ToList(); 

            PlayerDetailsViewModel viewModel = new PlayerDetailsViewModel()
            {
                Player = _context.Players.SingleOrDefault(p => p.Id == pageModel.PlayerId),
                Comments = fillComments
            };

            return View("Details", viewModel);
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

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var player = _context.Players.SingleOrDefault(p => p.Id == id); 

            if(player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        [HttpDelete]
        public IActionResult Delete(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        public IActionResult Edit(Player player)
        {
            if(player.Image == null)
            {
                player.Image = new byte[0];  
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return View();
            }

            _context.Players.Update(player);
            _context.SaveChanges();

            return View();
        }
    }
}
