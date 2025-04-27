using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CollectionQuestCondition : QuestCondition
{
    public string CollectName { get; }
    public int NeedCount { get; }
    public int CurrentCount { get; set; }

    public CollectionQuestCondition(string collectName, int needCount)
    {
        CollectName = collectName;
        NeedCount = needCount;
    }

    public override bool IsAchive() => NeedCount <= CurrentCount;
    public override string ProgressText() => $"({CurrentCount} / {NeedCount})";
}