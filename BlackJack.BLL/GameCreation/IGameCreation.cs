﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.GameCreation
{
    public interface IGameCreation
    {
        Player Create(string humanPlayerName, int AmountOfBots);
    }
}
