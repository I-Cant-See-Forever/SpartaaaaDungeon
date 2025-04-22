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
            int leftLength = 20;
            int rightLength = Console.WindowWidth - 2 - leftLength; ;
            DrawString($"《x0,y0》┏━《l{leftLength + 1}》─《l{rightLength - 4}》─《》━┓");

            for (int y = 1; y < Console.WindowHeight - 1; y++)
            {
                DrawString($"《x0,y{y}》│《x{leftLength + 3}》《x{Console.WindowWidth - 1}》│");
            }

            DrawString($"《x0,y{Console.WindowHeight - 1}》┗━《l{leftLength + 1}》─《l{rightLength - 4}》─《》━┛");

        }

        public override void Update()
        {
            DrawString("《x5,y5,tGray》이름을 입력해주세요.");
            DrawString("《x5,y6,tGray》이름: 《tDarkCyan》");
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
