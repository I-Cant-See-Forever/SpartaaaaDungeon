using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class NameScene : Scene
    {
        public NameScene(SceneController controller) : base(controller){}

        public override void End()
        {
            
        }

        public override void Start()
        {
            
            
        }

        public override void Update()
        {
            DrawString("《x5》《y5》《tGray》이름: 《tDarkCyan》");
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                GameManager.Instance.PlayerData.Name = input;
                DrawString($"《x5》《tRed》system : 《tDarkCyan》{GameManager.Instance.PlayerData.Name}《tRed》님 반갑습니다.");
                Thread.Sleep(1000);
                controller.ChangeScene<ClassScene>();
                
            }
            else
            {
                DrawString("《x5》《tRed》system : 잘못된 입력입니다. 다시 입력하세요.");
                Thread.Sleep(500);
                Console.Clear();
            }
            
        }
    }
}
