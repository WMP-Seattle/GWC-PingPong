using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PingPong.Entities;

//GWC-PingPong: This file controls data for the Players table within the database.
//              Retrieve data and add data via the functions within.
//TODO: Implement GET api/players/{id} - FindPlayerById().
//      Implement GET api/players/name?Name='' - FindPlayerByName().
//      Implement POST api/players - AddNewPlayer().
//      Implement PUT api/players/{id} - UpdatePlayer().
//      Modify GET api/players/leaderboard/{top} - GetLeaderBoard.
namespace PingPong.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        // GET api/players
        //Gets all players in the database
        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            using (PingPongDb db = new PingPongDb())
            {
                var players = db.Players.ToList();
                return Ok(players);
            }
        }

        // GET api/players/{id}
        // Gets a particular player given their ID
        [HttpGet ("{id}")]
        public IActionResult FindPlayerById(int id)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var player = new Player();
            return Ok(player);
        }

        // GET api/players/name?Name={}
        // Gets a particular player by name
        [HttpGet] 
        [Route("name")]
        public IActionResult FindPlayerByName([FromQueryAttribute]String name)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            name = name.ToLower();
            return NotFound();
        }

        // POST api/players
        // Add a new player with JSON body
        [HttpPost]
        public IActionResult AddNewPlayer(Player player)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Console.WriteLine("Add Player.");
            //Sanitize the player data.
            player.clean();
            return Ok(player);
        }

        //PUT api/players/{id}
        //Update a player's data
        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, Player player)
        {   
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Console.WriteLine("Add Player.");
            //Update player with new information.
            return Ok(player);
        }

        //Get api/players/leaderboard/{top}
        //Gets the leader board of all games played.
        //top = top number of players to return
        [HttpGet]
        [Route("leaderboard/{top=10}")]
        public IActionResult GetLeaderBoard(int top) {
            try {
                using(PingPongDb db = new PingPongDb())
                {
                    var players = db.Players.OrderBy(x => x.numberLosses).Take(top).ToArray();
                    return Ok(players);
                }
            }catch (Exception e) {
                throw new HttpRequestException(string.Format("Error: Failed to get Leaderboard.", e)); 
            }
        }
    }
}
