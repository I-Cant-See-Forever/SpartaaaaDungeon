using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestController
    {
        public List<QuestData> QuestDatas { get; private set; }
        public QuestController(List<QuestData> questDatas)
        {
            QuestDatas = questDatas;
        }

        public void AddReward(QuestReward reward)
        {
            var gameManager = GameManager.Instance;

            foreach (var item in reward.ItemCountDict)
            {
            }

            if (reward.Gold > 0)
            {
                gameManager.PlayerData.Gold += reward.Gold;
            }

            if (reward.Exp > 0)
            {
            }
        }

        public void UpdateHuntQuest(string enemyName)
        {
            foreach (var quest in QuestDatas)
            {
                if(quest.Condition is HuntQuestCondition huntConditon)
                {
                    if(enemyName == huntConditon.EnemyName || enemyName == "")
                    {
                        if(huntConditon.CurrentCount < huntConditon.NeedCount)
                        {
                            huntConditon.CurrentCount++;
                        }
                    }
                }
            }
        }

    }
}
