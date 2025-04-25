using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class DungeonReward
{
    public int Gold { get; set; }
    public Dictionary<string, int> ItemsNameDict { get; set; }

    [JsonIgnore]
    public int EXP { get; set; }

    public DungeonReward(int exp, int gold, Dictionary<string, int> itemsName)
    {
        EXP = exp;
        Gold = gold;
        ItemsNameDict = itemsName;
    }
}