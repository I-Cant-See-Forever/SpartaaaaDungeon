using System;
using System.Collections.Generic;
using System.Linq;
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

        public int currentExp { get; set; }
        public int maxExp = 10;

        public StatData Stat { get; set; }

        public PlayerData(string name, GameEnum.ClassType classType, int level, float gold, StatData statData)
        {
            Name = name;
            ClassType = classType;
            Level = level; 
            Gold = gold; 
            Stat = statData;
        }

        public void addExp(float exp)
        {
            currentExp++;
            if(currentExp >= maxExp)
            {
                Level++;
                maxExp += 10;
            }
        }

    }
}
