using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class ClassScene : Scene
    {
        public ClassScene(SceneController controller) : base(controller){}

        public override void End()
        {
            
        }

        public override void Start()
        {
            DrawString("《x5》《y5》《tGray》직업을 선택해주세요.");
            DrawString("《x5》《y6》1.Warrior");
            DrawString("《x5》《y7》2.Mage");
            DrawString("《x5》《y8》3.Archer");
            DrawString("《x5》《y9》숫자 입력: ");
        }

        public override void Update()
        {
            int input = int.Parse(Console.ReadLine());
            ConsoleKeyInfo Keyinput = Console.ReadKey(true);
            
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
            }
            controller.ChangeScene<StatScene>();
        }
    }
}
