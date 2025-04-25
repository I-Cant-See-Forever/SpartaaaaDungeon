using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class GameEndScene : Scene
    {
        public GameEndScene(SceneController controller) : base(controller)
        {
        }

        public override void Start()
        {
            Console.WriteLine("게임 종료,.");
        }

        public override void Update()
        {
            if(Console.KeyAvailable)
            {
                Console.ReadKey(true);

                controller.ChangeScene<TownScene>();
            }
        }


        public override void End()
        {
        }
    }
}
