using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestData
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public QuestReward Reward { get; private set; }
        public QuestCondition Condition { get; private set; }
        public QuestData(string title, string description, QuestReward reward, QuestCondition condition)
        {
            Title = title;
            Description = description;
            Reward = reward;
            Condition = condition;
        }
    }
}
