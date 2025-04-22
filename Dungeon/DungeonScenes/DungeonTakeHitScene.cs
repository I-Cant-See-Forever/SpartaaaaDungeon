using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprtaaaaDungeon
{
    class DungeonTakeHitScene : DungeonScene
    {
        protected DungeonController dungeonController;
        private DungeonPlayer dungeonPlayer;
        private SceneController sceneController;
        public DungeonTakeHitScene(SceneController controller, DungeonController controller2, DungeonPlayer player) : base(controller)
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
            foreach (var unit in dungeonController.dungeonMonsters)
            {
               
                Console.WriteLine("Battle!");
                Console.WriteLine($"Lv.{unit.Level} {unit.Name} 의 공격! ");
                Console.WriteLine($"{dungeonPlayer.Name}을(를) 맞췄습니다.");
                Console.WriteLine($"데미지 : {unit.Attack} ");
                Console.WriteLine($"Lv.{dungeonPlayer.Level} {dungeonPlayer.Name}");
                Console.WriteLine($"HP {dungeonPlayer.CurrentHealth} -> {dungeonPlayer.CurrentHealth-unit.Attack}");
                dungeonController.TakeDamage();
                Console.WriteLine("");
                Console.WriteLine("0. 다음\n");
                Console.Write(">>");
                string input = Console.ReadLine();
                Console.Clear();

            }

            Console.WriteLine();
            Console.WriteLine($"Lv.{dungeonPlayer.Level} {dungeonPlayer.Name}");
            Console.WriteLine($"HP {dungeonPlayer.CurrentHealth}");
            Console.WriteLine();
            Console.WriteLine("0. 전투재개 ");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

        }
        public override DungeonScene HandleInput(int input)
        {
            switch (input)
            {
                case 0:
                    sceneController.ChangeScene<DungeonBattleScene>();
                    break;
            }
            return this;
        }
    
    }
}
