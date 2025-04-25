using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class PlayerQuestData
    {
        public QuestData Data { get; set; }
        public bool IsClear { get; set; }

        public PlayerQuestData(QuestData data, bool isClear)
        {
            Data = data;
            IsClear = isClear;
        }
    }
}
