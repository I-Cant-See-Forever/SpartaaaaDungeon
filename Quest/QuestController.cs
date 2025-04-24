using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestController
    {
        public int SelectTypeIndex { get; set; } = 0;
        public List<PlayerQuestData>[] QuestTypeList { get; private set; }
        public List<PlayerQuestData> AcceptableDatas { get; private set; } = new();
        public List<PlayerQuestData> ProgressDatas { get; private set; } = new();
        public List<PlayerQuestData> FinishDatas { get; private set; } = new();

        public QuestController()
        {
            var gameManager = GameManager.Instance;
            var questDatas = gameManager.GameQuestDatas;
            var playerQuestDatas = gameManager.PlayerQuestDatas;

            foreach (var quest in questDatas)
            {
                var playerQuest = playerQuestDatas.FirstOrDefault(data => data.Data.Title == quest.Title);
                
                if (playerQuest != null)
                {
                    if (playerQuest.IsClear)
                    {
                        FinishDatas.Add(playerQuest);
                    }
                    else
                    {
                        ProgressDatas.Add(playerQuest);
                    }
                }
                else
                {
                    AcceptableDatas.Add(new PlayerQuestData(quest, false));
                }
            }

            QuestTypeList = new List<PlayerQuestData>[]
            {
                AcceptableDatas,
                ProgressDatas,
                FinishDatas
            };
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
            foreach (var quest in ProgressDatas)
            {
                if(quest.Data.Condition is HuntQuestCondition huntConditon)
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
