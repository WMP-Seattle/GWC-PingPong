using System;
using System.ComponentModel.DataAnnotations;

//GWC-PingPong: Fully implemented. No action required.
//WARNING: Changes to this file will require EntityFrame Migration updates.
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
        //Currently it is set to ignore case, and sets all letters to lower case.
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
