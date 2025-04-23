using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonWinResultScene : DungeonScene
    {
        protected DungeonController dungeonController;
        private DungeonPlayer dungeonPlayer;
        public DungeonWinResultScene(SceneController controller, DungeonController controller2, DungeonPlayer player) : base(controller)
        {
            this.dungeonController = controller2;
            this.dungeonPlayer = player;

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
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("Victory");
            Console.WriteLine("던전의 몬스터를 모두 잡았습니다.");

            Console.WriteLine("0. 다음");

        }
        public override DungeonScene HandleInput(int input)
        {
      
            return this;
        }

    }
}
