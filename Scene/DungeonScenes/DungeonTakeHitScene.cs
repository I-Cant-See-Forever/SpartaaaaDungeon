using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprtaaaaDungeon
{
    class DungeonTakeHitScene : DungeonScene
    {
        public DungeonTakeHitScene(SceneController controller) : base(controller)
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
            for (int i = 0; i < dungeonController.dungeonMonsters.Count; i++)
            {
                var unit = dungeonController.dungeonMonsters[i];
                if (unit.Health <= 0) continue;

                Console.WriteLine("Battle!");
                Console.WriteLine($"Lv.{unit.Level} {unit.Name} 의 공격! ");
                Console.WriteLine($"{dungeonPlayer.Name}을(를) 맞췄습니다.");
                Console.WriteLine($"데미지 : {unit.Attack} ");
                Console.WriteLine($"Lv.{dungeonPlayer.Level} {dungeonPlayer.Name}");

                float beforeHealth = dungeonPlayer.CurrentHealth;
                dungeonController.TakeDamage(i);
                float afterHealth = dungeonPlayer.CurrentHealth;
                Console.WriteLine($"HP {beforeHealth} -> {afterHealth}");

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
            if (dungeonPlayer.CurrentHealth > 0) Console.WriteLine("0. 전투재개 ");
            else if (dungeonPlayer.CurrentHealth == 0) Console.WriteLine("0. 전투종료 ");
            else if (dungeonController.dungeonMonsters.Count == 0)
                { Console.WriteLine("0. 전투종료 "); }
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

        }
        public override DungeonScene HandleInput(int input)
        {
            switch (input)
            {
                case 0:
                    if(dungeonPlayer.CurrentHealth > 0)
                    controller.ChangeScene<DungeonBattleScene>();
                    else if (dungeonPlayer.CurrentHealth == 0)
                        controller.ChangeScene<DungeonLoseResultScene>();
                    else if (dungeonController.dungeonMonsters.Count == 0)
                        controller.ChangeScene<DungeonWinResultScene>();

                        break;
            }
            return this;
        }

    }
}
