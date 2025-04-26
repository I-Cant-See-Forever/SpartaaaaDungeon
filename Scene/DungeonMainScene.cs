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
        TitleLayout layout = new();


        List<(string, Rectangle)> menuTextRects = new();
        List<(string, Rectangle)> imageRects = new();


        PlayerData playerData;
        DungeonController dungeonController;


        string[] pictures = new string[]
        {
            TextContainer.forsetDungeon,
            TextContainer.swampDungeon,
            TextContainer.caveDungeon
        };


        public DungeonMainScene(SceneController controller) : base(controller)
        {
            playerData = GameManager.Instance.PlayerData;
            dungeonController = GameManager.Instance.DungeonController;

            for (int i = 0; i < dungeonController.DungeonDatas.Count; i++)
            {
                menuTextRects.Add(
                    new(dungeonController.DungeonDatas[i].Name,
                    new Rectangle(
                        layout.Menu.X + 6,
                        layout.Menu.Y + 4 + i * 2,
                        layout.Menu.Width,
                        1)));
            }

            int startX = layout.Image.X + 32;
            int startY = layout.Image.Y - 8;
            int width = Console.WindowWidth - startX;
            int height = Console.WindowHeight - startY;

            for (int i = 0; i < dungeonController.DungeonDatas.Count; i++)
            {
                imageRects.Add(
                    new(dungeonController.DungeonDatas[i].Name,
                    new Rectangle(
                        startX,
                        startY,
                        width,
                        height)));
            }
        }


        public override void Start()
        {
            DrawDirectImage(TextContainer.dungeonTitle, layout.Title.X, layout.Title.Y, ConsoleColor.Red);

            //초기화 첫 인덱스 ... 그림 drawstring ... 던전 선택지 인덱스 dungeonIndex 넣어주기 

            DrawBackground(dungeonController.dungeonIndex);
            DrawMenuText(dungeonController.dungeonIndex);
        }



        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                bool isCorretInput = false;

                int tempSelectNum = dungeonController.dungeonIndex;

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        dungeonController.dungeonIndex = GetMoveSelectIndex(dungeonController.dungeonIndex, -1, menuTextRects.Count - 1);
                        isCorretInput = true;
                        break;

                    case ConsoleKey.DownArrow:
                        dungeonController.dungeonIndex = GetMoveSelectIndex(dungeonController.dungeonIndex, +1, menuTextRects.Count - 1);
                        isCorretInput = true;
                        break;

                    case ConsoleKey.Enter:

                        if (playerData.DungeonClearedLevel >= dungeonController.dungeonIndex)
                        {
                            dungeonController.SetDungeon(dungeonController.dungeonIndex);
                            controller.ChangeScene<DungeonBattleScene>();
                        }
                        break;

                    case ConsoleKey.Escape:
                        controller.ChangeScene<TownScene>();
                        break;

                    case ConsoleKey.P:
                        controller.ChangeScene<StatScene>();
                        break;

                    case ConsoleKey.I:
                        controller.ChangeScene<InventoryScene>();
                        break;

                }

                if (isCorretInput)
                {
                    DrawRemoveRect(menuTextRects[tempSelectNum].Item2);
                    DrawRemoveRect(imageRects[tempSelectNum].Item2);
                    DrawBackground(dungeonController.dungeonIndex);
                    DrawMenuText(dungeonController.dungeonIndex);
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


            backSpotlight[spotLightIndex] = dungeonController.dungeonIndex > playerData.DungeonClearedLevel ? "tdarkgray" : "tmagenta";
            selectSign[spotLightIndex] = "▶ ";


            for (int i = 0; i < menuTextRects.Count; i++)
            {
                DrawString($"《x{menuTextRects[i].Item2.X},y{menuTextRects[i].Item2.Y},{backSpotlight[i]}》{selectSign[i]}{menuTextRects[i].Item1}");
            }
        }


        void DrawBackground(int currentIndex)
        {
            DrawDirectImage(pictures[currentIndex], layout.Image.X + 38, layout.Image.Y - 4, ConsoleColor.Red);
        }
    }
}
