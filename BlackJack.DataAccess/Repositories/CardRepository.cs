﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Repositories
{
    public class CardRepository : ICardRepository
    {
        private string _connectionString;


        public CardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Card>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Card> cards = await db.GetAllAsync<Card>();
                return cards;
            }
        }
    }
}
