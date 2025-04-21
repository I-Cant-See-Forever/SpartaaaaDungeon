using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.Dungeon.DungeonData
{
    public class MonsterMaker
    {
        private MonsterData monster;
        public List<MonsterData> monsterLists { get; set; }
        public List<MonsterData> dungeonMosters { get; set; }

        public void MakeMonsterLists()
        {
            
            Random randomlevel = new Random();
            
            monsterLists = new List<MonsterData>()
            {
                new Banddit(randomlevel.Next(1, 10)),
                new Banddit(randomlevel.Next(10, 20)),
                new Banddit(randomlevel.Next(20, 30)),
                new Dragon(randomlevel.Next(1, 10)),
                new Dragon(randomlevel.Next(10, 20)),
                new Dragon(randomlevel.Next(20, 30)),
             
            };       
        }

        public void SetDungeon ()// input 값 받아서 던전 3개중 하나로 이동하면서 몬스터 던전타입별로 레벨 제한을 둬서 랜덤한 갯수(1~4사이)로 생성.
        {

            Random random = new Random();
            foreach ( var unit in monsterLists)
            {
                if(unit.Level < 10)
                {
                    dungeonMosters.Add(unit);
                }

            }
            
        }
      

    }
}
