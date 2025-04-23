using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    private float lastDamage;
    public int LastAttackIndex => lastAttackIndex; // 몬스터 공격시 마지막으로 공격한 몬스터의 인덱스 저장용도
    public float LastDamage => lastDamage;
    public float LastMonsterPrevHealth { get; private set; }

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
        // 우선 initMonster리스트는 앱실행시 모든 몬스터를 원하는 만큼 생성한다.
        // 랜덤한 레벨로 정해진 갯수만큼 반복해서 객체생성함.
        // 같은 레벨로 중복해서 생겨날수도있다, 확률상 거의다름.
        // 몬스터리스트에 이 몬스터들 1~10레벨 랜덤 10번, 10~20레벨 랜덤 10번, 20~30레벨 랜덤 10번으로 초기화해놓음

        Random randomlevel = new Random();

        initMonsters = new List<DungeonData>();
        for (int i = 0; i < 10; i++) //10번복사
        {
            int mixlevel = randomlevel.Next(1, 10); //저레벨던전용 랜덤 레벨구간, 몬스터스탯에 영향.

            initMonsters.Add(new Banddit(mixlevel)); // 저레벨던전용 몹리스트, MonsterList에서 생성한 클래스 추가가능, 저레벨이라 가짓수 적음.
            initMonsters.Add(new SkeletonArchor(mixlevel));
            initMonsters.Add(new SkeletonWarrior(mixlevel));
            initMonsters.Add(new GoblinWizard(mixlevel));
            initMonsters.Add(new Zombie(mixlevel));
        }
        for (int i = 0; i < 10; i++)
        {
            int mixlevel = randomlevel.Next(10, 20); // 중레벨 던전용

            initMonsters.Add(new Banddit(mixlevel));
            initMonsters.Add(new SkeletonArchor(mixlevel));
            initMonsters.Add(new WareWolf(mixlevel));
            initMonsters.Add(new SkeletonWarrior(mixlevel));
            initMonsters.Add(new GoblinWizard(mixlevel));
            initMonsters.Add(new Golem(mixlevel));
            initMonsters.Add(new Zombie(mixlevel));
        }
        for (int i = 0; i < 10; i++)
        {
            int mixlevel = randomlevel.Next(20, 30);  // 고레벨 던전용

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

        initMonsters = initMonsters.OrderBy(x => rd.NextDouble()).ToList(); //배열 섞어주기.
    }


    public void SetDungeon(string input)
    // input 값 받아서 던전 3개중 하나로 이동하면서 몬스터 던전타입별로
    // 던전제한에맞춰 넣어준다. 다합쳐서 들어가는 몬스터는 1~4마리사이, 
    {
        Random random = new Random();
        List<DungeonData> Dungeon1 = new List<DungeonData>();
        List<DungeonData> Dungeon2 = new List<DungeonData>();
        List<DungeonData> Dungeon3 = new List<DungeonData>();
        int lowspawncount = random.Next(1, 5); // 1~4마리, 저레벨던전용스폰확률 변수
        int middlespawncount = random.Next(2, 5); // 확정 2마리 이상
        int highspawncount = random.Next(3, 5); // 확정 3마리 이상
        foreach (var unit in initMonsters)
        {
            if (unit.Level < 10 && Dungeon1.Count < lowspawncount)
            {
                Dungeon1.Add(unit);
            }
            else if (unit.Level < 20 && unit.Level >= 10 && Dungeon2.Count < middlespawncount)
            {
                Dungeon2.Add(unit);
            }
            else if (unit.Level < 30 && unit.Level >= 20 && Dungeon3.Count < highspawncount)
            {
                Dungeon3.Add(unit);
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
        foreach(var unit in dungeonMonsters) // 던전에 들어가기전 몬스터 체력 초기화
        {
            unit.CurrentHealth = unit.MaxHealth;
        }
    }

    public void IsGameOver() // 게임오버체크용, start()에서 호출해서 체크
    {
        if (dungeonplayer.CurrentHealth <= 0)
        {
            GameManager.Instance.SceneController.ChangeScene<DungeonLoseResultScene>();
        }
        else if (dungeonMonsters.All(x => x.CurrentHealth <= 0))
        {
            GameManager.Instance.SceneController.ChangeScene<DungeonWinResultScene>();
        }
    }

    public void IsPlayerAliveCheck() // 플레이어 죽었으면 플래그 false
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
    // 특정 scene의 몬스터들 정보를 출력해주는 메서드, 추후 씬 꾸밀때는 필요없을수있음.
    // 소환된 몬스터 정보 띄워줌.
    // 죽은몬스터는 그레이컬로 색바꿔준뒤, Dead로 변경.
    //0 -> 몬스터 인덱스 번호 없는, 1 -> 몬스터 인덱스 번호있는 값임, input에 넣어줄것.
    {
        int PressAttack = 1;
        if (PressAttack != input)
        {
            for (int i = 0; i < dungeonMonsters.Count; i++)
            {
                var m = dungeonMonsters[i];
                if (m.CurrentHealth > 0)
                {
                    Console.WriteLine($"Lv.{m.Level} {m.Name} {m.CurrentHealth} 공격력 {m.Attack}");

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Lv.{m.Level} {m.Name} Dead"); 
                    isMobDead = true;
                    Console.ResetColor();
                }
            }
            //deathMonsters = dungeonMonsters.Where(x => x.Health <= 0).ToList();

       
        }
        else if(PressAttack == input)
        {
            for (int i = 0; i < dungeonMonsters.Count; i++)
            {
                var m = dungeonMonsters[i];
                if (m.CurrentHealth > 0)
                {
                    Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name}  HP:{m.CurrentHealth} 공격력 {m.Attack}");

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name} Dead");
                    isMobDead = true;
                    Console.ResetColor();
                }

            }
        }

    }

    public void Attack(int input)
    // 몬스터를 선택해서 공격해야한다. input으로 입력한 값 -1이 해당 몬스터의 인덱스일것이다.
    // 이 input -1 몬스터를 골라서 데미지를 입힌다. 
    // 이 몬스터가 체력이 0이라면 이미 사망했다 공격불가능하다고 뜬다. 
    //공격력 오차 10% 10 * 0.9~ 1,1 -> 9 ~ 11 // 
    //공격력 =  Attack * randomvalue.Next(0.9 , (1.1)+1),
    // 소숫점이면 올림처리 (int)Mathf.Celling(값)
    {
        if (input < 0 || input >= dungeonMonsters.Count) return;
        var selectmonster = dungeonMonsters[input]; //내가 선택한 몬스터
        Random randomvalue = new Random();
        if (selectmonster.CurrentHealth <= 0)
        {
            Console.WriteLine("이미 사망한 몬스터입니다.");
            Thread.Sleep(1000);
            return;
        }
        LastMonsterPrevHealth = selectmonster.CurrentHealth; //공격전 몬스터 체력저장
        float baseAttack = dungeonplayer.Attack;
        float dpsFactor = 0.9f + (float)randomvalue.NextDouble() * 0.2f; // 0.9~1.1
        lastDamage = (int)Math.Ceiling(baseAttack * dpsFactor); // 공격력 소숫점 올림처리

        selectmonster.CurrentHealth = Math.Max(0, selectmonster.CurrentHealth -lastDamage); //냅다 공격, 체력에서 확정데미지빼거나, 0 반환
        lastAttackIndex = input; // 마지막으로 공격한 몬스터의 인덱스 저장
        
        GameManager.Instance.SceneController.ChangeScene<DungeonAttackResultScene>(); //공격시 확정적으로 화면전환일어나므로, 
         



    }

    public void TakeDamage(int i) //순회하면서 공격하는용도
    {
        var unit = dungeonMonsters[i];
        if (unit.CurrentHealth <= 0) return;

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