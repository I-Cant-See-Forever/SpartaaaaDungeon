using SprtaaaaDungeon.Dungeon.DungeonScenes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class DungeonController
    {
        private DungeonData monster;
        public List<DungeonData> monsterLists { get; set; }
        public List<DungeonData> dungeonMonsters { get; set; }
        public List<DungeonData> deathMonsters { get; set; }

        private DungeonPlayer dungeonplayer;        

        bool isMobAlive = false;
        bool isPlayerAlive = false;
        bool isGameWin = false;
        bool isGameOver = false;


        public DungeonController(DungeonPlayer player)
        {
            dungeonplayer = player;
          
        }

        public void MakeMonsterLists()
        {
            // 우선 몬스터리스트에 몬스터들을 소환해주는데,
            // 이것도 랜덤한 레벨로 랜덤한 갯수로 넣어준다. 다만 최대 1개이상은 넣어줘야한다.
            // 같은 레벨로 , 혹은 다른 레벨로 중복해서 생겨날수도있다.
            Random randomlevel = new Random();
            
            monsterLists = new List<DungeonData>()
            {
                //new Banddit(randomlevel.Next(1, 10)),
                //new Banddit(randomlevel.Next(10, 20)),
                //new Banddit(randomlevel.Next(20, 30)),
                //new Dragon(randomlevel.Next(1, 10)),
                //new Dragon(randomlevel.Next(10, 20)),
                //new Dragon(randomlevel.Next(20, 30)),
                //new Rich(randomlevel.Next(1, 10)),
                //new Rich(randomlevel.Next(10, 20)),
                //new Rich(randomlevel.Next(20, 30)),
                //new Vampire(randomlevel.Next(1, 10)),
                //new Vampire(randomlevel.Next(10, 20)),
                //new Vampire(randomlevel.Next(20, 30)),
                //new SkeletonArchor(randomlevel.Next(1, 10)),
                //new SkeletonArchor(randomlevel.Next(10, 20)),
                //new SkeletonArchor(randomlevel.Next(20, 30)),            
            };       
        }


        public void SetDungeon(string input)
            // input 값 받아서 던전 3개중 하나로 이동하면서 몬스터 던전타입별로
            // 레벨 제한을 둬서 레벨제한에 맞게 생성한 몬스터들을 다시 넣어준다. 다합쳐서 들어가는 몬스터는 1~4마리사이로 등장해야함. 
        {                     
            Random random = new Random();
            List<DungeonData> Dungeon1 = new List<DungeonData>();
            List<DungeonData> Dungeon2 = new List<DungeonData>();
            List<DungeonData> Dungeon3 = new List<DungeonData>();
            
            foreach (var unit in monsterLists)
            {
                if (unit.Level < 10 && Dungeon1.Count <4)
                {  
                    for (int i = 0; i < random.Next(1,3); i++) 
                    {
                        Dungeon1.Add(unit);
                        if (Dungeon1.Count >= 4)
                        {
                            break;
                        }
                    }                
                }
                else if (unit.Level < 20 && unit.Level >= 10 && Dungeon2.Count < 4)
                {
                    for (int i = 0; i < random.Next(1, 3); i++)
                    {
                        Dungeon2.Add(unit);
                        if (Dungeon1.Count >= 4)
                        {
                            break;
                        }
                    }
                }
                else if (unit.Level < 30 && unit.Level >= 20 && Dungeon3.Count < 4)
                {
                    for (int i = 0; i < random.Next(1, 3); i++)
                    {
                        Dungeon3.Add(unit);
                        if (Dungeon1.Count >= 4)
                        {
                            break;
                        }
                    }
                }            

            }
            switch (input)
            {
                case "1":
                    dungeonMonsters = Dungeon1;
                    break;
                case "2":
                    dungeonMonsters = Dungeon2;
                    break;
                case "3":
                    dungeonMonsters = Dungeon3;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        public void IsAliveCheck() // start()에서 체크해서 죽은 몬스터는 그레이컬로 이름, 선택불가능하게, 전부 죽으면 넘어갈때 화면 전환
        {
            
            foreach (var unit in dungeonMonsters)
            { 
                if (unit.Health <= 0)
                {
                    isMobAlive = false;
               
                }
            }
            deathMonsters = dungeonMonsters.Where(x => x.Health <= 0).ToList();
            if (deathMonsters.Count == dungeonMonsters.Count)
            {
                isGameWin = true;
            }
            if(dungeonplayer.CurrentHealth <= 0)
            {
                isGameOver = true;
            }
            for (int i = 0; i < deathMonsters.Count; i++)
            {
                Console.WriteLine($"{deathMonsters[i].Name}");
            }
            
        }

        public void Attack(int input)
        {
            Random randomvalue = new Random();
            for( int i = 0; i < dungeonMonsters.Count; i++)
            {
                if (dungeonMonsters[input].Health > 0)
                {
                    dungeonMonsters[input].Health -= dungeonplayer.Attack;                    
                }
                else 
                {

                }
            } 
                                 
        }

        public void TakeDamage()
        {
            foreach (var unit in dungeonMonsters)
            {
                if (unit.Health > 0)
                {
                    dungeonplayer.CurrentHealth -= unit.Attack;
                }
            }
        }


    }
}
