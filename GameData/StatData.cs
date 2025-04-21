using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class StatData
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }

        public StatData(float maxHealth, float currentHP, float attack, float defense)
        {
            MaxHealth = maxHealth;
            Attack = attack;
            Defense = defense;
            CurrentHealth = currentHP;
        }
    }
}
