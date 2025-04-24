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

        List<(string, Rectangle)> menuTextRectList = new();

        List<QuestData> targetList;

        public QuestListScene(SceneController controller) : base(controller)
        {
            questController = GameManager.Instance.QuestController;
        }

        public override void Start()
        {
            targetList = questController.QuestTypeList[questController.SelectTypeIndex];

            for (int i = 0; i < targetList.Count; i++)
            {
                menuTextRectList.Add((targetList[i].Title, new(1, i * 2, 100, 1)));
            }

            if(targetList.Count > 0)
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
                        questController.SelectQuestIndex = GetMoveSelectIndex(questController.SelectQuestIndex, -1, menuTextRectList.Count - 1);
                        isCorretInput = true;

                        break;
                    case ConsoleKey.DownArrow:
                        questController.SelectQuestIndex = GetMoveSelectIndex(questController.SelectQuestIndex, +1, menuTextRectList.Count - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.Enter:
                        controller.ChangeScene<QuestInfoScene>();
                        break;
                    case ConsoleKey.Escape:
                        controller.ChangeScene<QuestMainScene>();
                        break;
                }

                if (isCorretInput)
                {
                    DrawRemoveRect(menuTextRectList[tempSelectNum].Item2);

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
            string[] backSpotlight = new string[menuTextRectList.Count];
            string[] selectSign = new string[menuTextRectList.Count];


            backSpotlight[spotLightIndex] = "tmagenta";
            selectSign[spotLightIndex] = "▶ ";

            for (int i = 0; i < targetList.Count; i++)
            {
                DrawString($"《x{menuTextRectList[i].Item2.X},y{menuTextRectList[i].Item2.Y},{backSpotlight[i]}》{targetList[i].Title} \n");
            }
        }
    }
}
