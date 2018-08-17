﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDealer { get; set; }

        public bool IsHuman { get; set; }
    }
}