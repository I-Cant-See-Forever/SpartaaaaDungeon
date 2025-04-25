using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonData
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public DungeonReward Reward { get; private set; }

        [JsonIgnore]
        public List<MonsterData> Monsters { get; private set; }
        public DungeonData(string name, int level, DungeonReward reward)
        {
            Name = name;
            Level = level;
            Reward = reward;

            Monsters = new();
        }
    }
}
