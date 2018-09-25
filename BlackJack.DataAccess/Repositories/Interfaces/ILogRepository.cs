﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAll();

        Task Create(long gameId, string message);

        Task CreateMany(IEnumerable<Log> logs);
    }
}
