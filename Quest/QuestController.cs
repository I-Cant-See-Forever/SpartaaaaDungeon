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
        public int SelectQuestIndex { get; set; } = 0;
        public List<QuestData>[] QuestTypeList { get; private set; }
        public List<QuestData> AcceptableDatas { get; private set; } = new();
        public List<QuestData> ProgressDatas { get; private set; } = new();
        public List<QuestData> FinishDatas { get; private set; } = new();

        public QuestController()
        {
            var playerQuestDatas = GameManager.Instance.PlayerQuestDatas;


            //foreach (var quest in questDatas)
            foreach (var quest in GameManager.Instance.GameQuestDatas)
            {
                var playerQuest = playerQuestDatas.FirstOrDefault(data => data.Data.Title == quest.Title);
                
                if (playerQuest != null)
                {
                    if (playerQuest.IsClear)
                    {
                        FinishDatas.Add(playerQuest.Data);
                    }
                    else
                    {
                        ProgressDatas.Add(playerQuest.Data);
                    }
                }
                else
                {
                    AcceptableDatas.Add(quest);
                }
            }

            QuestTypeList = new List<QuestData>[]
            {
                AcceptableDatas,
                ProgressDatas,
                FinishDatas
            };
        }
      

        public void Accept(QuestData targetQuest)
        {
            AcceptableDatas.Remove(targetQuest);
            ProgressDatas.Add(targetQuest);

            var newPlayerQuest = new PlayerQuestData(targetQuest, false);

            GameManager.Instance.PlayerQuestDatas.Add(newPlayerQuest);
        }

        public bool TryClear(QuestData targetQuest)
        {
            if(targetQuest.Condition.IsAchive())
            {
                ProgressDatas.Remove(targetQuest);
                FinishDatas.Add(targetQuest);

                foreach (var item in GameManager.Instance.PlayerQuestDatas)
                {
                    if (item.Data == targetQuest)
                    {
                        item.IsClear = true;
                    }
                }

                AddReward(targetQuest.Reward);

                return true;
            }

            return false;
        }

        public void AddReward(QuestReward reward)
        {
            var gameManager = GameManager.Instance;
/*
            foreach (var item in reward.ItemCountDict)
            {
            }*/

            if (reward.Gold > 0)
            {
                gameManager.PlayerData.Gold += reward.Gold;
            }

            if (reward.Exp > 0)
            {
                gameManager.PlayerData.addExp(reward.Exp);
            }
        }

        public void UpdateHuntQuest(string enemyName)
        {
            if (string.IsNullOrEmpty(enemyName)) return;

            foreach (var quest in ProgressDatas)
            {
                if(quest.Condition is HuntQuestCondition huntConditon)
                {
                    if (enemyName == huntConditon.EnemyName)
                    {
                        if(huntConditon.CurrentCount < huntConditon.NeedCount)
                        {
                            huntConditon.CurrentCount++;
                        }
                    }
                }
            }
        }
        public void UpdateCollectionQuest(string collectItemName)
        {
            foreach (var quest in ProgressDatas)
            {
                if (quest.Condition is CollectionQuestCondition collectionCondition)
                {
                    if (collectItemName == collectionCondition.CollectName)
                    {
                        if (collectionCondition.CurrentCount < collectionCondition.NeedCount)
                            collectionCondition.CurrentCount++;
                    }
                }
            }
        }

    }
}
