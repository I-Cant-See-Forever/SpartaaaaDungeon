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
        
        private readonly PlayerData originalPlayer;
        private StatData dungeonStatData;
        private PlayerData dungeonPlayer;

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
            //해당 PlayerData 참조 형태 복사본의 스탯들을 프로퍼티에 재할당, 값복사 두번함.
            Name = dungeonPlayer.Name;
            Level = dungeonPlayer.Level;
            MaxHealth = dungeonPlayer.Stat.MaxHealth;
            CurrentHealth = dungeonPlayer.Stat.CurrentHealth;
            Attack = dungeonPlayer.Stat.Attack;
            Defense = dungeonPlayer.Stat.Defense;
        }

        public void ApplyResult() // 던전플레이어 스탯을 원래 플레이어에게 돌려주기
        {

            dungeonPlayer.Level = this.Level;
            dungeonPlayer.Gold = this.Gold;
            dungeonPlayer.Stat.Attack = this.Attack;
            dungeonPlayer.Stat.Defense = this.Defense;
            dungeonPlayer.Stat.MaxHealth = this.MaxHealth;
            dungeonPlayer.Stat.CurrentHealth = this.CurrentHealth;

            originalPlayer.Name = dungeonPlayer.Name;
            originalPlayer.ClassType = dungeonPlayer.ClassType;
            originalPlayer.Level = dungeonPlayer.Level;
            originalPlayer.Gold = dungeonPlayer.Gold;
            originalPlayer.Stat = dungeonStatData;
            //dungeonPlayer = null;
            //dungeonStatData = null;
        }
    }
}
