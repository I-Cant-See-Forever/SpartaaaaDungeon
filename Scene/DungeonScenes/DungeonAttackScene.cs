using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprtaaaaDungeon
{
    class DungeonAttackScene : DungeonScene
    {
        public DungeonAttackScene(SceneController controller) : base(controller)
        {
        }

        private DungeonData monstser;
        public override void Start()
        {





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
            Console.WriteLine("Battle!");
            Console.WriteLine();
            for (int i = 0; i < dungeonController.dungeonMonsters.Count; i++)
            {
                var m = dungeonController.dungeonMonsters[i];
                Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name}  HP:{m.Health} 공격력 {m.Attack}");
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
                    controller.ChangeScene<DungeonBattleScene>();
                    break;
                case 1:
                    controller.ChangeScene<DungeonAttackResultScene>();
                    break;
            }
            return this;
        }

    }
}
