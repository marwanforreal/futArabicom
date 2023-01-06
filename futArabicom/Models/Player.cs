using System.ComponentModel.DataAnnotations;

namespace futArabicom.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Club { get; set; }
    }
}
