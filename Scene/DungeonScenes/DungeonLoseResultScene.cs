using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonLoseResultScene : DungeonScene
    {
        public DungeonLoseResultScene(SceneController controller) : base(controller)
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
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("You Lose");
            Console.WriteLine("Lv.1 Chad");
            Console.WriteLine($"HP {dungeonPlayer.MaxHealth} -> 0");

            Console.WriteLine("0. 다음");
            Console.Write(">>");

        }
        public override DungeonScene HandleInput(int input)
        {
            switch (input)
            {
                case 0:
                    controller.ChangeScene<DungeonStartScene>();
                    break;

            }

            return this;
        }

    }
}

