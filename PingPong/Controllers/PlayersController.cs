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
        public IEnumerable<Player> Get()
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.ToList();
            }
        }

        // GET api/players/{id}
        // Gets a particular player given their ID
        [HttpGet ("{id}")]
        public Player Get(int id)
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.First(p => p.Id == id);
            }
        }

        /*[HttpGet ({"firstName"})] 
        public PlayerData Get([FromQueryAttribute]String firstName, [FromQueryAttribute] String lastName)
        {
            using (PingPongDb db = new PingPongDb())
            {
                return db.Players.First(p => p.FirstName == firstName && p.LastName == lastName);
            }
        }*/

        // POST api/players
        // Add a new player with JSON body
        [HttpPost]
        public void Post([FromBody]JObject value)
        {
            Player posted = value.ToObject<Player>();
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
