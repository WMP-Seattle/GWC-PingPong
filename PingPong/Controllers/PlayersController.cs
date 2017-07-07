using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using PingPong.Entities;

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
                return db.Players.First(p => p.Id == id);
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
                return db.Players.First(p => p.Name == name);
            }
        }

        // POST api/players
        // Add a new player with JSON body
        [HttpPost]
        public int AddNewPlayer([FromBody]JObject value)
        {
            Player posted = value.ToObject<Player>();
            posted.Name = posted.Name.ToLower();
            int result;
            try{
                using (PingPongDb db = new PingPongDb())
                { 
                    db.Players.Add(posted);
                    db.SaveChanges();
                    result = db.Players.Where(x => x.Name == posted.Name).Select(x => x.Id).FirstOrDefault(); 
                }
            }catch{
                result = -1;
            }
            return result;
        }

        //PUT api/players/{id}
        //Update a player's data
        [HttpPut("{id}")]
        public void UpdatePlayer(int id, [FromBody]JObject value)
        {
            //Integer PlayerData.NumberWins = value.NumberWins;
            Player posted = value.ToObject<Player>();
            posted.Id = id;
            using (PingPongDb db = new PingPongDb()) 
            {
                db.Players.Update(posted);
                db.SaveChanges();
            }
        }

       
    }
}
