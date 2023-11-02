using futArabicom.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace futArabicom.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public string Country { get; set; } 

        public string? Club { get; set; }

        public int? Pace { get; set; }

        public int? Shooting { get; set; }

        public int? Passing { get; set; }

        public int? Dribbling { get; set; }

        public int? Defending { get; set; }

        public int? Physical { get; set; }

        public DateTime lastUpdate { get; set; }

        public string? Type { get; set; }

        public byte[] Image { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Claims> Claims { get; set; }

        public Player()
        {

        }

        public Player(int id, string name, string? club, int? pace, int? shooting, int? passing, int? dribbling, int? defending, int? physical, string? type, byte[] image)
        {
            Id = id;
            Name = name;
            Club = club;
            Pace = pace;
            Shooting = shooting;
            Passing = passing;
            Dribbling = dribbling;
            Defending = defending;
            Physical = physical;
            Type = type;
            Image = image;
            Comments = new List<Comment>() { }; 
        }
    }
}
