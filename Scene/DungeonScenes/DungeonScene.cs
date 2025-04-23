using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public abstract class DungeonScene : Scene
    {
        protected DungeonController dungeonController;
        protected DungeonPlayer dungeonPlayer;
        protected PlayerData playerData;
        protected DungeonData monsters;

        public DungeonScene(SceneController controller) : base(controller)
        {
            playerData = GameManager.Instance.PlayerData;
            dungeonController = GameManager.Instance.DungeonController;

            dungeonPlayer = new DungeonPlayer(playerData);
            dungeonPlayer.SetDungeonPlayer();
            monsters = new DungeonData();
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

