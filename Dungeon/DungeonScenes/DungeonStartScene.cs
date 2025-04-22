using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonStartScene : DungeonScene
    {
        protected DungeonController dungeonController;
        private DungeonPlayer dungeonPlayer;
        private PlayerData player;
        private SceneController sceneController;
        public DungeonStartScene(SceneController controller, DungeonController controller2, DungeonPlayer player): base(controller) 
        {
            sceneController = controller;
            dungeonController = controller2;
            dungeonPlayer = player;
        }

        public override void Start()
        {           
            
        }
        public override void Update()
        {
            this.show();
            int input = int.Parse(Console.ReadLine());
            this.HandleInput(input);
        }
        public override void End()
        {
            
        }
        public override void show()
        {
            Console.Clear();
            Console.WriteLine("던전입구");
            Console.WriteLine("던전을 고르시오");
            Console.WriteLine("1. 뉴비던전");
            Console.WriteLine("2. 중수던전");
            Console.WriteLine("3. 고수던전");
            Console.WriteLine("0. 뒤로가기");

        }
        public override DungeonScene HandleInput(int input)
        {
            switch (input)
            {
                case 1:
                    dungeonController.SetDungeon("1");                   
                    sceneController.ChangeScene<DungeonBattleScene>();
                    break;

                case 2:
                    dungeonController.SetDungeon("2");
                    sceneController.ChangeScene<DungeonBattleScene>();
                    break;
                case 3:
                    dungeonController.SetDungeon("3");
                    sceneController.ChangeScene<DungeonBattleScene>();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
            return this;
        }

    }
}
