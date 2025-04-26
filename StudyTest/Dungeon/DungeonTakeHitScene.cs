using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprtaaaaDungeon.StudyTest.Dungeon
{
    class DungeonTakeHitScene : DungeonScene
    {
        public DungeonTakeHitScene(SceneController controller) : base(controller)
        {
        }

        private DungeonData monstser;
        public override void Start()
        {
            //dungeonController.IsGameOver();
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
            /*Console.Clear();
            for (int i = 0; i < dungeonController.dungeonMonsters.Count; i++)
            {
                var unit = dungeonController.dungeonMonsters[i];
                if (unit.CurrentHealth <= 0) continue;

                Console.WriteLine("Battle!");
                Console.WriteLine();
                Console.WriteLine($"Lv.{unit.Level} {unit.Name} 의 공격! ");
                Console.WriteLine();
                Console.WriteLine($"{dungeonPlayer.Name}을(를) 맞췄습니다.");
                Console.WriteLine();
                //Console.WriteLine($"데미지 : {unit.Attack} ");
                Console.WriteLine();
                Console.WriteLine($"Lv.{dungeonPlayer.Level} {dungeonPlayer.Name}");

                float beforeHealth = dungeonPlayer.CurrentHealth;
                dungeonController.TakeDamage(i);
                float afterHealth = dungeonPlayer.CurrentHealth;

                Console.WriteLine($"HP {beforeHealth} -> {afterHealth}");

                Console.WriteLine("");
                Console.WriteLine("0. 다음\n");
                Console.Write(">>");
                string input = Console.ReadLine(); //키입력방식 바뀔수도있어서 간단하게 아무입력이나받음 넘어가게
                Console.Clear();
            }

            Console.WriteLine();
            Console.WriteLine($"Lv.{dungeonPlayer.Level} {dungeonPlayer.Name}");
            Console.WriteLine($"HP {dungeonPlayer.CurrentHealth}");
            Console.WriteLine();
            if (dungeonPlayer.CurrentHealth > 0) Console.WriteLine("0. 전투재개 ");
            else if (dungeonPlayer.CurrentHealth == 0 || dungeonController.dungeonMonsters.Count == 0) Console.WriteLine("0. 전투종료 ");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");*/

        }
        public override DungeonScene HandleInput(int input)
        {
            switch (input)
            {
                case 0:
                    controller.ChangeScene<DungeonBattleScene>();
                    break;
            }
            return this;
        }

    }
}
