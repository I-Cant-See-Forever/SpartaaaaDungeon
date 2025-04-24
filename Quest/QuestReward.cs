using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuestReward
{
    public Dictionary<string, int> ItemCountDict;
    public float Gold { get; set; }
    public float Exp { get; set; }

    public QuestReward(Dictionary<string, int> itemCountDict, float gold, float exp)
    {
        ItemCountDict = itemCountDict;
        Gold = gold;
        Exp = exp;
    }
}