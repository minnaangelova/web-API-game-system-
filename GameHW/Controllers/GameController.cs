using GameHW.Context;
using GameHW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameHW.Controllers
{
    public class GameController : ApiController
    {

        private readonly DatabaseContext dbConnection = new DatabaseContext();

        public GameController()
        {
           
        }

       // List all available games

        [Route("api/games/all")]
        [HttpGet]

        public IHttpActionResult GetAllGames()
        {
            var allGames = this.dbConnection.Games.ToArray();
            return Ok(allGames);                    
            
        }

        //List game by given game name.
        //If game doesn’t exist, status 404 must be returned to the user.

        [Route("api/games/{GameTitle}")]
        [HttpGet]

        public IHttpActionResult GetByTitle(string GameTitle)
        {
            var queryResult = this.dbConnection.Games.FirstOrDefault((g) => g.GameTitle == GameTitle);

            if (queryResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(queryResult);
            }
        }


        //List game by given game ID. 
        //If game doesn’t exist, status 404 must be returned to the user.

        [Route("api/games/{GameId:int}")]
        [HttpGet]

        public IHttpActionResult GetById(int GameId)
        {
            var queryResult = this.dbConnection.Games.FirstOrDefault((g) => g.GameId == GameId);

            if (queryResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(queryResult);
            }
        }

        //Adds new game. 
        //If game with the same name already exists, 
        //status 400 must be returned to the user.



        [Route("api/games")]
        [HttpPost]

        public IHttpActionResult PostNewGame([FromBody]Game game)
        {
            
                var sameGameExist = dbConnection.Games.Any(g => g.GameTitle == game.GameTitle);

            if (!sameGameExist)
            {
                dbConnection.Games.Add(game);
                dbConnection.SaveChanges();
                return Ok(game);

            }
            else
            {
                return BadRequest();
            }
        }

        //Modify the whole game.
        //If game doesn’t exist, status 404 must be returned to the user.

        [Route("api/games/{GameId}")]
        [HttpPut]

        public IHttpActionResult PutToModifyGame(int GameId, [FromBody]Game game)
        {
            var gameExist = this.dbConnection.Games.Any((g) => g.GameId == GameId);

            if (!gameExist)
            {
                return NotFound();
            }
            else
            {
                var modGame = this.dbConnection.Games.FirstOrDefault((g) => g.GameId == GameId);
                modGame.GameTitle = game.GameTitle;
                dbConnection.SaveChanges();
                return Ok();
            }
        }

        //Delete existing game.
        //If game doesn’t exist, status 404 must be returned to the user.

        [Route("api/games/{GameId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteGame(int GameId)
        {
            var removeGame = this.dbConnection.Games.FirstOrDefault((g) => g.GameId == GameId);
            var gameExist = this.dbConnection.Games.Any((g) => g.GameId == GameId);

            if (!gameExist)
            {
                return NotFound();
            }
            else
            {
                this.dbConnection.Games.Remove(removeGame);
                dbConnection.SaveChanges();
                return Ok();
            }
        }

    }
}
