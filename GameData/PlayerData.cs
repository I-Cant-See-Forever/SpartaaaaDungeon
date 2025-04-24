using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class PlayerData
    {
        public string Name { get; set; }
        public GameEnum.ClassType ClassType { get; set; }
        public int Level { get; set; }
        public float Gold { get; set; }

        public float CurrentExp { get; set; }
        public float MaxExp { get; set; }

        public StatData Stat { get; set; }

        public PlayerData(string name, GameEnum.ClassType classType, int level, float gold, StatData statData)
        {
            Name = name;
            ClassType = classType;
            Level = level; 
            Gold = gold; 
            Stat = statData;

            MaxExp = level * 10;
        }

        public void addExp(float exp)
        {
            CurrentExp += exp;

            if(CurrentExp >= MaxExp)
            {
                Level++;
                CurrentExp = 0;

                MaxExp = Level * 10;
            }
        }

    }
}
