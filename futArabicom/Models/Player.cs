﻿using System.ComponentModel.DataAnnotations;

namespace futArabicom.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Club { get; set; }

        public int? Pace { get; set; }

        public int? Shooting { get; set; }

        public int? Passing { get; set; }

        public int? Dribbling { get; set; }

        public int? Defending { get; set; }

        public int? Physical { get; set; }

        public Player()
        {

        }

        public Player(int id, string name, string? club, int? pace, int? shooting, int? passing, int? dribbling, int? defending, int? physical)
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
        }
    }
}
