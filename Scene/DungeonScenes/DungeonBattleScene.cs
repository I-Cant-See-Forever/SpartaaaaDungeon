using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    class DungeonBattleScene : DungeonScene
    {
        public DungeonBattleScene(SceneController controller) : base(controller)
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
            foreach (var unit in dungeonController.dungeonMonsters)
            {
                Console.WriteLine($"Lv.{unit.Level} {unit.Name} {unit.Health} 공격력 {unit.Attack}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{dungeonPlayer.Level} {dungeonPlayer.Name} ({dungeonPlayer.ClassType})");
            Console.WriteLine($"HP {dungeonPlayer.MaxHealth} / {dungeonPlayer.CurrentHealth}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 공격");
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
                    controller.ChangeScene<DungeonAttackScene>();
                    break;

            }

            return this;
        }

    }
}
