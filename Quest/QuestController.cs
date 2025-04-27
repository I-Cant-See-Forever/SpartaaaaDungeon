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

        private List<QuestData> dummyQuestDatas = new();
        public QuestController()
        {
            InitializeQuests();

            var gameManager = GameManager.Instance;
            var questDatas = gameManager.GameQuestDatas;
            var playerQuestDatas = gameManager.PlayerQuestDatas;

            if (questDatas == null || questDatas.Count == 0)
            {
                InitializeQuests();
            }

            //foreach (var quest in questDatas)
            foreach (var quest in dummyQuestDatas)
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

        void InitializeQuests()
        {
            if (dummyQuestDatas.Count > 0)
                return;

            dummyQuestDatas.Add(new QuestData(
                title: "고요한 숲의 위협, 상엽 제거",
                description: "숲의 평화를 깨뜨린 상엽을 제거하라.",
                condition: new HuntQuestCondition("임상엽", 3),
                reward: new QuestReward(new Dictionary<string, int>(), 500, 50)
            ));

            dummyQuestDatas.Add(new QuestData(
                title: "늪의 지배자, 민혁 토벌",
                description: "짙은 늪지의 지배자 민혁을 찾아 쓰러뜨려라.",
                condition: new HuntQuestCondition("이민혁", 2),
                reward: new QuestReward(new Dictionary<string, int>(), 800, 70)
            ));

            dummyQuestDatas.Add(new QuestData(
                title: "동굴의 심연, 진안 처단",
                description: "어둠을 뒤흔드는 진안의 기운을 끊어 동굴을 해방하라.",
                condition: new HuntQuestCondition("최진안", 1),
                reward: new QuestReward(new Dictionary<string, int>(), 1000, 100)
            ));

            dummyQuestDatas.Add(new QuestData(
                title: "정유현의 저주받은 눈",
                description: "정유현을 처단하고, 그의 저주받은 눈 3개를 수집하라",
                condition: new CollectionQuestCondition("정유현의 눈", 3),
                reward: new QuestReward(new Dictionary<string, int>(), 600, 60)
           ));
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
