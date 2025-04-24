using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonMainScene : Scene
    {

        DungeonController dungeonController;

        TitleLayout layout = new();


        List<(string, Rectangle)> menuTextRects = new();

        public DungeonMainScene(SceneController controller) : base(controller)
        {
            dungeonController = GameManager.Instance.DungeonController;

            for (int i = 0; i < dungeonController.DungeonDatas.Count; i++)
            {
                menuTextRects.Add(
                    new(dungeonController.DungeonDatas[i].Name,
                    new Rectangle(
                        layout.Menu.X + 9,
                        layout.Menu.Y + 4 + i * 2,
                        layout.Menu.Width,
                        1)));
            }
        }

        public override void Start()
        {
            DrawString("[던전입구]\n\n");
            DrawString("던전을 고르시오\n\n");

            DrawMenuText(dungeonController.SelectIndex);
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                bool isCorretInput = false;

                int tempSelectNum = dungeonController.SelectIndex;

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        dungeonController.SelectIndex = GetMoveSelectIndex(dungeonController.SelectIndex, -1, menuTextRects.Count - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.DownArrow:
                        dungeonController.SelectIndex = GetMoveSelectIndex(dungeonController.SelectIndex, +1, menuTextRects.Count - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.Enter:
                        dungeonController.SetDungeon(dungeonController.SelectIndex);
                        controller.ChangeScene<DungeonBattleScene>();
                        break;
                    case ConsoleKey.Escape:
                        controller.ChangeScene<TownScene>();
                        break;
                }

                if (isCorretInput)
                {
                    DrawRemoveRect(menuTextRects[tempSelectNum].Item2);

                    DrawMenuText(dungeonController.SelectIndex);
                }
            }
        }

        public override void End()
        {
        }

        void DrawMenuText(int spotLightIndex)
        {
            string[] backSpotlight = new string[menuTextRects.Count];
            string[] selectSign = new string[menuTextRects.Count];


            backSpotlight[spotLightIndex] = "tmagenta";
            selectSign[spotLightIndex] = "▶ ";


            for (int i = 0; i < menuTextRects.Count; i++)
            {
                DrawString($"《x{menuTextRects[i].Item2.X},y{menuTextRects[i].Item2.Y},{backSpotlight[i]}》{selectSign[i]}{menuTextRects[i].Item1}");
            }
        }
    }
}
