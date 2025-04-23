using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SprtaaaaDungeon
{
    class DungeonAttackResultScene : DungeonScene
    {
        public DungeonAttackResultScene(SceneController controller) : base(controller)
        {
        }

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
            int i = dungeonController.LastAttackIndex;
            var m = dungeonController.dungeonMonsters[i];
            float dmg = dungeonController.LastDamage;
            float before = dungeonController.LastMonsterPrevHealth;
            float after = m.Health;
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine($"{dungeonPlayer.Name}의 공격!");
            Console.WriteLine($"Lv.{m.Level} {m.Name} 을(를) 맞췄습니다.  [데미지 : {dmg}]");   
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Lv.{m.Level} {m.Name}");
            Console.WriteLine($"HP {before} ->{after}");
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
                    controller.ChangeScene<DungeonTakeHitScene>();
                    break;
            }
            return this;
        }

    }
}
