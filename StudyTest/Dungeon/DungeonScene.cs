using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.StudyTest.Dungeon
{
    public abstract class DungeonScene : Scene
    {
        protected DungeonController dungeonController => GameManager.Instance.DungeonController;
        protected DungeonPlayer dungeonPlayer;


        public DungeonScene(SceneController controller) : base(controller)
        {



        }

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
        public abstract DungeonScene HandleInput(int input);

    }
}

