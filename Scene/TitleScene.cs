using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
 
    public class TitleScene : Scene
    {
        TitleLayout layout = new();

        (string, Rectangle)[] menuTextRect;

        int currentSelectNum = 0;

        int animIndex = 0;

        Stopwatch animWatch = new();

        public List<Scene> SelectScenes;

        public TitleScene(SceneController controller) : base(controller) 
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
                        layout.Menu.X + 9, 
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
            };


            //string title = $"《x{layout.Title.X},y{layout.Title.Y},tyellow》" +
            //    $"████████╗ ██████╗ ██╗    ██╗███╗   ██╗\r\n" +
            //    $"╚══██╔══╝██╔═══██╗██║    ██║████╗  ██║\r\n" +
            //    $"   ██║   ██║   ██║██║ █╗ ██║██╔██╗ ██║\r\n" +
            //    $"   ██║   ██║   ██║██║███╗██║██║╚██╗██║\r\n" +
            //    $"   ██║   ╚██████╔╝╚███╔███╔╝██║ ╚████║\r\n" +
            //    $"   ╚═╝    ╚═════╝  ╚══╝╚══╝ ╚═╝  ╚═══╝";
            //string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Title.X},tyellow》");

            //DrawString(replaceTitle);

            //DrawMenuText(currentSelectNum);

            DrawStore(animIndex);
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

                //switch (input.Key)
                //{
                //    case ConsoleKey.UpArrow:
                //        currentSelectNum = GetMoveSelectIndex(currentSelectNum, -1, menuTextRect.Length - 1);
                //        isCorretInput = true;
                //        break;
                //    case ConsoleKey.DownArrow:
                //        currentSelectNum = GetMoveSelectIndex(currentSelectNum, +1, menuTextRect.Length - 1);
                //        isCorretInput = true;
                //        break;
                //    case ConsoleKey.Enter:
                //        controller.ChangeScene(SelectScenes[currentSelectNum]);
                //        break;
                //    case ConsoleKey.Escape:
                //        controller.ChangeScene<StatScene>();
                //        break;
                //}
               
                //if(isCorretInput)
                //{
                //    DrawRemoveRect(menuTextRect[tempSelectNum].Item2);

                //    DrawMenuText(currentSelectNum);
                //}
            }
        }

        public override void End()
        {
            currentSelectNum = 0;
        }


        //void DrawMenuText(int spotLightIndex)
        //{
        //    string[] backSpotlight = new string[menuTextRect.Length];
        //    string[] selectSign = new string[menuTextRect.Length];


        //    backSpotlight[spotLightIndex] = "tmagenta";
        //    selectSign[spotLightIndex] = "▶ ";


        //    for (int i = 0; i < menuTextRect.Length; i ++)
        //    {
        //        DrawString($"《x{menuTextRect[i].Item2.X},y{menuTextRect[i].Item2.Y},{backSpotlight[i]}》{selectSign[i]}{menuTextRect[i].Item1}");
        //    }
        //}

        //void DrawStore(int currentIndex)
        //{
        //    int startPosX = layout.Image.X;


        //    string[] pictures = new string[]
        //    {
        //      $"《x{startPosX},y{layout.Image.Y - 1}》" +

        //        $"\r\n ███████╗███████╗██████╗  █████╗ ██████╗ ████████╗ █████╗ " + 
        //        $"\r\n ╚══███╔╝╚══███╔╝██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔══██╗ " + 
        //        $"\r\n   ███╔╝   ███╔╝ ██████╔╝███████║██████╔╝   ██║   ███████║ " + 
        //        $"\r\n  ███╔╝   ███╔╝  ██╔═══╝ ██╔══██║██╔══██╗   ██║   ██╔══██║ " + 
        //        $"\r\n ███████╗███████╗██║     ██║  ██║██║  ██║   ██║   ██║  ██║ " + 
        //        $"\r\n ╚══════╝╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝ ",

        //      $"《x{startPosX},y{layout.Image.Y - 1}》" +
        //        $"\r\n ███████╗███████╗██████╗  █████╗ ██████╗ ████████╗ █████╗ " +
        //        $"\r\n ╚══███╔╝╚══███╔╝██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔══██╗ " +
        //        $"\r\n   ███╔╝   ███╔╝ ██████╔╝███████║██████╔╝   ██║   ███████║ " +
        //        $"\r\n  ███╔╝   ███╔╝  ██╔═══╝ ██╔══██║██╔══██╗   ██║   ██╔══██║ " +
        //        $"\r\n ███████╗███████╗██║     ██║  ██║██║  ██║   ██║   ██║  ██║ " +
        //        $"\r\n ╚══════╝╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝ ",

        //         $"《x{startPosX},y{layout.Image.Y - 1}》" +
        //        $"\r\n ███████╗███████╗██████╗  █████╗ ██████╗ ████████╗ █████╗ " +
        //        $"\r\n ╚══███╔╝╚══███╔╝██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔══██╗ " +
        //        $"\r\n   ███╔╝   ███╔╝ ██████╔╝███████║██████╔╝   ██║   ███████║ " +
        //        $"\r\n  ███╔╝   ███╔╝  ██╔═══╝ ██╔══██║██╔══██╗   ██║   ██╔══██║ " +
        //        $"\r\n ███████╗███████╗██║     ██║  ██║██║  ██║   ██║   ██║  ██║ " +
        //        $"\r\n ╚══════╝╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝ "
        //    };
        //    string input = pictures[currentIndex].Replace("\r\n", $"\n《x{startPosX}》");


        //    DrawString(input);
        //}


        void DrawStore(int currentIndex)
        {
            int startPosX = layout.Image.X;


            string[] pictures = new string[]
            {
              $"《x{startPosX},y{layout.Image.Y - 5}》" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⠛⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⣀⣀⣀⣀⡀⣀⣀⣀⣀⣀⣀⣀⡀⠀⣀⠀⣀⣀⣀⠀⠀⠀⢀⣀⣀⣀⠀⠀⠀⠀⣀⡀⢀⣀⣀⣀⣀⡇⢰⣿⣇⣀⡀⠀⠀⢀⣀⣀⡀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⡎⠉⠉⠉⠉⠉⠉⢉⣽⣷⠋⠉⠉⠉⠉⢩⣿⣷⡖⠙⣷⠋⠉⠉⢦⡄⣴⠎⡉⠉⠉⢲⣤⢠⣶⠋⢹⡞⠉⠉⣽⣿⡁⢨⡍⠉⣽⣿⢠⡖⠊⠉⠉⠑⢶⡄⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⡇⢠⣾⣿⣿⠖⠀⣾⠛⢹⣴⣿⣿⠇⠀⢺⣿⢿⡆⠀⣾⣷⣦⠀⣿⡟⣿⣶⣿⣷⡄⠸⣿⣿⣦⠀⢾⣷⠀⢶⣿⠻⡇⢸⣿⣿⠋⠀⡇⣴⣶⣶⣶⠀⢨⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠷⠿⠁⡴⠁⢀⣴⡏⠀⠈⠁⣸⡏⠀⣤⣿⠏⢸⡇⠀⣿⡇⣿⠀⣿⡇⢈⡱⠶⠿⠇⠈⣿⡇⣿⠀⢸⣿⠿⠿⠁⠀⡇⢸⣿⡇⠀⠀⠉⡁⠰⠶⠿⠀⢸⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⡠⠃⢀⣿⡏⠀⠀⠀⡠⠋⠀⢰⣿⠁⠀⢸⡇⠀⣿⡇⣿⠀⣿⡷⠋⢠⣿⢷⡆⠀⣿⡇⣿⠀⢸⣿⠀⠀⠀⠀⡇⢸⣿⡇⠀⣀⡜⠀⣾⡿⣿⠀⢸⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⢀⡾⠃⠀⣾⣟⣁⢶⣆⢰⠃⠀⠀⣾⣋⣰⡆⢸⡇⠀⣿⣇⠿⠀⣿⣧⡀⠸⣿⣾⠇⠀⢿⣧⣿⠀⢸⣿⠀⠀⠀⠀⡇⢸⣿⣇⠞⣿⡇⠀⣿⣷⠿⠀⢸⣿⣤⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⣀⠏⠀⢀⣀⣀⣀⣀⣾⣿⠁⣀⣆⣀⣀⣰⣾⠇⢸⡇⢀⣶⣀⣰⣾⠟⢹⣇⡀⣀⣀⣀⣶⣾⡿⣿⢀⣲⣿⠿⠀⠀⠀⢇⣲⣶⣠⣾⠿⢇⠀⣂⣶⣶⣶⣶⣾⠿⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠛⠛⠛⠛⠛⠛⠛⠛⠛⠙⠛⠛⠛⠛⠛⠛⠛⠀⢸⡇⢸⣿⡟⠛⠃⠀⠀⠙⠛⠛⠋⠙⠛⠛⠀⠛⠛⠛⠋⠀⠀⠀⠀⠈⠛⠛⠛⠉⠀⠈⠛⠛⠛⠉⠙⠛⠃⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⠏⢁⣾⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⠛⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",

              $"《x{startPosX},y{layout.Image.Y - 5}》" +

                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⢿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⣀⣀⣀⣀⡀⣀⣀⣀⣀⣀⣀⣀⡀⠀⣀⠀⣀⣀⣀⠀⠀⠀⢀⣀⣀⣀⠀⠀⠀⠀⣀⡀⢀⣀⣀⣀⣸⡇⢸⣿⣇⣀⣀⠀⠀⢀⣀⣀⡀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⡎⢉⣩⣉⣉⠉⠉⣭⣿⣷⠋⣉⣉⣉⡍⣽⣿⣷⡖⢹⣷⡏⠉⢹⣦⡄⣴⣎⣍⡉⢙⣶⣤⣠⣾⠋⣿⡾⠉⣩⣿⣿⡇⣼⣏⣭⣿⣿⣠⡖⡊⣭⢩⠳⣶⡄⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⡇⣼⣿⣿⣿⠷⣠⣿⠛⢹⣾⣿⣿⠟⠙⣿⣿⢿⡇⢾⣿⣿⣶⠸⣿⡟⣿⣾⣿⣿⡇⣿⣿⣿⣷⠸⣿⣷⣾⣿⣿⢿⡇⣿⣿⣿⠋⠀⣯⣾⣿⣿⣿⠸⣿⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠷⠿⠁⡴⢃⣴⣿⡏⠀⠈⠁⣸⡏⢰⣾⣿⠏⢸⡇⢸⣿⡇⣿⠀⣿⡇⢈⡵⠶⠿⠇⣽⣿⡇⣿⠀⣿⣿⠿⠿⠁⢸⡇⢼⣿⡇⠀⠀⠉⡁⠴⠶⣿⠀⣿⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⢀⡰⠃⣸⣿⡟⠀⠀⠀⣠⠟⠀⣿⣿⠁⠀⢸⡇⢸⣿⡇⣿⢘⣿⡷⠟⣹⣿⢿⡆⢹⣿⡇⣿⠀⣿⣿⠀⠀⠀⢸⡇⣹⣿⡇⠀⣀⡜⣱⣿⡿⣿⠈⣿⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⢀⣾⠃⢰⣿⣿⣁⣶⣆⢰⠃⢰⢶⣿⣏⣰⡆⢸⡇⠸⣿⣇⡿⢺⣿⣷⡔⣿⣿⣾⡇⣿⣿⣧⣿⠐⣿⣿⠀⠀⠀⢸⡇⣿⣿⣇⣞⣿⡇⢺⣿⣷⢿⢰⣿⣿⣤⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⣀⢟⣰⣶⣾⣿⣶⣾⣿⣿⣃⣶⣿⣶⣿⣿⣿⠇⢸⡇⢺⣿⣷⣾⣿⠟⢻⣿⣿⣿⣷⣶⣿⣿⡿⣿⣶⣿⣿⠿⠀⠀⠸⢗⣿⣷⣾⣿⠿⢷⣿⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠛⠛⠛⠛⠛⠛⠛⠛⠛⠙⠛⠛⠛⠛⠛⠛⠛⠀⢸⡇⣽⣿⡟⠛⠋⠀⠈⠙⠛⠛⠋⠙⠛⠛⠀⠛⠛⠛⠋⠀⠀⠀⠀⠈⠛⠛⠛⠉⠀⠈⠛⠛⠛⠉⠙⠛⠋⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣟⣹⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠛⠛⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",


              $"《x{startPosX},y{layout.Image.Y - 5}》" +

                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⣀⣀⣀⣀⡀⣀⣀⣀⣀⣀⣀⣀⡀⠀⣀⡀⣀⣀⣀⠀⠀⠀⢀⣀⣀⣀⠀⠀⠀⠀⣀⣀⢀⣀⣀⣀⣸⣇⣿⣿⣇⣀⣀⠀⠀⢀⣀⣀⡀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⡎⣭⣭⣭⣭⣭⣭⣽⣿⣷⢛⣭⣭⣭⣽⣿⣿⣷⡖⣽⣷⣏⢩⣽⣦⡄⣴⣮⣭⣭⣿⣶⣤⣠⣾⣫⣿⣾⢫⣿⣿⣿⡇⣿⣿⣿⣿⣿⣠⣖⣏⣭⣽⣷⣶⣄⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⡇⣿⣿⣿⣿⣿⣿⣿⠛⢹⣿⣿⣿⠿⣿⣿⣿⢿⡇⣿⣿⣿⣿⢹⣿⡟⣿⣿⣿⣿⡏⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⢿⡇⣿⣿⣿⠋⠀⣿⣾⣿⣿⣿⢿⣿⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠷⠿⠁⡴⢣⣾⣿⡏⠀⠈⠁⣸⡟⣼⣿⣿⠏⢸⡇⣿⣿⡇⣿⣼⣿⡇⢈⡵⢶⡿⠇⣿⣿⡇⣿⢼⣿⣿⠿⠿⠁⢸⡇⣿⣿⡇⠀⠀⠉⡁⡴⣶⣿⢸⣿⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⢀⣰⢣⣿⣿⡟⠀⠀⠀⣠⠟⢹⣿⣿⠃⠀⢸⡇⣿⣿⡇⣿⣻⣿⣷⣟⣿⣿⢿⡏⣿⣿⡇⣿⢸⣿⣿⠀⠀⠀⢸⡇⣿⣿⡇⠀⣀⡞⣿⣿⡿⣿⢹⣿⣿⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⢀⣾⢣⣾⣿⣿⣃⣶⣆⢰⢷⣾⣿⣿⣏⣰⡆⢸⡇⣿⣿⣇⣿⣿⣿⣿⣾⣿⣿⣾⣷⣿⣿⣧⣿⣺⣿⣿⠀⠀⠀⢸⡇⣿⣿⣇⣾⣿⡗⣿⣿⣷⢿⣾⣿⣿⣦⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⣀⣟⣶⣿⣿⣿⣿⣿⣿⣿⣳⣿⣿⣿⣿⣿⣿⠇⢸⡇⣿⣿⣿⣿⣿⠟⢻⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⣿⠿⠀⠀⠸⢷⣿⣿⣿⣿⠿⢿⣿⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠛⠛⠛⠛⠛⠛⠛⠛⠛⠙⠛⠛⠛⠛⠛⠛⠛⠀⢸⡇⣿⣿⡟⠛⠋⠀⠈⠙⠛⠛⠋⠙⠛⠛⠁⠛⠛⠛⠋⠀⠀⠀⠀⠈⠛⠛⠛⠉⠀⠈⠛⠛⠛⠉⠙⠛⠋⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣟⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠛⠛⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀"


            };
            string input = pictures[currentIndex].Replace("\r\n", $"\n《x{startPosX}》");


            DrawString(input);
        }
    }
}
    

