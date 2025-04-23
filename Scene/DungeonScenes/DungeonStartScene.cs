using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonStartScene : DungeonScene
    {

        public DungeonStartScene(SceneController controller) : base(controller)
        {
            
        }

        public override void Start()
        {
            dungeonController.IsPlayerAliveCheck(); // 체력 0 되면 false로 바꿈
        }
        public override void Update()
        {
            show();
            int input = int.Parse(Console.ReadLine());
            HandleInput(input);
        }
        public override void End()
        {

        }
        public override void show()
        {
            Console.Clear();           
            Console.WriteLine("[던전입구]");
            Console.WriteLine();
            Console.WriteLine("던전을 고르시오");
            Console.WriteLine();
            Console.WriteLine("1. 뉴비던전");
            Console.WriteLine("2. 중수던전");
            Console.WriteLine("3. 고수던전");
            Console.WriteLine();
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

        }
        public override DungeonScene HandleInput(int input)
        {


            switch (input)
            {
                case 1:
                    if (dungeonController.isPlayerAlive)
                    {
                        dungeonController.SetDungeon("1");
                        controller.ChangeScene<DungeonBattleScene>();
                    }
                    else

                    {
                        Console.WriteLine("플레이어 탈진상태. 공략불가");
                        Thread.Sleep(1000);
                    }


                    break;

                case 2:
                    if (dungeonController.isPlayerAlive)
                    {
                        dungeonController.SetDungeon("2");
                        controller.ChangeScene<DungeonBattleScene>();
                    }
                    else

                    {
                        Console.WriteLine("플레이어 탈진상태. 공략불가");
                        Thread.Sleep(1000);
                    }
                    break;
                case 3:
                    if (dungeonController.isPlayerAlive)
                    {
                        dungeonController.SetDungeon("3");
                        controller.ChangeScene<DungeonBattleScene>();
                    }
                    else

                    {
                        Console.WriteLine("플레이어 탈진상태. 공략불가");
                        Thread.Sleep(1000);
                    }
                    break;
                case 0:
                    controller.ChangeScene<TownScene>();
                    break;

            }



            return this;
        }

    }
}
