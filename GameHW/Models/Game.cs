using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameHW.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public string GameTitle { get; set; }

    }
}