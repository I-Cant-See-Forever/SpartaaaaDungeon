using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestScene : Scene
    {
        List<QuestData> questDatas;
        public QuestScene(SceneController controller) : base(controller)
        {
            questDatas = GameManager.Instance.QuestDatas;
        }

        public override void Start()
        {
            DrawString(
                $"{questDatas[0].Title}\n\n" +
                $"{questDatas[0].Description}\n\n" +
                $"{questDatas[0].Condition.Description}  " +
                $"{questDatas[0].Condition.ProgressText()}"
                );
        }

        public override void Update()
        {
            Console.ReadKey();

            Console.Clear();

            GameManager.Instance.QuestController.UpdateHuntQuest("");

            if (questDatas[0].Condition.IsAchive())
            {
                DrawString(
                $"{questDatas[0].Title}\n\n" +
                $"{questDatas[0].Description}\n\n" +
                $"{questDatas[0].Condition.Description}  " +
                $"{questDatas[0].Condition.ProgressText()}\n\n\n" +
                $"완료!\n\n\n" +
                $"보상\n");

                DrawRewardText(questDatas[0].Reward);

                GameManager.Instance.QuestController.AddReward(questDatas[0].Reward);

                DrawString($"{GameManager.Instance.PlayerData.Gold}");
            }
            else
            {
                DrawString(
                  $"{questDatas[0].Title}\n\n" +
                  $"{questDatas[0].Description}\n\n" +
                  $"{questDatas[0].Condition.Description}  " +
                  $"{questDatas[0].Condition.ProgressText()}"
                  );

                DrawString($"{GameManager.Instance.PlayerData.Gold}");
            }
        }

        public override void End()
        {
        }



        void DrawRewardText(QuestReward reward)
        {
            foreach (var item in reward.ItemCountDict)
            {
                DrawString($"{item.Key.Name} + {item.Value}\n\n");
            }

            if (reward.Gold > 0)
            {
                DrawString($"+ {reward.Gold} G\n\n");
            }

            if (reward.Exp > 0)
            {
                DrawString($"+ {reward.Exp} EXP\n\n");
            }
        }
    }
}
