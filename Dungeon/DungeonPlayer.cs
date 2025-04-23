using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonPlayer
    {
        private PlayerData dungeonPlayer;
        private PlayerData originalPlayer;
        private StatData dungeonStatData;

        public string Name { get; set; }
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public GameEnum.ClassType ClassType { get; set; }
        public int Level { get; set; }
        public float Gold { get; set; }

        public PlayerData DungeonPlayerClone
        {
            get { return dungeonPlayer; }
            private set { dungeonPlayer = value; }
        }

        public DungeonPlayer(PlayerData player)
        {
            originalPlayer = player;
        }

        public void SetDungeonPlayer()
        {
            dungeonStatData = new StatData(

                originalPlayer.Stat.MaxHealth,
                originalPlayer.Stat.CurrentHealth,
                originalPlayer.Stat.Attack,
                originalPlayer.Stat.Defense
                );

            dungeonPlayer = new PlayerData(

                originalPlayer.Name,
                originalPlayer.ClassType,
                originalPlayer.Level,
                originalPlayer.Gold,
                dungeonStatData
                );
            Name = dungeonPlayer.Name;
            Level = dungeonPlayer.Level;
            MaxHealth = dungeonPlayer.Stat.MaxHealth;
            CurrentHealth = dungeonPlayer.Stat.CurrentHealth;
            Attack = dungeonPlayer.Stat.Attack;
            Defense = dungeonPlayer.Stat.Defense;
        }

        public void ApplyResult()
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
