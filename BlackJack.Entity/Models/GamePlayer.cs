﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity.Models
{
    public class GamePlayer
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int GameId { get; set; }


        public int Score { get; set; }


        public int Bet { get; set; }

        public int RoundScore { get; set; }
    }
}