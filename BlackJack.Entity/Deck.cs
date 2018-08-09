﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity
{
    public class Deck
    {
        public int Id { get; set; }

        public virtual List<Card> Cards { get; set; }
    }
}
