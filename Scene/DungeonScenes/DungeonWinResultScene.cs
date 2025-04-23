using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonWinResultScene : DungeonScene
    {
        public DungeonWinResultScene(SceneController controller) : base(controller)
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
