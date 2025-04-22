using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public abstract class DungeonScene : Scene
    {
        public DungeonScene(SceneController controller) : base(controller) { }



        public override void Start()
        {

        }
        public override void Update()
        {

        }
        public override void End()
        {

        }

        public abstract void show();
        public abstract DungeonScene HandleInput(string input);
     
    }
}

