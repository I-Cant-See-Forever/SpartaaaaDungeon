using System;
using System.Collections.Generic;
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
                GameEnum.ClassType.Mage,
                GameEnum.ClassType.Archor
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
            DrawString($"《x2,{Console.WindowWidth / 2 - 20},y{Console.WindowHeight / 3 + 4},l{Console.WindowWidth - 30}》 ");

            string[] spotLight = new string[classTypes.Length];
            string[] selectSign = new string[classTypes.Length];

            spotLight[currentIndex] = ",tmagenta";
            selectSign[currentIndex] = "▶ ";

            DrawString($"《x{Console.WindowWidth / 2 - 10},y{Console.WindowHeight / 3 + 2},tGray》직업을 선택해주세요.");
            DrawString($"《x{Console.WindowWidth / 2 - 20},y{Console.WindowHeight / 3 + 4}{spotLight[0]}》{selectSign[0]}1.Warrior");
            DrawString($"《x{Console.WindowWidth / 2 - 3},y{Console.WindowHeight / 3 + 4}{spotLight[1]}》{selectSign[1]}2.Mage");
            DrawString($"《x{Console.WindowWidth / 2 + 11},y{Console.WindowHeight / 3 + 4}{spotLight[2]}》{selectSign[2]}3.Archer");
        }
    }
}
