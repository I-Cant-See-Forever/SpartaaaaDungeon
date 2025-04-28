using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.StudyTest.Dungeon
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
            /* dungeonStatData = new StatData(

                 originalPlayer.StatData.MaxHealth,
                 originalPlayer.StatData.CurrentHealth,
                 originalPlayer.StatData.Attack,
                 originalPlayer.StatData.Defense
                 );*/

            /*dungeonPlayer = new PlayerData(

                originalPlayer.Name,
                originalPlayer.ClassType,
                originalPlayer.Level,
                originalPlayer.Gold,
                dungeonStatData
                );*/
            //해당 PlayerData 참조 형태 복사본의 스탯들을 프로퍼티에 재할당, 값복사 두번함.
            Name = dungeonPlayer.Name;
            Level = dungeonPlayer.Level;
            MaxHealth = dungeonPlayer.StatData.MaxHealth;
            CurrentHealth = dungeonPlayer.StatData.CurrentHealth;
            Attack = dungeonPlayer.StatData.Attack;
            Defense = dungeonPlayer.StatData.Defense;
        }

        public void ApplyResult() // 던전플레이어 스탯을 원래 플레이어에게 돌려주기
        {

            dungeonPlayer.Level = Level;
            dungeonPlayer.Gold = Gold;
            dungeonPlayer.StatData.Attack = Attack;
            dungeonPlayer.StatData.Defense = Defense;
            dungeonPlayer.StatData.MaxHealth = MaxHealth;
            dungeonPlayer.StatData.CurrentHealth = CurrentHealth;

            originalPlayer.Name = dungeonPlayer.Name;
            originalPlayer.ClassType = dungeonPlayer.ClassType;
            originalPlayer.Level = dungeonPlayer.Level;
            originalPlayer.Gold = dungeonPlayer.Gold;
            originalPlayer.StatData = dungeonStatData;
            //dungeonPlayer = null;
            //dungeonStatData = null;
        }
    }
}
