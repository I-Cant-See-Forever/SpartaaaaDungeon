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
        private QuestController questController;

        //List<(string, Rectangle)> menuTextRectList = new();
        List<Rectangle> menuListRects = new();

        List<QuestData> targetList;

        QuestMenuInfoLayout layout = new();

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
            Frame();
            DrawTitle(questController.SelectTypeIndex);

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
                        var targetQuest = questController.QuestTypeList[questController.SelectTypeIndex][questController.SelectQuestIndex];

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

        void DrawTitle(int num)
        {
            if (num == 0)
            {
                string title = $"《x{layout.Title.X},y{layout.Title.Y},tyellow》" +
                $"███╗   ██╗███████╗██╗    ██╗   ██████╗ ██╗   ██╗███████╗███████╗████████╗ \r\n" +
                $"████╗  ██║██╔════╝██║    ██║  ██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝ \r\n" +
                $"██╔██╗ ██║█████╗  ██║ █╗ ██║  ██║   ██║██║   ██║█████╗  ███████╗   ██║    \r\n" +
                $"██║╚██╗██║██╔══╝  ██║███╗██║  ██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║    \r\n" +
                $"██║ ╚████║███████╗╚███╔███╔╝  ╚██████╔╝╚██████╔╝███████╗███████║   ██║    \r\n" +
                $"╚═╝  ╚═══╝╚══════╝ ╚══╝╚══╝    ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝    ";
                string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Title.X},tyellow》");

                DrawString(replaceTitle);
            }
            else if (num == 1)
            {
                string title = $"《x{layout.Title.X},y{layout.Title.Y},tyellow》" +
                $"██████╗ ██╗      █████╗ ██╗   ██╗   ██████╗ ██╗   ██╗███████╗███████╗████████╗ \r\n" +
                $"██╔══██╗██║     ██╔══██╗╚██╗ ██╔╝  ██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝ \r\n" +
                $"██████╔╝██║     ███████║ ╚████╔╝   ██║   ██║██║   ██║█████╗  ███████╗   ██║    \r\n" +
                $"██╔═══╝ ██║     ██╔══██║  ╚██╔╝    ██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║    \r\n" +
                $"██║     ███████╗██║  ██║   ██║     ╚██████╔╝╚██████╔╝███████╗███████║   ██║    \r\n" +
                $"╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝      ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝    ";
                string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Title.X},tyellow》");

                DrawString(replaceTitle);
            }
            else
            {
                string title = $"《x{layout.Title.X},y{layout.Title.Y},tyellow》" +
                $"███████╗███╗   ██╗██████╗    ██████╗ ██╗   ██╗███████╗███████╗████████╗ \r\n" +
                $"██╔════╝████╗  ██║██╔══██╗  ██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝ \r\n" +
                $"█████╗  ██╔██╗ ██║██║  ██║  ██║   ██║██║   ██║█████╗  ███████╗   ██║    \r\n" +
                $"██╔══╝  ██║╚██╗██║██║  ██║  ██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║    \r\n" +
                $"███████╗██║ ╚████║██████╔╝  ╚██████╔╝╚██████╔╝███████╗███████║   ██║    \r\n" +
                $"╚══════╝╚═╝  ╚═══╝╚═════╝    ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝    ";
                string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Title.X},tyellow》");

                DrawString(replaceTitle);
            }



        }

        void Frame() // 테두리 그리는 함수!
        {
            DrawString($"《x0,y0》┏━《l{Console.WindowWidth - 5}》━《》━┓");

            for (int y = 1; y < Console.WindowHeight - 1; y++)
            {
                DrawString($"《x0,y{y}》┃《x{Console.WindowWidth - 2}》┃");
            }

            DrawString($"《x0,y{Console.WindowHeight - 1}》┗━《l{Console.WindowWidth - 5}》━《》━┛");
        }

        void DrawQuestList(List<QuestData> targetList, int spotLightIndex)
        {
            string[] backSpotlight = new string[targetList.Count];
            string[] selectSign = new string[targetList.Count];


            backSpotlight[spotLightIndex] = "tmagenta";
            selectSign[spotLightIndex] = "▶ ";

            for (int i = 0; i < targetList.Count; i++)
            {
                DrawString($"《x{layout.Left.X + 1},y{menuListRects[i].Y},{backSpotlight[i]}》{targetList[i].Title} \n");
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

            DrawString($"《x{posX},y{posY},tgray》{title}\n\n");

            DrawString($"《x{posX},y{posY + 2},tgray》{description}\n\n");

            DrawString($"《x{posX},y{posY + 4},tgray》{condition.Description}\n\n");

            DrawString($"《x{posX},y{posY + 6},tgray》{conditionProgress}\n\n");

            DrawString($"《x{posX},y{posY + 8},tgray》보상\n");

            int rewardDeltaY = 0;
            if (reward.Gold > 0)
            {
                DrawString($"《x{posX},y{posY + 10},tgray》 + {reward.Gold} G\n");
                rewardDeltaY++;
            }

            if (reward.Exp > 0)
            {
                DrawString($"《x{posX},y{posY + 10 + rewardDeltaY},tgray》 + {reward.Exp} G\n");
                rewardDeltaY++;
            }

            int itemIndex = 0;
            foreach (var item in reward.ItemCountDict)
            {
                DrawString($"《x{posX},y{posY + 10 + itemIndex + rewardDeltaY},tgray》{item.Key} + {item.Value}\n");

                itemIndex++;
            }

            switch (questController.SelectTypeIndex)
            {
                case 0: DrawString($"《x{posX},y{posY+ 17},tgray》 수락 : Enter \n"); break;
                case 1: DrawString($"《x{posX},y{posY+ 17},tgray》 보상 받기 : Enter \n"); break;
            }
        }
    }
}
