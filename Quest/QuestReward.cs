using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestReward
    {
        public Dictionary<ItemData, int> ItemCountDict;
        public float Gold { get; set; }
        public float Exp { get; set; }

        public QuestReward(Dictionary<ItemData, int> itemCountDict, float gold, float exp)
        {
            ItemCountDict = itemCountDict;
            Gold = gold;
            Exp = exp;
        }
    }
}
