using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

       
    }
}
