using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

            DrawString($"《x{Console.WindowWidth / 2 - 10},y{Console.WindowHeight / 3 + 2},tGray》직업을 선택해주세요.");
            
            // 직업 선택
            int totalWidth = 70;
            int padding = (Console.WindowWidth - totalWidth) / 2; //양쪽 여백

            for (int i = 0; i < classTypes.Length; i++)
            {
                int stringLenght = classTypes[i].ToString().Length + 2;

                int posX = (totalWidth / (classTypes.Length + 1)) * (i + 1) - (stringLenght / 2) + padding;
                int posY = Console.WindowHeight / 3 + 4;

                DrawString($"《x{posX},y{posY}{spotLight[i]}》{i + 1}.{classTypes[i]}");
            }
        }
    }
}
