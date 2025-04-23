using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DungeonController
{
    private DungeonData monster;
    public List<DungeonData> dungeonMonsters { get; set; }
    public List<DungeonData> deathMonsters { get; set; }
    public List<DungeonData> initMonsters { get; set; }

    private DungeonPlayer dungeonplayer;
    public DungeonPlayer DungeonPlayerInstance => dungeonplayer;

    private int lastAttackIndex;
    public int LastAttackIndex => lastAttackIndex; // 몬스터 공격시 마지막으로 공격한 몬스터의 인덱스 저장용도

    public bool isMobDead = false;
    public bool isPlayerAlive = false;
    public bool isGameWin = false;
    public bool isGameOver = false;


    public DungeonController(DungeonPlayer player)
    {
        dungeonplayer = player;



    }

    public void MakeMonsterLists()
    {
        // 우선 몬스터리스트에 몬스터들을 소환해주는데,
        // 이것도 랜덤한 레벨로 랜덤한 갯수로 넣어준다. 다만 최대 1개이상은 넣어줘야한다.
        // 같은 레벨로 , 혹은 다른 레벨로 중복해서 생겨날수도있다.
        // 우선 초기 몬스터 리스트 생성에 몬스터 한개씩 넣어줌, 기본값
        // 몬스터리스트에 이 몬스터들 1~10레벨 랜덤 5번, 10~20레벨 랜덤 5번, 20~30레벨 랜덤 5번으로 넣어줌

        Random randomlevel = new Random();

        initMonsters = new List<DungeonData>();
        for (int i = 0; i < 5; i++) //5번복사
        {
            int mixlevel = randomlevel.Next(1, 10); //저레벨던전용 랜덤 레벨구간

            initMonsters.Add(new Banddit(mixlevel)); // 저레벨던전용 몹리스트, MonsterList에서 추가가능
            initMonsters.Add(new SkeletonArchor(mixlevel));
            initMonsters.Add(new SkeletonWarrior(mixlevel));
            initMonsters.Add(new GoblinWizard(mixlevel));
            initMonsters.Add(new Zombie(mixlevel));
        }
        for (int i = 0; i < 5; i++)
        {
            int mixlevel = randomlevel.Next(10, 20);

            initMonsters.Add(new Banddit(mixlevel));
            initMonsters.Add(new SkeletonArchor(mixlevel));
            initMonsters.Add(new WareWolf(mixlevel));
            initMonsters.Add(new SkeletonWarrior(mixlevel));
            initMonsters.Add(new GoblinWizard(mixlevel));
            initMonsters.Add(new Golem(mixlevel));
            initMonsters.Add(new Zombie(mixlevel));
        }
        for (int i = 0; i < 5; i++)
        {
            int mixlevel = randomlevel.Next(20, 30);

            initMonsters.Add(new Banddit(mixlevel));
            initMonsters.Add(new Dragon(mixlevel));
            initMonsters.Add(new Rich(mixlevel));
            initMonsters.Add(new Vampire(mixlevel));
            initMonsters.Add(new SkeletonArchor(mixlevel));
            initMonsters.Add(new WareWolf(mixlevel));
            initMonsters.Add(new SkeletonWarrior(mixlevel));
            initMonsters.Add(new GoblinWizard(mixlevel));
            initMonsters.Add(new Golem(mixlevel));
            initMonsters.Add(new Zombie(mixlevel));
        }

        Random rd = new Random();

        initMonsters = initMonsters.OrderBy(x => rd.NextDouble()).ToList();
    }


    public void SetDungeon(string input)
    // input 값 받아서 던전 3개중 하나로 이동하면서 몬스터 던전타입별로
    // 던전제한에맞춰 넣어준다. 다합쳐서 들어가는 몬스터는 1~4마리사이, 중복2개 허용 
    {
        Random random = new Random();
        List<DungeonData> Dungeon1 = new List<DungeonData>();
        List<DungeonData> Dungeon2 = new List<DungeonData>();
        List<DungeonData> Dungeon3 = new List<DungeonData>();
        int lowspawncount = random.Next(1, 5); // 1~4마리, 저레벨던전용스폰확률 변수
        int middlespawncount = random.Next(2, 5);
        int highspawncount = random.Next(3, 5);
        foreach (var unit in initMonsters)
        {
            if (unit.Level < 10 && Dungeon1.Count < lowspawncount)
            {
                for (int i = 0; i < random.Next(1, 3); i++) // 중복2개 허용
                {
                    Dungeon1.Add(unit);
                    if (Dungeon1.Count >= lowspawncount)
                    {
                        break;
                    }
                }
            }
            else if (unit.Level < 20 && unit.Level >= 10 && Dungeon2.Count < middlespawncount)
            {
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    Dungeon2.Add(unit);
                    if (Dungeon2.Count >= middlespawncount)
                    {
                        break;
                    }
                }
            }
            else if (unit.Level < 30 && unit.Level >= 20 && Dungeon3.Count < highspawncount)
            {
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    Dungeon3.Add(unit);
                    if (Dungeon3.Count >= highspawncount)
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

    public void IsPlayerAliveCheck() // start()에서 체크해서 죽은 몬스터는 그레이컬로 이름, 선택불가능하게, 전부 죽으면 넘어갈때 화면 전환
    {

        if (dungeonplayer.CurrentHealth <= 0)
        {
            isPlayerAlive = false;
        }
        else
        {
            isPlayerAlive = true;
        }

    }
    public void IsMobAliveCheck(int input)
    // start()에서 체크해서
    // 아직 몬스터들이 조금이라도 살아있다면 우선 소환된 몬스터 띄워줌.
    // 죽은 몬스터는 dungeonMonsters에서 deathMonsters로 복사
    // 죽은몬스터는 그레이컬로 이름바꿔준뒤, Dead로 추가.
    {
        int PressAttack = 1;
        if (PressAttack != input)
        {
            for (int i = 0; i < dungeonMonsters.Count; i++)
            {
                var m = dungeonMonsters[i];
                if (m.Health > 0)
                {
                    Console.WriteLine($"Lv.{m.Level} {m.Name} {m.Health} 공격력 {m.Attack}");

                }
                else
                {
                    Console.WriteLine($"Lv.{m.Level} {m.Name} Dead");
                    isMobDead = true;
                }
            }
            deathMonsters = dungeonMonsters.Where(x => x.Health <= 0).ToList();
        }
        else if(PressAttack == input)
        {
            for (int i = 0; i < dungeonMonsters.Count; i++)
            {
                var m = dungeonMonsters[i];
                if (m.Health > 0)
                {
                    Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name}  HP:{m.Health} 공격력 {m.Attack}");

                }
                else
                {
                    Console.WriteLine($"Lv.{m.Level} {m.Name} Dead");
                    isMobDead = true;
                }

            }
        }
        
    }

    public void Attack(int input)
    // 몬스터를 선택해서 공격해야한다. input으로 입력한 값 -1이 해당 몬스터의 인덱스일것이다.
    // 이 input -1 몬스터를 골라서 데미지를 입힌다. 
    // 이 몬스터가 체력이 0이라면 이미 사망했다 공격불가능하다고 뜬다. 
    {
        var selectmonster = dungeonMonsters[input - 1];
        Random randomvalue = new Random();
        if (selectmonster.Health > 0)
        {
            selectmonster.Health -= dungeonplayer.Attack;
        }
        else if (selectmonster.Health <= 0)
        {
            Console.WriteLine("이미 사망한 몬스터입니다.");
        }
        lastAttackIndex = input - 1; // 마지막으로 공격한 몬스터의 인덱스 저장
    }

    public void TakeDamage(int i) //순회하면서 공격하는용도
    {
        var unit = dungeonMonsters[i];
        if (unit.Health <= 0) return;

        else if (dungeonplayer.CurrentHealth < unit.Attack)
        {
            dungeonplayer.CurrentHealth = 0;
            isPlayerAlive = false;
        }
        else
        {
            dungeonplayer.CurrentHealth -= unit.Attack;
        }


    }


}