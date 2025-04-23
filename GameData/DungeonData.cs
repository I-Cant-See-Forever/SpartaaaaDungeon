using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonData
    {
        public virtual string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float CurrentHealth { get; set; }

        public float MaxHealth { get; set; }

        public DungeonData(string name, int level, float attack, float currentHealth, float maxHealth)
        {
            Name = name;
            Level = level;
            Attack = attack;
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public DungeonData()
        {

        }


    }
}
