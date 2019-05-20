using GameHW.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace GameHW.Context
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {


            base.Seed(context);

              var games = new List<Game>
              {
                new Game
                {
                    GameId = 1,
                    GameTitle = "Mortal Combat 5"
                },

                new Game
                {
                    GameId = 2,
                    GameTitle = "Fifa 19"
                },

                new Game
                {
                    GameId = 3,
                    GameTitle = "LoL"
                },

                new Game
                {
                    GameId = 4,
                    GameTitle = "CS"
                },

                new Game
                {
                    GameId = 5,
                    GameTitle = "Angry Birds"
                }
            };

            games.ForEach(s => context.Games.Add(s));
            context.SaveChanges();

        }
    
    }
}