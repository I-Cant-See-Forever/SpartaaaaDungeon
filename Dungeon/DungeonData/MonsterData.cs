using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.Dungeon.DungeonData
{
    public class MonsterData
    {
        public string Name { get; set; }
        public int Level { get; set; }   
        public float Attack { get; set; }
        public float Health { get; set; }

        public MonsterData(string name,int level, float attack, float health)
        {
            Name = name;        
            Level = level;
            Attack = attack;
            Health = health;
        }

        public MonsterData()
        {

        }
    }
}
