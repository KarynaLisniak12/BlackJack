﻿using System;

namespace BlackJack.Entities.Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }
    }
}
