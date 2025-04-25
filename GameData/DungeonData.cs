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
        public string Name { get; set; }
        public int Level { get; set; }
        public DungeonReward Reward { get; set; }

        [JsonIgnore]
        public List<MonsterData> Monsters { get; private set; } = new();
    }
}
