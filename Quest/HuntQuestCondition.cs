using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class HuntQuestCondition : QuestCondition
    {
        public string EnemyName { get; private set; }
        public int NeedCount { get; private set; }
        public int CurrentCount { get; set; }
        public HuntQuestCondition(string enemyName, string description, int needCount) : base(description) 
        {
            EnemyName = enemyName;
            NeedCount = needCount;
        }

        public override bool IsAchive() => NeedCount <= CurrentCount;

        public override string ProgressText() => $"({CurrentCount} / {NeedCount})";
        
    }
}
