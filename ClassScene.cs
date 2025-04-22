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

            DrawString("《x5》《y5》《tGray》직업을 선택해주세요.");
            DrawString("《x5》《y6》1.Warrior");
            DrawString("《x5》《y7》2.Mage");
            DrawString("《x5》《y8》3.Archer");
            DrawString("《x5》《y9》숫자 입력: ");
        }

        public override void Update()
        {
            //int input = int.Parse(Console.ReadLine());

            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo Keyinput = Console.ReadKey(true);

                switch(Keyinput.Key)
                {
                    case ConsoleKey.LeftArrow: 
                        if(currentIndex > 0 )
                        {
                            currentIndex--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentIndex < classTypes.Length -1)
                        {
                            currentIndex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        GameManager.Instance.PlayerData.ClassType = classTypes[currentIndex];
                        //controller.ChangeScene<StatScene>();
                        break;
                  

                }

                Draw(currentIndex);


            }


/*
            switch ((GameEnum.ClassType)input)
            {
                case GameEnum.ClassType.Warrior:
                    GameManager.Instance.PlayerData.ClassType = GameEnum.ClassType.Warrior;
                    break;
                case GameEnum.ClassType.Mage:
                    GameManager.Instance.PlayerData.ClassType = GameEnum.ClassType.Mage;
                    break;
                case GameEnum.ClassType.Archer:
                    GameManager.Instance.PlayerData.ClassType = GameEnum.ClassType.Archer;
                    break;
            }*/
        }

        void Draw(int currentIndex)
        {
            Console.Clear();

            string[] spotLight = new string[classTypes.Length];


            spotLight[currentIndex] = ",tmagenta";

            DrawString("《x5,y5,tGray》직업을 선택해주세요.");
            DrawString($"《x5,y6{spotLight[0]}》1.Warrior");
            DrawString($"《x5,y7{spotLight[1]}》2.Mage");
            DrawString($"《x5,y8{spotLight[2]}》3.Archer");
            DrawString("《x5,y9》숫자 입력: ");
        }
    }
}
