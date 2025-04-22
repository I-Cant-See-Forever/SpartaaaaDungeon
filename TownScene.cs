using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class TownScene : Scene
    {
        int leftLenght;
        int rightLenght;

        int menuStartHeight;
        int menuStartWidth;


        int currentSelectNum = 0;

        string[] menuArray = new string[]
        {
            "상점",
            "휴식",
            "퀘스트",
            "던전"
        };

        public TownScene(SceneController controller) : base(controller) 
        {
            leftLenght = 20;
            rightLenght = Console.WindowWidth - 2 - leftLenght;
            menuStartHeight = 4;
            menuStartWidth = 5;

        }

        public override void Start()
        {
            DrawString($"《x0,y0》┏━《l{leftLenght + 1}》─《》┬《l{rightLenght-4}》─《》━┓");

            for (int y = 1; y < Console.WindowHeight - 1; y++)
            {
                DrawString($"《x0,y{y}》│《x{leftLenght+3}》│《x{Console.WindowWidth - 1}》│");
            }

            DrawString($"《x0,y{Console.WindowHeight -1}》┗━《l{leftLenght + 1}》─《》┴《l{rightLenght-4}》─《》━┛");

          
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
                        if (currentSelectNum < menuArray.Length - 1)
                        {
                            currentSelectNum++;

                            isCorretInput = true;
                        }
                        break;
                }

                if(isCorretInput)
                {
                    RemoveMenuText(tempSelectNum);

                    DrawMenuText(currentSelectNum);

                    RemoveImage();

                    switch(currentSelectNum)
                    {
                        case 0: DrawStore(); break;
                        default: break;
                    }
                }
            }
        }

        public override void End()
        {
            currentSelectNum = 0;
        }
       
        
        void DrawMenuText(int index)
        {
            string[] backSpotlight = new string[menuArray.Length];
            string[] selectSign = new string[menuArray.Length];


            backSpotlight[index] = "tmagenta";
            selectSign[index] = "▶ ";

            for (int i = 0; i < menuArray.Length; i ++)
            {
                DrawString($"《x{menuStartWidth},y{menuStartHeight + i * 2},{backSpotlight[i]}》{selectSign[i]}{menuArray[i]}");
            }
        }

        void RemoveMenuText(int removeIndex) => DrawString($"《x{menuStartWidth},y{menuStartHeight + removeIndex * 2},l{leftLenght - menuStartWidth}》 ");

        void RemoveImage()
        {
            for (int i = 1; i < Console.WindowHeight-1; i++)
            {
                DrawString($"《x{leftLenght+4},y{i},l{Console.WindowWidth - leftLenght - 5}》 ");
            }
        }

        void DrawStore()
        {
            //《tyellow》《》
            string pictureString = $"《x44,y5》⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⠒⠒⠒⠒⠒⢄⠀⠀⠀⠀⣤⣤⣤⣤⣤⣤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠜⣁⢾⠽⠯⠟⣧⣈⠒⢄⠀⠀⠚⡴⡥⢶⢥⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⢊⣄⢶⠝⠹⠤⠭⠤⠋⠾⣣⢆⡑⢄⠘⡯⡽⢭⢽⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠚⢄⡷⠎⠃⡈⠄⢂⠐⠠⢁⠐⡈⠳⣧⡂⡙⢷⣹⢪⣽⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⠊⡔⣼⠟⡑⠲⡑⠒⠘⣂⢚⡐⢂⠒⢰⠓⢊⠻⣖⠤⠑⢞⣼⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⠜⣁⢾⡙⢣⠘⡘⢐⡇⢈⠖⣩⣶⣍⠦⡐⢨⠃⠣⢁⡞⢫⡷⣈⠹⢂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⢊⡔⣵⠟⠁⡈⠡⠉⢉⠉⢈⡇⠾⠿⠿⠿⠷⢩⠁⡉⢉⠉⠠⢁⠈⠻⣦⢆⡑⢄⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠀⢀⠜⣠⢞⣼⣁⣀⢂⡄⣡⢈⡄⣈⠄⡇⣿⣾⢂⣷⣿⢸⡀⡔⣠⢈⡔⣠⢈⡄⣌⣳⡮⡄⠣⣀⠀⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⡠⢊⢡⢶⢽⢁⠢⡄⡇⠠⠀⠄⠠⠀⠄⡈⡇⣿⣿⠡⣿⣿⢰⠀⡐⠀⠄⠠⠀⠄⢸⢀⠆⡌⡿⣦⣈⠑⢄⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⢀⠞⢠⣎⡏⠣⠘⠆⠣⠔⠃⠆⠱⠈⠆⡑⠢⠑⠚⠤⠄⠳⠤⠐⠎⠓⠄⠣⠘⠄⠣⠘⡘⠆⠚⠤⠃⠜⢣⡯⡄⠣⡀⠀⠀⠀\r\n⠀⠀⠀⠈⠧⠼⠗⠉⡇⠠⠁⢂⠁⢂⠡⠈⠄⡁⠂⠄⠡⢈⠐⠠⠈⠄⠠⠁⠌⡐⠈⠄⠡⠈⠄⠡⠐⡈⠐⠠⢁⠂⢸⡍⠻⠶⠼⠁⠀⠀\r\n⠀⠀⠀⠀⠀⠀⢠⠚⣉⠓⡍⢂⠉⢂⠁⢋⠐⡉⠌⡉⠌⡁⢊⠁⠋⠌⡑⠉⢂⠁⠋⠌⡑⠉⠌⠃⡑⢈⠉⡑⢨⢋⢃⠓⡄⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠸⣀⣃⣃⢇⡠⣈⢄⡈⣶⠒⠒⡒⠒⢒⠒⠒⣎⡐⣠⢀⡁⣖⢚⣒⣒⢒⣓⣚⢒⡆⣀⢂⡄⡹⣐⣊⣂⠇⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⢈⡇⠀⠄⠠⠀⠄⡀⣿⡁⢳⠘⠓⠪⣌⠁⡇⠠⢀⠂⡐⢸⠸⣿⢿⢸⡿⣟⢸⠀⠄⢂⠠⠐⡀⢸⡅⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⢀⠤⠧⠬⡤⠡⠬⠰⠄⣷⠡⢫⠰⣁⠓⡆⡁⡧⠔⠤⠢⠔⣬⠳⣶⣶⢰⣶⣶⠸⡤⠌⠤⠤⢡⠴⠜⠦⡀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⢸⠠⢓⠂⡇⠐⠠⠁⠄⣿⠁⢯⡐⢢⠙⣆⠁⡇⢀⠂⡐⠠⢰⣉⣍⣙⢌⣉⣃⢞⠀⡐⢀⠂⢼⠰⡉⠆⡇⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠈⠒⡖⠓⠑⠊⠒⠑⠂⣿⠉⢶⠈⢆⡙⠦⡅⡗⠂⠒⡐⠃⢚⠬⠿⠿⠸⠿⠿⣘⠒⠐⢂⠚⠈⠒⢲⠞⠁⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⢈⡇⠐⡈⠄⠡⠈⠄⣯⢃⢮⠑⡌⡘⡇⠄⡇⠠⢁⠐⡈⠓⢒⠒⠒⠖⠒⠒⠆⡃⠌⡀⢂⠡⢈⢸⡃⠀⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⢰⠉⡍⡉⡇⠉⠡⠉⠌⣿⠠⠺⠬⠴⠥⡓⠄⡏⠁⡉⢈⠁⡉⠌⠉⢉⠈⡉⢉⠈⡁⢉⠈⡁⢹⠉⠥⡉⡆⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠸⢤⣥⣡⣧⣌⣤⣥⣂⢯⡗⣧⠷⣞⠶⡵⢦⡧⣔⣤⣆⡴⣤⣌⡴⣄⣦⡔⣤⣆⡴⣄⣦⡔⣼⣬⣱⡤⠃⠀⠀⠀⠀⠀\r\n⠀⠀⠀⠀⠀⠀⠀⠠⣗⣞⣒⢶⣙⡞⡐⣂⡐⣂⢒⡐⢒⡐⣂⢒⡘⢲⣖⣓⢶⣊⡳⣞⡲⣞⡱⣞⡲⣝⣒⣺⡜⣶⣹⠆⠀⠀⠀⠀⠀⠀\r\n⠀⠀⢀⣄⣠⣀⣄⣰⡷⣦⣟⣦⣿⣰⡴⣤⣖⣤⣦⣲⣤⣖⣤⣦⣴⣢⣷⣭⡾⣵⢧⣷⣳⣼⣳⡽⣶⣮⢷⡶⣾⡵⣾⣅⣠⣀⣄⣀⠀⠀\r\n⠀⠀⡿⢶⠶⣖⡶⣞⡳⢶⢶⣲⠶⡶⡷⢶⠶⣖⢶⡞⡶⢶⢶⡲⢶⢶⣣⢶⠶⣶⠶⣞⠶⡶⢶⣲⢶⡳⢶⠶⣖⡶⡼⢶⠶⡶⣶⢾⡃⠀\r\n⠀⠀⠛⠋⠛⠚⠓⠋⠛⠛⠚⠓⠛⠓⠛⠋⠛⠚⠓⠛⠙⠛⠚⠛⠛⠚⠓⠋⠛⠓⠛⠚⠛⠙⠛⠓⠛⠚⠋⠛⠚⠓⠛⠋⠛⠓⠛⠚⠁⠀";

            string input = pictureString.Replace("\r\n", "\n《x44》");


            DrawString(input);
        }
    }
}
