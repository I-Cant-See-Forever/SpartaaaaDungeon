using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class Quest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestReward QuestReward { get; set; }
        public Quest(string title, string description, QuestReward questReward)
        {
            Title = title;
            Description = description;
            QuestReward = questReward;
        }
    }
}
