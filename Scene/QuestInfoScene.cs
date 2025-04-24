using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestInfoScene : Scene
    {
        QuestController questController;

        QuestData targetQuest;

        public QuestInfoScene(SceneController controller) : base(controller)
        {
            questController = GameManager.Instance.QuestController;
        }

        public override void Start()
        {
            targetQuest = questController.QuestTypeList[questController.SelectTypeIndex][questController.SelectQuestIndex];
            
            DrawInfo(targetQuest);
        }


        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow: 
                        questController.UpdateHuntQuest("");
                        break;

                    case ConsoleKey.Enter:
                        if(questController.SelectTypeIndex < 2)
                        {
                            switch (questController.SelectTypeIndex)
                            {
                                case 0: questController.Accept(targetQuest);
                                    controller.ChangeScene<QuestListScene>();
                                    break;
                                case 1:
                                    if (questController.TryClear(targetQuest))
                                    {
                                       controller.ChangeScene<QuestListScene>();
                                    }
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        controller.ChangeScene<QuestListScene>();
                        break;
                }
            }

        }

        public override void End()
        {
        }


        void DrawInfo(QuestData targetQuest)
        {
            var title = targetQuest.Title;
            var description = targetQuest.Description;
            var condition = targetQuest.Condition;
            var conditionProgress = condition.ProgressText();
            var reward = targetQuest.Reward;

            DrawString($"《tgray》{title}\n\n");
            
            DrawString($"{description}\n\n");

            DrawString($"{condition.Description}\n\n");

            DrawString($"{conditionProgress}\n\n");

            DrawString($"보상\n");

            foreach (var item in reward.ItemCountDict)
            {
                DrawString($"{item.Key} + {item.Value}\n");
            }

            if(reward.Gold > 0)
            {
                DrawString($" + {reward.Gold} G\n");
            }

            if(reward.Exp > 0)
            {
                DrawString($" + {reward.Exp} G\n");
            }

            switch(questController.SelectTypeIndex)
            {
                case 0: DrawString($" 수락 : Enter \n"); break;
                case 1: DrawString($" 보상 받기 : Enter \n"); break;
            }
        }
    }
}
