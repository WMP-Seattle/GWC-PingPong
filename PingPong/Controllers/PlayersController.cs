using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace PlayersAPI.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        // GET api/players
        //Gets all players in the database
        [HttpGet]
        public IEnumerable<PlayerData> Get()
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.ToList();
            }
        }

        // GET api/players/{id}
        // Gets a particular player given their ID
        [HttpGet ("{id}")]
        public PlayerData Get(int id)
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.First(p => p.Id == id);
            }
        }

        // GET api/players/name?firstName={}&lastName={}
        // Gets a particular player by first and last name
        [HttpGet] 
        [Route("name")]
        public PlayerData Get([FromQueryAttribute]String firstName, [FromQueryAttribute] String lastName)
        {
            Console.WriteLine("first name: " + firstName);
            Console.WriteLine("last name: " + lastName);
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.First(p => p.FirstName == firstName && 
                                            p.LastName == lastName);
            }
        }

        // POST api/players
        // Add a new player with JSON body
        [HttpPost]
        public void Post([FromBody]JObject value)
        {
            PlayerData posted = value.ToObject<PlayerData>();
            using (PingPongDb db = new PingPongDb())
            {
                db.Players.Add(posted);
                db.SaveChanges();
            }
        }

        //PUT api/players/{id}
        //Update a player's data
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]JObject value)
        {
            //Integer PlayerData.NumberWins = value.NumberWins;
            PlayerData posted = value.ToObject<PlayerData>();
            posted.Id = id;
            using (PingPongDb db = new PingPongDb()) 
            {
                db.Players.Update(posted);
                db.SaveChanges();
            }
        }

       
    }
}
