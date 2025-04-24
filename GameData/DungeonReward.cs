using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DungeonReward
{
    public int EXP { get; set; }
    public int Gold { get; set; }
    public Dictionary<ItemData, int> Items { get; set; }

    public DungeonReward(int exp, int gold, Dictionary<ItemData, int> items)
    {
        EXP = exp;
        Gold = gold;
        Items = items;
    }
}