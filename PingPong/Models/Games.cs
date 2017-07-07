using System;
using System.ComponentModel.DataAnnotations;

//Games table contains 2 players, their scores, the winner and if the game is completed.
namespace PingPong.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PlayerOneId { get; set; }
        public Player PlayerOne { get; set; }
        public String PlayerOneName {get; set;}
        public int PlayerTwoId { get; set; }
        public Player PlayerTwo { get; set; }
        public String PlayerTwoName {get; set;}
        public int Winner { get; set; }
        public bool complete { get; set; }
        
    }
}