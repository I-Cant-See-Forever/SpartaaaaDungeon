using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class HuntQuestCondition : QuestCondition
{
    public string EnemyName { get; private set; }
    public int NeedCount { get; private set; }
    public int CurrentCount { get; set; }

    public HuntQuestCondition(string enemyName, int needCount)
    {
        EnemyName = enemyName;
        NeedCount = needCount;
        CurrentCount = 0; // 기본값
    }

    public override bool IsAchive() => NeedCount <= CurrentCount;

    public override string ProgressText() => $"({CurrentCount} / {NeedCount})";

}