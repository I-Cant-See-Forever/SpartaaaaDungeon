using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class TownScene : Scene
    {
        TitleLayout layout = new();

        (string, Rectangle)[] menuTextRect;

        int currentSelectNum = 0;

        public TownScene(SceneController controller) : base(controller) 
        {
            menuTextRect = new (string, Rectangle)[]
            {
                new("상점", new()),
                new("휴식", new()),
                new("퀘스트", new()),
                new("던전", new()),
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
            string title = $"《x{layout.Title.X},y{layout.Title.Y},tyellow》" +
                $"████████╗ ██████╗ ██╗    ██╗███╗   ██╗\r\n" +
                $"╚══██╔══╝██╔═══██╗██║    ██║████╗  ██║\r\n" +
                $"   ██║   ██║   ██║██║ █╗ ██║██╔██╗ ██║\r\n" +
                $"   ██║   ██║   ██║██║███╗██║██║╚██╗██║\r\n" +
                $"   ██║   ╚██████╔╝╚███╔███╔╝██║ ╚████║\r\n" +
                $"   ╚═╝    ╚═════╝  ╚══╝╚══╝ ╚═╝  ╚═══╝";
            string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Title.X},tyellow》");

            DrawString(replaceTitle);

            DrawMenuText(currentSelectNum);

            DrawStore();
        }

        public override void Update()
        {

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                bool isCorretInput = false;
                int tempSelectNum = currentSelectNum;

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentSelectNum > 0)
                        {
                            currentSelectNum--;

                            isCorretInput = true;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentSelectNum < menuTextRect.Length - 1)
                        {
                            currentSelectNum++;

                            isCorretInput = true;
                        }
                        break;
                    case ConsoleKey.Enter:
                        //ChangeScene;
                        break;
                    case ConsoleKey.Escape:
                        controller.ChangeScene<StatScene>();
                        break;
                }
               
                if(isCorretInput)
                {
                    DrawRemoveRect(menuTextRect[tempSelectNum].Item2);

                    DrawMenuText(currentSelectNum);
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

        void DrawStore()
        {
            int startPosX = layout.Image.X + 10;

            string pictureString = $"《x{startPosX},y{layout.Image.Y - 1}》" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢧⣄⡀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠘⠹⠽⠯⣟⣽⣳⣖⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣞⣗⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠺⠕⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡤⣴⢴⣲⣖⢶⣲⣖⡶⣳⣻⡽⠚⠀⠀⠀《tyellow》⠀⠀⢠⡴⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⡿⣝⠓⠉⠁⠉⠉⠈⠈⠉⠁⠀⠀⠀⠀⠀⠀⠀《tyellow》⢰⢯⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠀⣠⡀《》⠀⠀⠀⠀⠀⠻⢽⣲⣤⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⢸⢽⣞⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠈⠻⠈⠀《》⠀⠀⠀⠀⠀⠀⠀⠈⠈⠘⠳⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠹⠾⣽⣳⢖⠶⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣲⢶⢶⡳⠒⢳⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠀⠁⠈⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠀⣠⡀⠀《》⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣻⡽⡝⠙⢹⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⢞⡧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tyellow》⠀⠘⠀《》⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣲⣴⣲⢼⣳⢯⡇⠀⣼⣖⡦⣖⣖⣖⣖⡶⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣖⣯⢾⣺⣞⡯⣯⢟⡶⡯⣷⣳⣟⡽⣞⡗⠃⣠⡈⠺⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡴⣯⢷⢯⣟⣾⣺⢽⡽⣽⢽⢯⣗⣷⣳⡟⠍⣐⡾⡉⣙⣦⡈⠻⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tdarkgreen》⡴⡯⡄《》⠀⠀⠀⠀⠀⠀⠀⠀⠀⡤⣗⡿⣽⢽⡽⣞⣾⣺⢯⢯⡯⣟⡽⣞⡾⠚⣀⡞⠊⠋⠋⠓⠑⠻⣤⠉⢳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀《tdarkgreen》⢀⢼⣫⢿⢽⡄《》⠀⠀⠀⠀⠀⠀⡠⣞⣯⢯⣟⡾⣽⢽⣳⣗⡯⣟⣽⢽⡽⣽⠝⢀⡴⠷⠳⠶⢶⢶⣲⠶⠲⠾⠽⣄⡘⠧⡄⠀⠀⠀⠀⠀《tdarkgreen》⣰⣳⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀《tdarkgreen》⢀⡮⣟⣽⢽⢯⣻⡤《》⠀⠀⠀⠀⠲⠻⠽⠺⠽⠾⠽⠽⠽⠞⠾⠽⠽⠞⠯⠟⢀⣴⣟⣤⢤⡤⣤⢼⢯⡯⡧⡤⣤⢤⣜⢾⡀⠙⠇⠀⠀⠀《tdarkgreen》⣰⣗⣯⢯⡀⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀⠀《tdarkgreen》⢡⣻⣺⡽⡬⣤⠁《》⠀⠀⠀⠀⠀⢰⣺⣖⢷⣲⣗⣶⣳⣺⣖⣾⣲⣳⣖⣗⣟⣆⣀⣀⣀⣀⣀⣁⣀⣀⣈⣀⣀⣀⡀⣱⠅⠀⠀⠀⠀《tdarkgreen》⢴⢗⡷⡯⣟⡽⡄⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀⠀《tdarkgreen》⢠⣷⣻⣺⢽⣽⣳⢧⠀《》⠀⠀⠀⠀⢰⣳⢯⡯⣗⠗⠗⣯⠞⠞⢾⣺⣳⣳⣻⣺⡝⠘⠘⠘⠘⢺⣺⣞⡾⣺⡎⠊⠊⠉⢹⡂⠀⠀⠀⠀《tdarkgreen》⢀⡾⣽⣳⢖⣆⠀⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀⠀《tdarkgreen》⣰⢟⣾⣺⢽⣽⣺⣞⡯⣷⡀《》⠀⠀⠀⠰⡯⣯⢯⣟⣀⣀⣞⣅⣀⣰⢯⣞⣗⣟⣞⡗⠯⠺⠺⠳⢽⢞⡾⣽⣳⠗⠗⠻⠚⢷⠅⠀⠀《tdarkgreen》⠀⢠⢯⡯⣗⣯⢿⢽⣕⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀《tdarkgreen》⠀⠉⢩⣺⣞⣟⡾⣈⣌⣉⠁⠁《》⠀⠀⠀⠸⣽⢽⣽⣺⠉⠁⣻⡊⠉⢑⣟⣞⣗⣟⡾⡵⣤⢤⣤⣤⢼⢯⡯⣗⣯⢧⣤⣤⣤⢼⡂⠀⠀⠀《tdarkgreen》⠏⢟⡾⣽⢾⠹⠛⠚⠇⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀⠀《tdarkgreen》⠀⡰⣽⣳⣳⣗⡿⣽⣺⡽⣧《》⠀⠀⠀⠀⢨⢯⡷⣳⢯⣖⣖⣞⣖⣦⢾⣺⣳⣻⣺⢽⣍⡀⡀⡀⡀⣹⢽⡽⣽⢞⡇⡀⡀⡀⣸⡂⠀⠀⠀《tdarkgreen》⡰⣽⢽⣳⣟⣽⣻⣳⠀⠀⠀⠀⠀⠀⠀" +
                $"\r\n⠀⠀⠀⠀《tdarkgreen》⠀⣰⢯⣗⣟⣞⡾⣽⡳⣗⣯⢷⣳⡀《》⠀⠀⢨⢯⡯⣟⡽⣞⣗⣟⣞⣗⣯⢷⣻⣺⢽⡽⡞⠙⠉⠋⠋⢫⡯⣯⢯⡯⡏⠋⠙⠙⢚⠆⠀⠀《tdarkgreen》⣰⢯⡯⣟⣾⣺⣞⡾⣽⣳⡀⠀⠀⠀⠀⠀";
            string input = pictureString.Replace("\r\n", $"\n《x{startPosX}》");

            DrawString(input);
        }
    }
}
