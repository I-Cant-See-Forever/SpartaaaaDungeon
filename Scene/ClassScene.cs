using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class ClassScene : Scene
    {
        int currentIndex = 0;
        GameEnum.ClassType[] classTypes;


        public ClassScene(SceneController controller) : base(controller) { }

        public override void End()
        {

        }

        public override void Start()
        {
            classTypes = new GameEnum.ClassType[]
            {
                GameEnum.ClassType.Warrior,
                GameEnum.ClassType.Archer,
                GameEnum.ClassType.Mage,
                GameEnum.ClassType.Assassin
            };
            Draw(currentIndex);
            Frame();

        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo Keyinput = Console.ReadKey(true);

                switch (Keyinput.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                            Draw(currentIndex);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentIndex < classTypes.Length - 1)
                        {
                            currentIndex++;
                            Draw(currentIndex);
                        }
                        break;
                    case ConsoleKey.Enter:
                        GameManager.Instance.PlayerData.ClassType = classTypes[currentIndex];
                        controller.ChangeScene<TownScene>();
                        break;
                }
            }
        }

        void Draw(int currentIndex)
        {
            string[] selectSign = new string[classTypes.Length];
            string[] spotLight = new string[classTypes.Length];

            spotLight[currentIndex] = ",tmagenta";

            DrawString($"《x{Console.WindowWidth / 2 - 10},y6,tGray》직업을 선택해주세요.");

            // 직업 선택
            
            int padding = 5;
            int totalWidth = Console.WindowWidth - padding * 2;
            int slotWidth = totalWidth / classTypes.Length;

            for (int i = 0; i < classTypes.Length; i++)
            {
                int imageXLength = 23;
                int stringLenght = classTypes[i].ToString().Length;

                int posX = i * slotWidth + padding + slotWidth / 2 - imageXLength / 2;
                int posY = Console.WindowHeight / 3;

                //int posStringX = posX + imageXLength / 2 - stringLenght / 2;
                //DrawString($"《x{posStringX},y{posY+15}{spotLight[i]}》{classTypes[i]}");
                DrawProfileImagesTest(posX, posY, i, spotLight[i]);
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

        void DrawProfileImagesTest(int x, int y, int num, string spotlight)
        {
            string[] images = new string[]
            {
                $"《x{x},y{y},{spotlight}》" +
                $"⠀⢀⣠⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣄⡀⠀\r\n" +
                $"⠀⣾⠃⠀⠀⠀⡤⠀⠀⠀⠀⣤⣤⠀⠀⠀⠀⣠⠀⠀⠀⠙⣧⠀\r\n" +
                $"⠀⣿⠀⠀⠀⠠⣿⣦⡀⡶⠛⢹⣟⠙⢧⣀⣔⣿⡂⠀⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠀⠀⠙⠿⣸⣧⣄⣸⣯⣠⣬⣧⠻⠋⠀⠀⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠀ ⠀⠀⢺⣭⡉⢹⣟⢉⣭⡟⠀⠀⠀  ⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠀ ⠀⠀⢹⣿⡇⠈⠁⢸⣿⡏⠀⠀ ⠀⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⣠⡶⠺⠚⠋⠉⠻⠶⠶⠛⠉⢙⣛⢖⠦⠄⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⢰⡏⠀⠀⡀⠀ ⠀⠀⢀⣰⡾⢛⣙⣙⣛⠿⣦⡄⣿⠀\r\n" +
                $"⠀⣿⠀⣼⠃⠀⠈⣧⣀⠀⠀⠀⣾⢏⣴⣿⣿⣿⣿⣿⣎⢿⣿⠀\r\n" +
                $"⠀⣿ ⡏⠀⠀⢠⣿⡝⠙⠋⢸⡟⢼⣿⣿⠋⠈⢹⣿⣿⠜⣿⠀\r\n" +
                $"⠀⣿⠀⣿⣶⣶⣟⢀⣡⣴⣶⡌⣿⡸⣿⣿⣷⣴⣿⣟⡿⢸⣿⠀\r\n" +
                $"⠀⣿ ⣿ ⠀⣿⡎⣭⣶⢶⠷⠙⢷⣜⡻⠽⠿⠷⣛⣵⠟⣿⠀\r\n" +
                $"⠀⢻⣄⠛⠳⠓⠛⠷⠁⠀⠀⠀⠀⠀⠙⠛⠿⠾⠛⠋⠁⣠⡟⠀\r\n" +
                $"⠀⠀⠙⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠉⠀⠀\r\n",

                $"《x{x},y{y},{spotlight}》" +
                $"⠀⢀⣠⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣀⡀⠀\r\n" +
                $"⠀⣾ ⠀⠀⠀⣠⣴⣶⣦⣤⡀⠀⠀⠀⣦⠀⠀⠀⠀⠀⠹⣧⠀\r\n" +
                $"⠀⣿⠀⠀⢀⣾⡿⣯⣿⣟⠿⢿⣆⠀⢀⣿⢷⣄⠀⠀⠀ ⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠀⣼⣿⣿⠟⣴⣿⣶⡹⡧⣼⠃⠀⠙⢷⡄⠀ ⣿⠀\r\n" +
                $"⠀⣿⠀⠀⢸⣿⣿⠏⡞⠉⠿⢉⡷⣹⠃⠀⠀⠀⠈⣿⡀ ⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠸⣿⣿⡑⣷⣄⣀⡼⣱⠃⠀⠀⠀ ⠀⣻⡇ ⣿⠀\r\n" +
                $"⠀⣿⠀⣴⣿⣿⣻⣿⣮⠟⠛⣷⠯⠿⠷⠦⠦⢦⣬⢿⡮⣷⣿⠀\r\n" +
                $"⠀⣿⠀⢿⣿⣿⣻⡯⠗⣀⣀⡾⣿⣿⣿⢿⣿⣿⣁⣨⡇ ⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠉⢶⣶⣾⣿⣿⣿⣷⡹⡇⠛⠛⠉⠉⠉⣿⡇ ⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠀⠹⣿⣟⣯⣿⣯⣿⣷⡁⠀⠀⠀  ⣿⠀ ⣿⠀\r\n" +
                $"⠀⣿⠀⠀⢀⣾⣶⣬⣬⣭⣬⣴⣦⢳⡄⠀⠀ ⣾⠋⠀ ⣿⠀\r\n" +
                $"⠀⣿⠀⠀⣼⣿⡿⣿⣿⣿⣿⣿⣿⣇⢿⣄⡶⠛⠁⠀⠀ ⣿⠀\r\n" +
                $"⠀⢻⣄⢰⣿⣿⣻⣿⣯⣿⣯⣷⣿⣿⠸⡏⠀⠀⠀⠀⢀⣸⡟⠀\r\n" +
                $"⠀⠀⠉⠛⠙⠙⠙⠙⠙⠙⠙⠙⠙⠙⠓⠚⠛⠛⠛⠛⠋⠁⠀⠀\r\n",

                $"《x{x},y{y},{spotlight}》" +
                $"⠀⢀⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣄⡀⠀\r\n" +
                $"⠀⣾ ⢀⣤⣄⡀⠀⠀⠀⠀⢀⣤⣶⣤⣄⠀⠀⠀⠀⠀⠘⣧⠀\r\n" +
                $"⠀⣿⠀⣿⠋⠙⣿⠀⠀ ⢠⣿⣷⣿⡿⣿⠿⠄⠀⡠⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠻⣷⣾⠟⠀⢀⢠⢿⣛⣛⣚⡻⢿⣆⠠⢼⡧⠄⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠈⢻⡟⠡⣴⣾⣿⡿⡿⡿⣿⢿⣿⣿⣷⣬⠁⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⢸⣗⠀⠈⠙⢺⣇⠀⠀⠀⠀⣿⡞⠉⠈⠀⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⢸⣗⠀⠀⢄⡜⣿⣆⡀⣡⣼⣿⢣⣷⡆⠀ ⠀⣿⠀\r\n" +
                $"⠀⣿⠀⣰⠾⢷⡄⠀⣜⢷⣝⢿⣿⣿⣯⣿⢟⣫⣶⡄ ⠀⣿⠀\r\n" +
                $"⠀⣿⠀⢽⣀⣸⡟⣿⣿⣷⣍⡻⠿⢓⣫⣵⣿⣟⣿⣿⡀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠈⢹⣿⢨⣿⡿⣿⢿⣿⢿⣿⢿⣿⣻⣽⣿⣟⣇⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⢺⣿⢰⣿⢗⣿⣿⣿⢿⣿⢿⣿⢿⡸⣿⡿⣿⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⢺⡟⠟⠋⢸⣿⣿⢿⣿⢿⣿⢿⣿⡪⣿⣿⣿⡆⣿⠀\r\n" +
                $"⠀⢿⣀⠀⢺⣏⠀⠀⢽⣿⣿⢿⣿⢿⣿⡿⣿⡆⣿⣿⣷⣧⡟⠀\r\n" +
                $"⠀⠀⠙⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠙⠉⠀⠀\r\n",

                $"《x{x},y{y},{spotlight}》" +
                $"⠀⢀⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣤⣄⡀⠀\r\n" +
                $"⠀⣾⠁⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⡀⠀⠀⠀⠀⠀⠀⠀⠙⣧⠀\r\n" +
                $"⠀⣿⠀⠀⠀⠀⠀⠀⢀⣾⣿⣻⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⠀⠀⠀⢠⣿⣿⠿⢛⠻⢾⣿⣷⠀⠀⠀⠀ ⠀⣿⠀\r\n" +
                $"⠀⣿⠀ ⠀⠀⠀⣺⡿⢩⣾⣿⣿⣶⡙⣿⡇⠀⠀  ⠀⣿⠀\r\n" +
                $"⠀⣿⠀⣿⡄⠀⠠⣿⢱⣿⣬⣽⣯⣴⣿⡸⣯⠀⠀⣰⣿⠀⣿⠀\r\n" +
                $"⠀⣿⠀⢿⣿⡄⢀⣿⣜⢿⣿⣯⣷⣿⠟⣰⣏⡀⣰⣿⡏⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠈⢿⣿⣌⢿⣿⣶⣕⡻⢋⣵⣾⣿⡟⣱⣿⡟⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠀⣎⢿⡿⢎⣋⢻⣿⣿⣿⣿⣛⣙⠴⣿⡟⣤⠀⠀⣿⠀\r\n" +
                $"⠀⣿⠀⣼⡟⣰⣼⣿⣯⡜⣿⣯⣿⣿⢢⣿⣿⣶⡌⣿⣆⠀⣿⠀\r\n" +
                $"⠀⣿⢰⣿⣿⡆⣿⣿⣻⣧⡹⣿⡿⢆⣿⣿⣯⡷⣰⣿⣿⠀⣿⠀\r\n" +
                $"⠀⣿⠀⠻⣿⣿⣦⣝⣙⣫⣼⣿⣿⣧⣛⣍⣭⡶⣿⡿⠏⠀⣿⠀\r\n" +
                $"⠀⢿ ⠀⠈⠀⣾⣿⣿⣿⣿⣻⣾⣿⢿⣿⣟⡧⠁⠁⠀⣠⡟⠀\r\n" +
                $"⠀⠈⠙⠛⠛⠛⠛⠛⠙⠓⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠉⠀⠀\r\n"

            };
            string replaceImage = images[num].Replace("\r\n", $"\n《x{x},{spotlight}》");
            DrawString(replaceImage);
        }
    }
}