using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using PingPong.Entities;
using PingPong.Controllers;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
        public IEnumerable<Game> Get()
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Games.ToList();
            }
        }

        // GET api/games/{id}
        // Gets a particular game given the ID
        //TODO: Unused code.
        [HttpGet ("{id}")]
        public Game Get(int id)
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Games.First(p => p.Id == id);
            }
        }


        //POST api/game
        // Creates a game record in the db
        [HttpPost]
        public IActionResult Post(Game gameRequest) 
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Console.WriteLine("Add Game.");
            return Ok();
            //TODO: figure out how much direction we want to put in the code.
            //Option 1: Vague input/output.
            //input: Game object.
            //logic: Determine the winner of the game.
            //       Add game to Games table.
            //       Update player win/loss records.
            //output: none.

            //Option 2: step by step, what the funciton should do.
            //Sanitize the data for searching/adding new records.

            //Saving the two players locally for updating their win/loss record.

            //See if playerOne exists in the database yet.  

            //if they do. Remove the player from the game object as to not re-add them to the database.
            //            But set the Forign Key to that players Id.
            //            Update the local version of playerOne as to have accurate win/loss record.

            //Repeat for playerTwo.

            //Determine Game winner based on Score.
            //Add logic for if games scores are correct, i.e. someone reached 21(or 11), The winner won by 2 points, etc.
            //Update the win/loss record of each player, via the local version of that player.
                    //Add a record into the Games table.
                    //This .Add function will add new rows to the player table if the objects are not null.
                    //      Object == null | Id != null < Update
                    //      Object != null | Id == null < Add
            //Update the Database again with the Players new win/loss records.
        }
       
    }
}
