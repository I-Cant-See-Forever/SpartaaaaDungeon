using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestListScene : Scene
    {

        string[] titles = new string[]
        {
            TextContainer.newQuestTitle,
            TextContainer.playQuestTitle,
            TextContainer.endQuestTitle
        };

        QuestMenuInfoLayout layout = new();


        List<Rectangle> menuListRects = new();

        List<QuestData> targetList = new();

        QuestController questController;


        public QuestListScene(SceneController controller) : base(controller)
        {
            questController = GameManager.Instance.QuestController;


            menuListRects = new();

            for (int i = 0; i < layout.Left.Height; i++)
            {
                menuListRects.Add(new(1, layout.Left.Y + i * 2, layout.Left.Width, 1));
            }
        }

        public override void Start()
        {
            DrawFrame();
            DrawDirectImage(titles[questController.SelectTypeIndex], layout.Title.X, layout.Title.Y, ConsoleColor.Yellow);

            targetList = questController.QuestTypeList[questController.SelectTypeIndex];

            if (targetList.Count > 0)
            {
                DrawQuestList(targetList, questController.SelectQuestIndex);
            }
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                bool isCorretInput = false;
                int tempSelectNum = questController.SelectTypeIndex;

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        questController.SelectQuestIndex = GetMoveSelectIndex(questController.SelectQuestIndex, -1, targetList.Count - 1);
                        isCorretInput = true;

                        break;
                    case ConsoleKey.DownArrow:
                        questController.SelectQuestIndex = GetMoveSelectIndex(questController.SelectQuestIndex, +1, targetList.Count - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.Enter:

                        var targetTypeQuestList = questController.QuestTypeList[questController.SelectTypeIndex];

                        if (targetTypeQuestList.Count > 0)
                        {
                            var targetQuest = targetTypeQuestList[questController.SelectQuestIndex];

                            if (questController.SelectTypeIndex < 2)
                            {
                                switch (questController.SelectTypeIndex)
                                {
                                    case 0:
                                        questController.Accept(targetQuest);
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
                        }
                        break;

                    case ConsoleKey.Escape:
                        controller.ChangeScene<QuestMainScene>();
                        break;
                }

                if (isCorretInput)
                {
                    DrawRemoveRect(menuListRects[tempSelectNum]);

                    if (targetList.Count > 0)
                    {
                        DrawQuestList(targetList, questController.SelectQuestIndex);
                    }
                }
            }
        }

        public override void End()
        {
        }


        void DrawQuestList(List<QuestData> targetList, int spotLightIndex)
        {
            string[] backSpotlight = new string[targetList.Count];
            string[] selectSign = new string[targetList.Count];


            backSpotlight[spotLightIndex] = "tmagenta";
            selectSign[spotLightIndex] = "▶ ";

            for (int i = 0; i < targetList.Count; i++)
            {
                DrawString($"《x{layout.Left.X + 3},y{menuListRects[i].Y},{backSpotlight[i]}》{targetList[i].Title} \n");
            }

            DrawInfo(targetList[spotLightIndex]);
        }

        void DrawInfo(QuestData targetQuest)
        {
            var title = targetQuest.Title;
            var description = targetQuest.Description;
            var condition = targetQuest.Condition;
            var conditionProgress = condition.ProgressText();
            var reward = targetQuest.Reward;

            int posX = layout.Right.X;
            int posY = layout.Right.Y;

            DrawRemoveRect(layout.Right);

            switch(condition)
            {
                case HuntQuestCondition:
                    DrawString($"《x{posX},y{posY},tred》[ {title} ]"); break;
                case CollectionQuestCondition:
                    DrawString($"《x{posX},y{posY},tdarkyellow》[ {title} ]"); break;
            }

            DrawString($"《x{posX},y{posY + 2},tgray》{description}");

            DrawString($"《x{posX},y{posY + 12},tgray》{condition.Description}《tdarkgray》 {conditionProgress}");

            DrawString($"《x{posX},y{posY + 14},tgray》보상");

            int rewardDeltaY = 0;
            if (reward.Gold > 0)
            {
                DrawString($"《x{posX + 7},y{posY + 14},tdarkgray》+ {reward.Gold} G");
                rewardDeltaY++;
            }

            if (reward.Exp > 0)
            {
                DrawString($"《x{posX + 7},y{posY + 14 + rewardDeltaY},tdarkgray》+ {reward.Exp} G");
                rewardDeltaY++;
            }

            int itemIndex = 0;
            foreach (var item in reward.ItemCountDict)
            {
                DrawString($"《x{posX + 7},y{posY + 14 + itemIndex + rewardDeltaY},tdarkgray》+ {item.Key} ({item.Value})");

                itemIndex++;
            }

            switch (questController.SelectTypeIndex)
            {
                case 0: DrawString($"《x{posX},y{posY+ 19},tgreen》 수락 : Enter "); break;
                case 1: 
                    if(condition.IsAchive())
                    {
                        DrawString($"《x{posX},y{posY + 19},tgreen》 보상 받기 : Enter "); 
                    }
                    else
                    {
                        DrawString($"《x{posX},y{posY + 19},tdarkgray》 보상 받기 : Enter ");
                    }
                    break;
            }
        }
    }
}
