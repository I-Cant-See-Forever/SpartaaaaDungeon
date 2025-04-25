using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class MonsterData : CharacterData
    {
        [JsonIgnore]
        public string ImageString { get; set; }
    }
}
