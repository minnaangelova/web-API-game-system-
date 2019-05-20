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

        private readonly DatabaseContext dbc = new DatabaseContext();

        public GameController()
        {
           
        }

       // List all available games

        [Route("api/games/all")]
        [HttpGet]

        public List<Game> GetAll()
        {
            var getAll = this.dbc.Games.ToList();
            
            return getAll;
        }

        //List game by given game name.
        //If game doesn’t exist, status 404 must be returned to the user.

        [Route("api/games/{GameTitle}")]
        [HttpGet]

        public IHttpActionResult GetByTitle(string GameTitle)
        {
            var gbt = this.dbc.Games.FirstOrDefault((g) => g.GameTitle == GameTitle);

            if (gbt == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(gbt);
            }
        }


        //List game by given game ID. 
        //If game doesn’t exist, status 404 must be returned to the user.

        [Route("api/games/{GameId:int}")]
        [HttpGet]

        public IHttpActionResult GetById(int GameId)
        {
            var gbt = this.dbc.Games.FirstOrDefault((g) => g.GameId == GameId);

            if (gbt == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(gbt);
            }
        }

        //Adds new game. 
        //If game with the same name already exists, 
        //status 400 must be returned to the user.



        [Route("api/games")]
        [HttpPost]

        public IHttpActionResult PostNewGame([FromBody]Game gameP)
        {
            
                var sameExst = dbc.Games.Any(g => g.GameTitle == gameP.GameTitle);

            if (!sameExst)
            {
                dbc.Games.Add(gameP);
                dbc.SaveChanges();
                return Ok(gameP);

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

        public IHttpActionResult PutToModifyGame(int GameId, [FromBody]Game gameP)
        {
            var doesExst = this.dbc.Games.Any((g) => g.GameId == GameId);

            if (!doesExst)
            {
                return NotFound();
            }
            else
            {
                var modGame = this.dbc.Games.FirstOrDefault((g) => g.GameId == GameId);
                modGame.GameTitle = gameP.GameTitle;
                dbc.SaveChanges();
                return Ok();
            }
        }

        //Delete existing game.
        //If game doesn’t exist, status 404 must be returned to the user.

        [Route("api/games/{GameId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteGame(int GameId)
        {
            var removeGame = this.dbc.Games.FirstOrDefault((g) => g.GameId == GameId);
            var doesExst = this.dbc.Games.Any((g) => g.GameId == GameId);

            if (!doesExst)
            {
                return NotFound();
            }
            else
            {
                this.dbc.Games.Remove(removeGame);
                dbc.SaveChanges();
                return Ok();
            }
        }

    }
}
