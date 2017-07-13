using System;
using System.ComponentModel.DataAnnotations;

//Players table to hold First Name, Last Name, and Number of Wins
namespace PingPong.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Missing Player Name.")]
        [MaxLength(50, ErrorMessage = "Player Name must be less than {1} characters long.")]
        public string Name { get; set; }
        public string Office { get; set; }
        public int numberWins {get; set; }
        public int numberLosses {get; set;}

        //This function can be used to sanitize a player object.
        //Curretly its set to ignore case, a set all letters to lower.
        public void clean() {
            if(this.Name != null) this.Name = this.Name.ToLower();
            if(this.Office != null) this.Office = this.Office.ToLower();
        }
    }
    public enum TasteRating
    {
        Test
    }
}