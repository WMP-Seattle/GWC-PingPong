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
        public void Post([FromBody]JObject requestObj) 
        {
            Console.Write("Add Game!");
            Game gameRequest = requestObj.ToObject<Game>();
            //make call to Player API passing Player1 obj  
            Player p1 = playersController.FindPlayerByName(gameRequest.PlayerOne.Name);
            if(p1 != null){
                gameRequest.PlayerOneId = p1.Id; 
            }else{
                Player newPlayerObj = new Player();                
                newPlayerObj.Name = gameRequest.PlayerOne.Name; 
                JObject newPlayer = new JObject(newPlayerObj);        
                var newPlayerId = playersController.AddNewPlayer(newPlayer);
                if(newPlayerId != -1){
                    gameRequest.PlayerOneId = newPlayerId; 
                }else{
                    throw new HttpRequestException(string.Format("Error: Failed to add new Player, {0}", gameRequest.PlayerOne.Name)); 
                }
            }        
            //make call to Player API passing Player2 obj  
            Player p2 = playersController.FindPlayerByName(gameRequest.PlayerTwo.Name);
            if(p2 != null){
                gameRequest.PlayerTwoId = p2.Id; 
            }else{
                Player newPlayerObj = new Player();                
                newPlayerObj.Name = gameRequest.PlayerTwo.Name; 
                JObject newPlayer = new JObject(newPlayerObj);        
                var newPlayerId = playersController.AddNewPlayer(newPlayer);
                if(newPlayerId != -1){
                    gameRequest.PlayerOneId = newPlayerId; 
                }else{
                    throw new HttpRequestException(string.Format("Error: Failed to add new Player, {0}", gameRequest.PlayerTwo.Name)); 
                }
            }            
            //Determine Game winner based on Scores
            if(gameRequest.PlayerOneScore > gameRequest.PlayerTwoScore){
                gameRequest.Winner = gameRequest.PlayerOneId;
            }else{
                gameRequest.Winner = gameRequest.PlayerTwoId;
            }
            gameRequest.complete = true;
            try{
                using (PingPongDb db = new PingPongDb())
                {
                    db.Games.Add(gameRequest);
                    db.SaveChanges();
                }
            }catch{
                throw new HttpRequestException("Error: Failed to add game record."); 
            }
        }
       
    }
}
