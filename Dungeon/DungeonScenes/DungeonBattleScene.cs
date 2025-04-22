using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    class DungeonBattleScene : DungeonScene
    {
        protected DungeonController dungeonController;
        private DungeonPlayer dungeonPlayer;
        public DungeonBattleScene(SceneController controller, DungeonController controller2, DungeonPlayer player) : base(controller)
        {
            this.dungeonController = controller2;
            this.dungeonPlayer = player;
        }

        private DungeonData monstser;
        public override void Start()
        {
                     

            foreach (var unit in dungeonController.dungeonMonsters)
            {
                Console.WriteLine($"{unit.Level} {unit.Name} 이(가) 나타났다. {unit.Attack} {unit.Health} ");
            }


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
            Console.WriteLine("Battle!");
            Console.WriteLine($"");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 방어");
            Console.WriteLine("3. 도망");
            Console.WriteLine("0. 뒤로가기");
        }
        public override DungeonScene HandleInput(int input)
        {
            return this;
        }
    
    }
}
