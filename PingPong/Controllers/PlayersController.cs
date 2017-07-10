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
        public IEnumerable<Player> GetAllPlayers()
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.ToList();
            }
        }

        // GET api/players/{id}
        // Gets a particular player given their ID
        [HttpGet ("{id}")]
        public Player FindPlayerById(int id)
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.FirstOrDefault(p => p.Id == id);
            }
        }

        // GET api/players/name?Name={}
        // Gets a particular player by first and last name
        [HttpGet] 
        [Route("name")]
        public Player FindPlayerByName([FromQueryAttribute]String name)
        {
            name = name.ToLower();
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.FirstOrDefault(p => p.Name == name);
            }
        }

        // POST api/players
        // Add a new player with JSON body
        [HttpPost]
        public Player AddNewPlayer([FromBody]Player player)
        {
            player.clean();
            using (PingPongDb db = new PingPongDb())
            { 
                db.Players.Add(player);
                db.SaveChanges();
                return db.Players.Where(x => x.Name == player.Name).FirstOrDefault(); 
            }
        }

        //PUT api/players/{id}
        //Update a player's data
        [HttpPut("{id}")]
        public void UpdatePlayer(int id, [FromBody]Player player)
        {
            //Integer PlayerData.NumberWins = value.NumberWins;
            player.Id = id;
            using (PingPongDb db = new PingPongDb()) 
            {
                db.Players.Update(player);
                db.SaveChanges();
            }
        }

        //Get api/players/leaderboard/{top}
        //Gets the leader board of all games played.
        //top = top number of players to return
        [HttpGet]
        [Route("leaderboard/{top=10}")]
        public Player[] GetLeaderBoard(int top) {
            try {
                using(PingPongDb db = new PingPongDb())
                {
                    return db.Players.OrderByDescending(x => x.numberWins).Take(top).ToArray();
                }
            }catch (Exception e) {
                throw new HttpRequestException(string.Format("Error: Failed to get Leaderboard.", e)); 
            }
        }
    }
}
