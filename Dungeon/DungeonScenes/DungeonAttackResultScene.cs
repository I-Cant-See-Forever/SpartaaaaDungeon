using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprtaaaaDungeon
{
    class DungeonAttackResultScene : DungeonScene
    {
        protected DungeonController dungeonController;
        private DungeonPlayer dungeonPlayer;
        private SceneController sceneController;
        public DungeonAttackResultScene(SceneController controller, DungeonController controller2, DungeonPlayer player) : base(controller)
        {
            sceneController = controller;
            dungeonController = controller2;
            dungeonPlayer = player;
        }

        private DungeonData monstser;
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
            Console.WriteLine("Battle!");
            Console.WriteLine($"{dungeonPlayer.Name}의 공격!");
            Console.WriteLine($"을 맞췄습니다.");
            Console.WriteLine($"데미지 : ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"");
            Console.WriteLine($"HP -> ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

        }
        public override DungeonScene HandleInput(int input)
        {
            switch (input)
            {
                case 0:
                    sceneController.ChangeScene<DungeonTakeHitScene>();
                    break;
            }
            return this;
        }
    
    }
}
