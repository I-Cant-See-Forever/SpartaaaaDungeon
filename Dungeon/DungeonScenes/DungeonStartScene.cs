using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.Dungeon.DungeonScenes
{
    public class DungeonStartScene : DungeonScene
    {
        public DungeonStartScene(SceneController controller) : base(controller) { }

        public override void Start()
        {

        }
        public override void Update()
        {
            this.show();
            string input = Console.ReadLine();
            this.HandleInput(input);
        }
        public override void End()
        {

        }
        public override void show()
        {
            Console.WriteLine("던전입구");
            Console.WriteLine("던전을 고르시오");
            Console.WriteLine("1. 뉴비던전");
            Console.WriteLine("2. 중수던전");
            Console.WriteLine("3. 고수던전");
            Console.WriteLine("0. 뒤로가기");

        }
        public override DungeonScene HandleInput(string input)
        {
            return this;
        }


    }
}
