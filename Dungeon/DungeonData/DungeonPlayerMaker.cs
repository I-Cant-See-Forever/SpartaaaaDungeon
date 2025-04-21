using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.Dungeon.DungeonData
{
    class DungeonPlayerMaker
    {
        private PlayerData dungeonPlayer;
        private PlayerData originalPlayer;
     
        private StatData dungeonStatData;

        public PlayerData DungeonPlayer
        {
            get { return dungeonPlayer; }
           private set { dungeonPlayer = value; }
        }

        public DungeonPlayerMaker(PlayerData player)
        {
            originalPlayer = player;
          
            SetDungeonPlayer();
        }

        public void SetDungeonPlayer()
        {
            dungeonStatData = new StatData(

                originalPlayer.Stat.Attack,
                originalPlayer.Stat.MaxHealth,
                originalPlayer.Stat.Defense,
                originalPlayer.Stat.CurrentHealth
                );

            dungeonPlayer = new PlayerData(

                originalPlayer.Name, 
                originalPlayer.ClassType, 
                originalPlayer.Level, 
                originalPlayer.Gold, 
                dungeonStatData
                );
        }

        public void ApplyResult ()
        {
            originalPlayer.Name = dungeonPlayer.Name;
            originalPlayer.ClassType = dungeonPlayer.ClassType;
            originalPlayer.Level = dungeonPlayer.Level;
            originalPlayer.Gold = dungeonPlayer.Gold;
            originalPlayer.Stat = dungeonStatData;
            dungeonPlayer = null;
            dungeonStatData = null;
        }
    }
}
