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

        string[] images = new string[]
        {
            TextContainer.warrior,
            TextContainer.archer,
            TextContainer.mage,
            TextContainer.assasin
        };

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
                        GameManager.Instance.IsCreatedPlayer = true;
                        controller.ChangeScene<TownScene>();
                        break;
                }
            }
        }

        void Draw(int currentIndex)
        {
            DrawString($"《x{Console.WindowWidth / 2 - 10},y6,tGray》직업을 선택해주세요.");

            // 직업 선택
            
            int padding = 5;
            int totalWidth = Console.WindowWidth - padding * 2;
            int slotWidth = totalWidth / classTypes.Length;

            for (int i = 0; i < classTypes.Length; i++)
            {
                int imageXLength = 23;

                int posX = i * slotWidth + padding + slotWidth / 2 - imageXLength / 2;
                int posY = Console.WindowHeight / 3;

                DrawProfileImagesTest(posX, posY, i, currentIndex);
            }
        }

        void DrawProfileImagesTest(int x, int y, int num, int selectIndex)
        {
            if(selectIndex == num)
            {
                DrawDirectImage(images[num], x, y, ConsoleColor.Magenta);
            }
            else
            {
                DrawDirectImage(images[num], x, y, ConsoleColor.Gray);
            }
        }
    }
}