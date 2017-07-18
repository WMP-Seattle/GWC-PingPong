using System;
using System.ComponentModel.DataAnnotations;

//GWC-PingPong: Fully implemented. No action required.
//WARNING: Changes to this file will required EntityFrame Migration updates.
//Games table contains 2 players, their scores, the winner and if the game is completed.
namespace PingPong.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public int PlayerOneId { get; set; }
        [Required(ErrorMessage = "Missing Player One information.")]
        public Player PlayerOne { get; set; }        
        [Required(ErrorMessage = "Missing Player One's score.")]
        public int PlayerOneScore {get; set;}
        public int PlayerTwoId { get; set; }
        [Required(ErrorMessage = "Missing Player Two information.")]
        public Player PlayerTwo { get; set; }
        [Required(ErrorMessage = "Missing Player Two's score.")]
        public int PlayerTwoScore {get; set;}
        public int Winner { get; set; }
        public bool complete { get; set; }
        
    }
}