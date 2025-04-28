using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class DungeonController
{
    List<DungeonData> dungeonDatas;

    PlayerData playerData;

    public int dungeonIndex;
    public int monsterIndex;
    public int attackTypeIndex;
    public int mkillIndex;
    public List<DungeonData> DungeonDatas => dungeonDatas;
    public DungeonData CurrentDungeon { get; private set; }

    public DungeonController()
    {
        var gameManager = GameManager.Instance;

        if (gameManager.DungeonDatas == null)
        {
            gameManager.DungeonDatas = new List<DungeonData>(); // null이면 새로 만들기
        }

        playerData = gameManager.PlayerData; //플레이어데이터 캐싱

        dungeonDatas = GameManager.Instance.DungeonDatas;
    }

    public void SetDungeon(int input)
    {
        // 이제 이곳은 DungeonData를 참조해 던전 레벨에 맞는 몬스터를 세팅합니다.
        Random random = new Random();

        int randCount = random.Next(1, 4);


        //현재던전을 깨끗한던전으로 초기화
        CurrentDungeon = new DungeonData
        {
             Name = dungeonDatas[input].Name,
             Level = dungeonDatas[input].Level,
             Reward = dungeonDatas[input].Reward,
        }
       ;

        for (int i = 0; i < randCount; i++)
        {
            // 던전 레벨에 맞는 몬스터를 던전에 추가
            CurrentDungeon.Monsters.Add(
                GetLeveledMonsterData(CurrentDungeon, GameManager.Instance.MonsterDatas));
        }

        CurrentDungeon.Reward.EXP = randCount;
    }


    MonsterData GetLeveledMonsterData(DungeonData targetDungeon, List<MonsterData> monsterDatas)
    {
        List<MonsterData> leveledMonsterData = new();

        for (int i = 0; i < monsterDatas.Count; i++)
        {
            //해당 던전 레벨 + 5 까지의 몬스터들을 리스트에 넣어줍니다.
            //이러면 최대체력의 몬스터를 참조해서 추가 하는것이니 따로 몬스터 체력 초기화 안해도됨.
            if (monsterDatas[i].Level >= targetDungeon.Level && monsterDatas[i].Level < targetDungeon.Level + 5)
            {
                leveledMonsterData.Add(monsterDatas[i]);
            }
        }


        //그중에서 한마리를 선택
        Random random = new Random();

        int randCount = random.Next(0, leveledMonsterData.Count);

        MonsterData monsterData = new MonsterData
        {
            Name = leveledMonsterData[randCount].Name,
            Level = leveledMonsterData[randCount].Level,
            StatData = new StatData
            {
               Attack = leveledMonsterData[randCount].StatData.Attack,
               MaxHealth =  leveledMonsterData[randCount].StatData.MaxHealth,
               CurrentHealth =  leveledMonsterData[randCount].StatData.CurrentHealth
            }
        };

        return monsterData;
    }

    public void UseAttackSkill(List<CharacterData> targets, SkillData skillData, out float attackDamage)
    {
        attackDamage = 0;

        if (skillData.TargetCount > 1)
        {
            targets.Clear();

            List<int> useRandList = new List<int>();

            int targetCount = CurrentDungeon.Monsters.Count < skillData.TargetCount ?  CurrentDungeon.Monsters.Count : skillData.TargetCount;


            for (int i = 0; i < targetCount; i++)
            {
                int randTempCount = 0;

                while(true)
                {
                    randTempCount = new Random().Next(0, CurrentDungeon.Monsters.Count);

                    if(!useRandList.Contains(randTempCount))
                    {
                        useRandList.Add(randTempCount);
                        break;
                    }
                }

                targets.Add(CurrentDungeon.Monsters[randTempCount]);
            }
        }
       


        switch (skillData)
        {
            case AttackSkillData attackSkillData:
                attackSkillData.UseSkill(playerData, targets, out float resultValue);
                attackDamage = resultValue;
                break;
        }


        playerData.StatData.CurrentMP -= skillData.CostMP;
    }



    public void TryBasicAttack(List<CharacterData> targets, out float attackDamage, out bool isCritical)
    {
        Random rand = new Random();

        bool isEvade = rand.Next(100) < 10;

        isCritical = false;

        attackDamage = 0;

        if (!isEvade)
        {
            if (targets.Count == 1)
            {
                CharacterData targetMonster = targets[0];

                isCritical = rand.Next(100) < 15;


                var tempDamage = isCritical ? playerData.StatData.Attack * 1.6f : playerData.StatData.Attack;

                int minDamage = (int)(tempDamage * 0.9f);
                int maxDamage = (int)(tempDamage * 1.1f);


                attackDamage = rand.Next(minDamage, maxDamage);

                targetMonster.StatData.CurrentHealth -= attackDamage;
            }
        }
    }

    public void TakeDamage(MonsterData monster, out float attackDamage)
    {
        attackDamage = monster.StatData.Attack;

        playerData.StatData.CurrentHealth -= attackDamage;
    }


    public void GetReward()
    {
        if(playerData.DungeonClearedLevel < dungeonIndex)
        {
            playerData.DungeonClearedLevel++;
        }

        playerData.Gold += CurrentDungeon.Reward.Gold;
        playerData.addExp(CurrentDungeon.Reward.EXP);
        //인벤토리 아이템 추가

        for (int i = 0; i < CurrentDungeon.Reward.EXP; i++) //임시용.. 몬스터수 = exp 이니..
        {
            GameManager.Instance.QuestController.UpdateHuntQuest("");
        }
    }


    public void CheckMonsterDead(List<CharacterData> monsters)
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].StatData.CurrentHealth <= 0)
            {
                if (monsters[i] is MonsterData monsterData)
                {
                    string monsterName = monsterData.Name;
                    var questController = GameManager.Instance.QuestController;

                    questController.UpdateHuntQuest(monsterName);

                    Random random = new Random();

                    if (monsterName == "정유현")
                    {
                        int chance = random.Next(0, 100);
                        if (chance < 50)
                        {
                            questController.UpdateCollectionQuest("정유현의 눈");
                        }
                    }

                    CurrentDungeon.Monsters.Remove(monsterData);
                }
            }
        }
    }

    public bool IsPlayerDead()
    {
        return playerData.StatData.CurrentHealth < 0;
    }

}