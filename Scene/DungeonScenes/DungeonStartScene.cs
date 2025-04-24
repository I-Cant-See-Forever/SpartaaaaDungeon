using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonStartScene : DungeonScene
    {
        int currentSelectNum = 0;

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

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo inputInfo = Console.ReadKey(true);

                bool isCorretInput = false;
                int tempSelectNum = currentSelectNum;

                switch (inputInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentSelectNum = GetMoveSelectIndex(currentSelectNum, -1, menuTextRect.Length - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.DownArrow:
                        currentSelectNum = GetMoveSelectIndex(currentSelectNum, +1, menuTextRect.Length - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.Enter:
                        controller.ChangeScene(SelectScenes[currentSelectNum]);
                        break;
                    case ConsoleKey.Escape:
                        controller.ChangeScene<StatScene>();
                        break;
                }

                if (isCorretInput)
                {
                    DrawRemoveRect(menuTextRect[tempSelectNum].Item2);

                    DrawMenuText(currentSelectNum);
                }
            }
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
                        dungeonController.EnterDungeon("1");
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
                        dungeonController.EnterDungeon("2");
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
                        dungeonController.EnterDungeon("3");
                        controller.ChangeScene<DungeonBattleScene>();
                    }
                    else

                    {
                        Console.WriteLine("플레이어 탈진상태. 공략불가");
                        Thread.Sleep(1000);
                    }
                    break;
                case 0:
                    dungeonController.ExitDungeon();
                    controller.ChangeScene<TownScene>();
                    break;

            }



            return this;
        }

    }
}
