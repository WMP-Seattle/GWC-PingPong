using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using PingPong.Entities;
using System.Net.Http;

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
            if(!ModelState.IsValid) return BadRequest(ModelState);
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
            if(!ModelState.IsValid) return BadRequest(ModelState);
            using (PingPongDb db = new PingPongDb())
            {
                var player = db.Players.FirstOrDefault(p => p.Id == id);
                if(player == null) return NotFound();
                return Ok(player);
            }
        }

        // GET api/players/name?Name={}
        // Gets a particular player by first and last name
        [HttpGet] 
        [Route("name")]
        public IActionResult FindPlayerByName([FromQueryAttribute]String name)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            name = name.ToLower();
            using (PingPongDb db = new PingPongDb())
            {
                var player = db.Players.FirstOrDefault(p => p.Name == name);
                if(player == null) return NotFound();
                return Ok(player);
            }
        }

        // POST api/players
        // Add a new player with JSON body
        [HttpPost]
        public IActionResult AddNewPlayer(Player player)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            player.clean();
            try {
                using (PingPongDb db = new PingPongDb())
                { 
                    db.Players.Add(player);
                    db.SaveChanges();
                    return Ok(db.Players.Where(x => x.Name == player.Name).FirstOrDefault()); 
                }
            } catch (Exception e) {
                throw new HttpRequestException(string.Format("Error: Failed to add player.", e)); 
            }
        }

        //PUT api/players/{id}
        //Update a player's data
        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, Player player)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            //Integer PlayerData.NumberWins = value.NumberWins;
            player.Id = id;
            using (PingPongDb db = new PingPongDb()) 
            {
                db.Players.Update(player);
                db.SaveChanges();
                return Ok();
            }
        }

        //Get api/players/leaderboard/{top}
        //Gets the leader board of all games played.
        //top = top number of players to return
        [HttpGet]
        [Route("leaderboard/{top=10}")]
        public IActionResult GetLeaderBoard(int top) {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try {
                using(PingPongDb db = new PingPongDb())
                {
                    var players = db.Players.OrderByDescending(x => x.numberWins).Take(top).ToArray();
                    return Ok(players);
                }
            }catch (Exception e) {
                throw new HttpRequestException(string.Format("Error: Failed to get Leaderboard.", e)); 
            }
        }
    }
}
