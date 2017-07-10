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
        public void Post([FromBody]Game gameRequest) 
        {
            //Sanitize the data for searching/adding new records.
            gameRequest.PlayerOne.clean();
            gameRequest.PlayerTwo.clean();

            //Saving the two players locally for updating their win/loss record.
            var playerOne = gameRequest.PlayerOne;
            var playerTwo = gameRequest.PlayerTwo;

            //See if playerOne exists in the database yet.  
            Player p1 = playersController.FindPlayerByName(gameRequest.PlayerOne.Name);
            //if they do. Remove the player from the game object as to not re-add them to the database.
            //            But set the Forign Key to that players Id.
            //            Update the local version of playerOne as to have accurate win/loss record.
            if(p1 != null){
                gameRequest.PlayerOne = null; 
                gameRequest.PlayerOneId = p1.Id;
                playerOne = p1;
            }        

            //Repeat for playerTwo.
            Player p2 = playersController.FindPlayerByName(gameRequest.PlayerTwo.Name);
            if(p2 != null){
                gameRequest.PlayerTwo = null;
                gameRequest.PlayerTwoId = p2.Id;
                playerTwo = p2; 
            }            

            //Determine Game winner based on Score.
            //Add logic for if games scores are correct, i.e. someone reached 21(or 11), The winner won by 2 points, etc.
            //Update the win/loss record of each player, via the local version of that player.
            if(gameRequest.PlayerOneScore > gameRequest.PlayerTwoScore){
                gameRequest.Winner = gameRequest.PlayerOneId;
                playerOne.numberWins++;
                playerTwo.numberLosses++;
            }else{
                gameRequest.Winner = gameRequest.PlayerTwoId;
                playerTwo.numberWins++;
                playerOne.numberLosses++;
            }
            gameRequest.complete = true;
            try{
                using (PingPongDb db = new PingPongDb())
                {
                    //Add a record into the Games table.
                    //This .Add function will add new rows to the player table if the objects are not null.
                    //      Object == null | Id != null < Update
                    //      Object != null | Id == null < Add
                    db.Games.Add(gameRequest);
                    db.SaveChanges();
                }
            }catch (Exception e){
                throw new HttpRequestException("Error: Failed to add game record."); 
            }
            //Update the Database again with the Players new win/loss records.
            playersController.UpdatePlayer(playerOne.Id, playerOne);
            playersController.UpdatePlayer(playerTwo.Id, playerTwo);
        }
       
    }
}
