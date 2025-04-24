using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class MonsterData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float BaseDamage { get; set; }
        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }

        public MonsterData(string name, int level, float baseDamage, float maxHealth)
        {
            Name = name;
            Level = level;
            BaseDamage = baseDamage;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }
    }
}
