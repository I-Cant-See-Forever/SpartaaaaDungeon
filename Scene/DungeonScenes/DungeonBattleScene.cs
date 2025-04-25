using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SprtaaaaDungeon.DungeonBattleScene;

namespace SprtaaaaDungeon
{
    class DungeonBattleScene : Scene
    {
        DungeonLayout layout = new();

        DungeonController dungeonController;

        DungeonData currentDungeon;
        PlayerData playerData;
        public enum SelectPhase
        {
            Behavior,
            Skill, 
            Monster
        }
        SelectPhase selectPhase;

        bool isMonsterPhase = false;
        SkillData currentSkill;

        public DungeonBattleScene(SceneController controller) : base(controller) 
        {
            dungeonController = GameManager.Instance.DungeonController;

            playerData = GameManager.Instance.PlayerData;
        }


        public override void Start()
        {
            currentDungeon = dungeonController.CurrentDungeon;
            selectPhase = SelectPhase.Behavior;
            isMonsterPhase = false;

            DrawPlayerInfo(true);
            DrawMonsterInfo(false);
        }

        public override void Update()
        {
            if(isMonsterPhase == false)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo input = Console.ReadKey(true);

                    if (char.IsDigit(input.KeyChar)) // keyinfo 숫자일때
                    {
                        int selectNum = int.Parse(input.KeyChar.ToString());

                        switch (selectPhase)
                        {
                            case SelectPhase.Behavior:

                                switch (selectNum)
                                {
                                    case 1:
                                        SetSelectMonsterPhase();
                                        break;
                                    case 2:
                                        SetSelectSkillPhase();
                                        break;
                                }
                                break;

                            case SelectPhase.Monster:

                                SelectMonster(selectNum, currentSkill != null); 
                                break;

                            case SelectPhase.Skill:

                                if(TrySelectSkill(selectNum))
                                {
                                    SetSelectMonsterPhase();
                                }
                                else
                                {
                                    SetSelectBehaviorPhase();
                                }

                                break;
                        }
                    }
                }
            }
        }

        public override void End()
        {
        }

        bool TrySelectSkill(int input)
        {
            var skillDatas = GameManager.Instance.SkillDatas;

            for (int i = 0; i < skillDatas.Count; i++)
            {
                if (input == i + 1)
                {
                    currentSkill = skillDatas[i];
                    break;
                }
            }

            if(currentSkill.CostMP <= playerData.StatData.CurrentMP)
            {
                DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4}》대상을 선택하세요!");
                return true;
            }
            else
            {
                DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4}》마나가 부족합니다!");
                return false;
            }
        }

        void SetSelectMonsterPhase()
        {
            selectPhase = SelectPhase.Monster;

            DrawRemoveRect(layout.BattleInfo);
            DrawRemoveRect(layout.MonsterInfo);
            DrawRemoveRect(layout.PlayerInfo);

            DrawMonsterInfo(true);
            DrawPlayerInfo(false);
        }

        void SetSelectSkillPhase()
        {
            selectPhase = SelectPhase.Skill;

            DrawRemoveRect(layout.BattleInfo);
            DrawRemoveRect(layout.PlayerInfo);

            DrawMonsterInfo(false);
            DrawSkillInfo();
        }

        void SetSelectBehaviorPhase()
        {
            selectPhase = SelectPhase.Behavior;

            DrawRemoveRect(layout.MonsterInfo);
            DrawRemoveRect(layout.PlayerInfo);

            DrawPlayerInfo(true);
            DrawMonsterInfo(false);
        }


        void SetMonsterPhase()
        {
            isMonsterPhase = true;

            DrawRemoveRect(layout.MonsterInfo);

            DrawMonsterInfo(false);


            Thread.Sleep(1000);

            DrawRemoveRect(layout.BattleInfo);
            DrawRemoveRect(layout.PlayerInfo);

            List<CharacterData> target = new()
            {
                playerData
            };

            for (int i = 0; i < currentDungeon.Monsters.Count; i++)
            {
                dungeonController.TakeDamage(currentDungeon.Monsters[i], out float attackDamage);
                DrawBattleResult(target, attackDamage, i);
                DrawRemoveRect(layout.PlayerInfo);
                DrawPlayerInfo(false);

                if (dungeonController.IsPlayerDead())
                {
                    GameManager.Instance.GameOver();
                    return;
                }
                Thread.Sleep(1000);
            }

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }


            SetSelectBehaviorPhase();

            isMonsterPhase = false;
        }

     

        void SelectMonster(int input, bool isSkill)
        {
            if (input > 0 && input < currentDungeon.Monsters.Count + 1)
            {
                float attackDamage = 0;

                List<CharacterData> targets = new()
                {
                    currentDungeon.Monsters[input - 1]
                };

                if (isSkill)
                {
                    dungeonController.UseAttackSkill(targets, currentSkill, out float resultDamage);

                    attackDamage = resultDamage;

                    currentSkill = null;
                }
                else
                {
                    dungeonController.TryBasicAttack(targets, out float resultDamage);

                    attackDamage = resultDamage;
                }

                DrawBattleResult(targets, attackDamage);

                dungeonController.CheckMonsterDead(targets);

                DrawRemoveRect(layout.MonsterInfo);
                DrawMonsterInfo(false);

                if (dungeonController.CurrentDungeon.Monsters.Count == 0)
                {
                    dungeonController.GetReward();

                    Thread.Sleep(1000);

                    DrawClearText();

                    Console.ReadLine();

                    controller.ChangeScene<DungeonMainScene>();
                }
                else
                {
                    SetMonsterPhase();
                }
            }
        }



        void DrawBattleResult(List<CharacterData> targets, float attackDamage, int deltaY = 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (attackDamage > 0)
                {
                    DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4 + i + deltaY}》{targets[i].Name} 에게 {attackDamage}의 데미지를 입혔습니다!");

                }
                else
                {
                    DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4 + i + deltaY}》{targets[i].Name}을 향한 공격은 실패했습니다..");
                }
            }
        }

        void DrawSkillInfo()
        {
            var skillDatas = GameManager.Instance.SkillDatas;

            DrawStatBar(playerData.StatData.MaxHealth, playerData.StatData.CurrentHealth, "green", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 1);
            DrawStatBar(playerData.StatData.MaxMP, playerData.StatData.CurrentMP, "cyan", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 2);

            for (int i = 0; i < skillDatas.Count; i++)
            {
                DrawString($"《x{layout.PlayerInfo.X + 3},y{layout.PlayerInfo.Y + i * 2 + 4},tmagenta》[{i + 1}] 《》{skillDatas[i].Name} - {skillDatas[i].CostMP}");
                DrawString($"《x{layout.PlayerInfo.X + 3},y{layout.PlayerInfo.Y + i * 2 + 5}》{skillDatas[i].Description}");
            }
        }


        void DrawMonsterInfo(bool isSelect)
        {
            for (int i = 0; i < currentDungeon.Monsters.Count; i++)
            {

                var targetMonster = currentDungeon.Monsters[i];



                if (isSelect)
                {
                    DrawString($"《x{layout.MonsterInfo.X + 2},y{layout.MonsterInfo.Y + i * 3 + 2},tmagenta》[{i + 1}] 《》{targetMonster.Name}");
                }
                else
                {
                    DrawString($"《x{layout.MonsterInfo.X + 2},y{layout.MonsterInfo.Y + i * 3 + 2}》{targetMonster.Name}");
                }

                DrawStatBar(targetMonster.StatData.MaxHealth, targetMonster.StatData.CurrentHealth, "red", layout.MonsterInfo.X + 2, layout.MonsterInfo.Y + i * 3 + 3);
            }
        }

        void DrawPlayerInfo(bool isSelect)
        {
            DrawString($"《x{layout.PlayerInfo.X + 2},y{layout.PlayerInfo.Y}》{playerData.Name}");

            DrawStatBar(playerData.StatData.MaxHealth, playerData.StatData.CurrentHealth, "green", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 1);
            DrawStatBar(playerData.StatData.MaxMP, playerData.StatData.CurrentMP, "cyan", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 2);

            if (isSelect)
            {
                DrawString($"《x{layout.PlayerInfo.X + 3},y{layout.PlayerInfo.Y + 4},tmagenta》[1] 《》공격");
                DrawString($"《x{layout.PlayerInfo.X + 3},y{layout.PlayerInfo.Y + 6},tmagenta》[2] 《》스킬");
            }
            else
            {
                DrawString($"《x{layout.PlayerInfo.X + 3},y{layout.PlayerInfo.Y + 4}》공격");
                DrawString($"《x{layout.PlayerInfo.X + 3},y{layout.PlayerInfo.Y + 6}》스킬");
            }
        }

        void DrawStatBar(float maxData, float curData, string color, int posX, int posY)
        {
            int maxBar = 10;
            int currentBar = (int)(MathF.Ceiling(curData * (maxBar / maxData)));

            if (maxBar < currentBar)
            {
                currentBar = maxBar;
            }
            DrawString($"《x{posX},y{posY}》《bWhite,l{maxBar}》 《》 {curData}/{maxData}\n");
            DrawString($"《x{posX},y{posY}》《b{color},l{currentBar}》 ");
        }

        void DrawClearText()
        {
            var reward = currentDungeon.Reward;

            DrawRemoveRect(layout.BattleInfo);

            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 2}》{currentDungeon.Name} 을 클리어 하셨습니다!\n");
            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 3}》축하합니다!\n");
            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4}》+ {reward.EXP} exp\n");
            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 5}》+ {reward.Gold} gold\n");

            int index = 0;

            foreach (var item in reward.ItemsNameDict)
            {
                DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4 + index}》+ {item.Key} + {item.Value} \n");
                index++;
            }
        }
    }
}
