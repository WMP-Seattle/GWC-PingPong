using System;
using System.ComponentModel.DataAnnotations;

//Players table to hold First Name, Last Name, and Number of Wins
namespace PlayersAPI
{
    public class PlayerData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int numberWins {get; set; }
        public int numberLosses {get; set;}
    }
    public enum TasteRating
    {
        Test
    }
}