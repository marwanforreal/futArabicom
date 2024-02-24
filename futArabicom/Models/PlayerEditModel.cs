using System.ComponentModel.DataAnnotations;

namespace futArabicom.Models
{
    public class PlayerEditModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? NameAr { get; set; }

        public string? Description { get; set; }

        public string? Country { get; set; }

        public string? Type { get; set; }

        public byte[]? Image { get; set; }

    }
}
