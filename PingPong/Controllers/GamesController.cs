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
    public class GamesController : Controller
    {
        // GET api/games
        //Gets all games in the database
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
        public void Post([FromBody]JObject value) 
        {
            Game posted = value.ToObject<Game>();
            using (PingPongDb db = new PingPongDb())
            {
                db.Games.Add(posted);
                db.SaveChanges();
            }
        }
       
    }
}
