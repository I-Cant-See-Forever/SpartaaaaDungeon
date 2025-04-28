using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class TownScene : Scene
    {
        int currentSelectNum = 0;

        int animIndex = 0;

        (string, Rectangle)[] menuTextRect;


        string[] anims = new string[]
        {
            TextContainer.townImage_1,
            TextContainer.townImage_2,
            TextContainer.townImage_3
        };

        Stopwatch animWatch = new();

        TitleLayout layout = new();


        public List<Scene> SelectScenes;

        public TownScene(SceneController controller) : base(controller) 
        {
            menuTextRect = new (string, Rectangle)[]
            {
                new("상점", new()),
                new("퀘스트", new()),
                new("던전", new())
            };

            for (int i = 0; i < menuTextRect.Length; i++)
            {
                menuTextRect[i].Item2 = 
                    new Rectangle(
                        layout.Menu.X + 6, 
                        layout.Menu.Y + 4 + i * 2, 
                        layout.Menu.Width, 
                        1);
            }
        }

        public override void Start()
        {
            SelectScenes = new()
            {
                controller.GetScene<ShopScene>(),
                controller.GetScene<QuestMainScene>(),
                controller.GetScene<DungeonMainScene>(),
            };


            DrawDirectImage(TextContainer.townTitle, layout.Title.X, layout.Title.Y, ConsoleColor.Yellow);

            DrawMenuText(currentSelectNum);

            DrawStore(animIndex);

            DrawHelp();
        }

        public override void Update()
        {
            if(animWatch.ElapsedMilliseconds == 0)
            {
                animWatch.Start();
            }

            if(animWatch.ElapsedMilliseconds > 1500)
            {
                if(animIndex < 2)
                {
                    animIndex ++;
                }
                else
                {
                    animIndex = 0;
                }

                animWatch.Restart();

                DrawStore(animIndex);
            }


            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                bool isCorretInput = false;
                int tempSelectNum = currentSelectNum;

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentSelectNum = GetMoveSelectIndex(currentSelectNum, -1, menuTextRect.Length - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.DownArrow:
                        currentSelectNum = GetMoveSelectIndex(currentSelectNum, +1, menuTextRect.Length - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.Enter:
                        controller.ChangeScene(SelectScenes[currentSelectNum]);
                        break;
                    case ConsoleKey.P:
                        controller.ChangeScene<StatScene>();
                        break;
                     case ConsoleKey.I:
                        controller.ChangeScene<InventoryScene>();
                        break;
                }
               
                if(isCorretInput)
                {
                    DrawRemoveRect(menuTextRect[tempSelectNum].Item2);

                    DrawMenuText(currentSelectNum);

                    DrawHelp();
                }
            }
        }

        public override void End()
        {
            currentSelectNum = 0;
        }
       
        
        void DrawMenuText(int spotLightIndex)
        {
            string[] backSpotlight = new string[menuTextRect.Length];
            string[] selectSign = new string[menuTextRect.Length];


            backSpotlight[spotLightIndex] = "tmagenta";
            selectSign[spotLightIndex] = "▶ ";

      
            for (int i = 0; i < menuTextRect.Length; i ++)
            {
                DrawString($"《x{menuTextRect[i].Item2.X},y{menuTextRect[i].Item2.Y},{backSpotlight[i]}》{selectSign[i]}{menuTextRect[i].Item1}");
            }
        }

        void DrawStore(int currentIndex)
        {
            DrawDirectImage(anims[currentIndex], layout.Image.X + 1, layout.Image.Y + 1, ConsoleColor.Gray);
        }

        void DrawHelp()
        {
            DrawString($"《x{layout.Menu.X + 1},y{layout.Menu.Height + 6},tYellow》[i] 《》인벤토리 《tyellow》[p] 《》상태창");
        }
    }
}
