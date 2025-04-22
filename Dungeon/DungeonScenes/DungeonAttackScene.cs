using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprtaaaaDungeon
{
    class DungeonAttackScene : DungeonScene
    {
        protected DungeonController dungeonController;
        private DungeonPlayer dungeonPlayer;
        private SceneController sceneController;
        public DungeonAttackScene(SceneController controller, DungeonController controller2, DungeonPlayer player) : base(controller)
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
            Console.WriteLine();
            for (int i = 0; i < dungeonController.dungeonMonsters.Count; i++)
            {
                var m = dungeonController.dungeonMonsters[i];
                Console.WriteLine($"{i+1}. Lv.{m.Level} {m.Name}  HP:{m.Health}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{dungeonPlayer.Level} {dungeonPlayer.Name} ({dungeonPlayer.ClassType})");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("공격상대를 선택해주세요.");
            Console.Write(">>");

        }
        public override DungeonScene HandleInput(int input)
        {
            switch (input)
            {
                case 0:
                    sceneController.ChangeScene<DungeonBattleScene>();
                    break;
                case 1:
                    sceneController.ChangeScene<DungeonAttackResultScene>();
                    break;
            }
            return this;
        }
    
    }
}
