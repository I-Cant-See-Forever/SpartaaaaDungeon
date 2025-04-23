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
            DrawString($"《x{(Console.WindowWidth)/2 -10},y{(Console.WindowHeight) / 3 + 2},tGray》이름을 입력해주세요.");
            DrawString($"《x{(Console.WindowWidth)/2 -10},y{(Console.WindowHeight) / 3 + 4},tGray》이름: ______________ 《tDarkCyan》");
            
            Console.SetCursorPosition(56, 14);
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length > 14)
                {
                    input = input.Substring(0, 14);
                    Console.SetCursorPosition(55, 14);
                }
                GameManager.Instance.PlayerData.Name = input;
                DrawString($"《x{(Console.WindowWidth) / 2 - 18 + input.Length},y{(Console.WindowHeight) / 3 + 6},tRed》system : 《tDarkCyan》{GameManager.Instance.PlayerData.Name}《tRed》님 반갑습니다.");
                Thread.Sleep(1000);
                controller.ChangeScene<ClassScene>();
                
            }
            else
            {
                DrawString($"《x{(Console.WindowWidth) / 2 - 20},y{(Console.WindowHeight) / 3 + 6},tRed》system : 잘못된 입력입니다. 다시 입력하세요.");
                Thread.Sleep(500);
                DrawString($"《x2,{(Console.WindowHeight) / 3 + 6},l{Console.WindowWidth - 30}》 ");
            }
        }
    }
}
