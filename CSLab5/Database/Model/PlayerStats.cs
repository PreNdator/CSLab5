﻿
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    class PlayerStats
    {
        public int Id {  get; set; }
        public int Level { get; set; } = 1;
        public int Strength { get; set; } = 0;
        public int Intelligence { get; set; } = 0;
        public int Agility { get; set; } = 0;
        public int PlayerId { get; set; } 
        public Player? Player { get; set; }

    }
}
