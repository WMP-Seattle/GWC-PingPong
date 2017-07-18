using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PingPong.Entities;
using PingPong.Controllers;

//GWC-PingPong: This file controlls data for the Games table within the database.
//              Retrieve data and add data via the functions within.
//TODO: Implement the POST /api/games api function.
namespace PingPong.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
    static HttpClient client = new HttpClient();
    //static PlayersController playersController = new PlayersController();
    private PlayersController playersController;

    public GamesController() {
        this.playersController = new PlayersController();
    }

        // GET api/games
        //Gets all games in the database
        //TODO: Unused call.
        [HttpGet]
        public IActionResult Get()
        {
            using (PingPongDb db = new PingPongDb())
            {
                var games = db.Games.ToList();
                return Ok(games);
            }
        }

        // GET api/games/{id}
        // Gets a particular game given the ID
        //TODO: Unused code.
        [HttpGet ("{id}")]
        public IActionResult Get(int id)
        {
            using (PingPongDb db = new PingPongDb())
            {
                var game = db.Games.FirstOrDefault(p => p.Id == id);
                if(game == null) NotFound();
                return Ok(game);
            }
        }


        //POST api/game
        // Creates a game record in the db
            //input: Game object.
            //logic: Determine the winner of the game.
            //       Add game to Games table.
            //       Update player win/loss records.
            //output: none.
        [HttpPost]
        public IActionResult Post(Game gameRequest) 
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Console.WriteLine("Add Game.");
            return Ok();

            //Sanitize the data for searching/adding new records.

            //See if playerOne exists in the database yet.  

            //if they do. Remove the player from the game object as to not re-add them to the database.
            //            But set the Forign Key to that players Id.
            //            Update the local version of playerOne as to have accurate win/loss record.

            //Repeat for playerTwo.

            //Determine Game winner based on Score.
            //Add logic for if games scores are correct, i.e. someone reached 21(or 11), The winner won by 2 points, etc.
            //Update the win/loss record of each player.
                    //Add a record into the Games table.
                    //This .Add function will add new rows to the player table if the objects are not null.
                    //      player == null | Id != null < Update
                    //      player != null | Id == null < Add (player.Id must equal null)
            //Update the Database again with the Players new win/loss records.
        }
       
    }
}
