using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CollectionQuestCondition : QuestCondition
{
    public string CollectName { get; private set; }
    public int NeedCount { get; private set; }
    public int CurrentCount { get; set; }

    public override bool IsAchive() => NeedCount <= CurrentCount;
    public override string ProgressText() => $"({CurrentCount} / {NeedCount})";
}