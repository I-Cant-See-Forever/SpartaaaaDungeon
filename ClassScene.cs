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

        public ClassScene(SceneController controller) : base(controller){}

        public override void End()
        {
            
        }

        public override void Start()
        {
            classTypes = new GameEnum.ClassType[]
            {
                GameEnum.ClassType.Warrior,
                GameEnum.ClassType.Mage,
                GameEnum.ClassType.Archer
            };
            Draw(currentIndex);
        }

        public override void Update()
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo Keyinput = Console.ReadKey(true);

                switch(Keyinput.Key)
                {
                    case ConsoleKey.LeftArrow: 
                        if(currentIndex > 0 )
                        {
                            currentIndex--;
                            Draw(currentIndex);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentIndex < classTypes.Length -1)
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
            Console.Clear();

            string[] spotLight = new string[classTypes.Length];

            spotLight[currentIndex] = ",tmagenta";

            DrawString("《x5,y5,tGray》직업을 선택해주세요.");
            DrawString($"《x0,y7{spotLight[0]}》1.Warrior");
            DrawString($"《x13,y7{spotLight[1]}》2.Mage");
            DrawString($"《x23,y7{spotLight[2]}》3.Archer");
        }
    }
}
