using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    class DungeonBattleScene : Scene
    {
        bool isMonsterPhase = false;


        public enum SelectPhase
        {
            Behavior,
            Skill,
            Monster
        }
        SelectPhase selectPhase;


        DungeonLayout layout = new();


        DungeonController dungeonController;
        DungeonData currentDungeon;
        PlayerData playerData;
        SkillData currentSkill;


        List<string> battInfoList = new();


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
            DrawMonsterImage(false);

            battInfoList.Clear();

            DrawBattleInfo("전투를 시작합니다!", ConsoleColor.Green);
        }

        public override void Update()
        {
            if (isMonsterPhase == false)
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

                                if (TrySelectSkill(selectNum))
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



            if (currentSkill.CostMP <= playerData.StatData.CurrentMP)
            {
                DrawBattleInfo("대상을 선택하세요!", ConsoleColor.Yellow);
            }
            else
            {
                DrawBattleInfo("마나가 부족합니다!", ConsoleColor.Blue);
                currentSkill = null;
            }

            return currentSkill != null;
        }

        void SetSelectMonsterPhase()
        {
            selectPhase = SelectPhase.Monster;

            DrawBattleInfo("대상을 선택하세요!", ConsoleColor.Yellow);

            DrawRemoveRect(layout.MonsterImage);
            DrawRemoveRect(layout.PlayerInfo);

            DrawMonsterImage(true);
            DrawPlayerInfo(false);
        }

        void SetSelectSkillPhase()
        {
            selectPhase = SelectPhase.Skill;

            DrawBattleInfo("스킬을 선택하세요!", ConsoleColor.Yellow);

            DrawRemoveRect(layout.PlayerInfo);

            DrawMonsterImage(false);
            DrawSkillInfo();
        }

        void SetSelectBehaviorPhase()
        {
            selectPhase = SelectPhase.Behavior;

            DrawBattleInfo("행동을 선택하세요!", ConsoleColor.Yellow);

            DrawRemoveRect(layout.MonsterImage);
            DrawRemoveRect(layout.PlayerInfo);

            DrawPlayerInfo(true);
            DrawMonsterImage(false);

        }


        void SetMonsterPhase()
        {
            isMonsterPhase = true;

            DrawRemoveRect(layout.MonsterImage);
            DrawMonsterImage(false);

            List<CharacterData> target = new()
            {
                playerData
            };

            for (int i = 0; i < currentDungeon.Monsters.Count; i++)
            {
                Thread.Sleep(1000);

                dungeonController.TakeDamage(currentDungeon.Monsters[i], out float attackDamage);

                DrawBattleResult(target[0], attackDamage);

                DrawRemoveRect(layout.PlayerInfo);
                DrawPlayerInfo(false);


                if (dungeonController.IsPlayerDead())
                {
                    GameManager.Instance.GameOver();
                    return;
                }
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
                    dungeonController.TryBasicAttack(targets, out float resultDamage, out bool isCrit);

                    attackDamage = resultDamage;

                    if (isCrit)
                    {
                        DrawBattleInfo("치명타!", ConsoleColor.Red);
                        Thread.Sleep(1000);
                    }
                }

                for (int i = 0; i < targets.Count; i++)
                {
                    DrawBattleResult(targets[i], attackDamage);
                    DrawMonsterImage(false, targets[i]);

                    Thread.Sleep(1000);
                }

                dungeonController.CheckMonsterDead(targets);

                DrawRemoveRect(layout.MonsterImage);


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



        void DrawBattleResult(CharacterData target, float attackDamage)
        {
            if (attackDamage > 0)
            {
                DrawBattleInfo($"{target.Name} 은 {attackDamage}의 데미지를 입었습니다!", ConsoleColor.Red);
            }
            else
            {
                DrawBattleInfo($"{target.Name} 은 회피했습니다!", ConsoleColor.Magenta);
            }
        }



        void DrawSkillInfo()
        {
            var skillDatas = GameManager.Instance.SkillDatas;

            DrawString($"《x{layout.PlayerInfo.X + 2},y{layout.PlayerInfo.Y + 2}》{playerData.Name}《tyellow》Lv.{playerData.Level}");

            DrawStatBar(playerData.StatData.MaxHealth, playerData.StatData.CurrentHealth, "green", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 3);
            DrawStatBar(playerData.StatData.MaxMP, playerData.StatData.CurrentMP, "cyan", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 4);

            for (int i = 0; i < skillDatas.Count; i++)
            {
                DrawString($"《x{layout.PlayerInfo.X + 1},y{layout.PlayerInfo.Y + i * 3 + 10},tGreen》[{i + 1}] 《》{skillDatas[i].Name} - {skillDatas[i].CostMP}");
                DrawString($"《x{layout.PlayerInfo.X + 1},y{layout.PlayerInfo.Y + i * 3 + 11}》{skillDatas[i].Description}\n");
            }
        }



        void DrawMonsterImage(bool isSelect, CharacterData damageTarget = null)
        {
            var targetMonsters = currentDungeon.Monsters;

            int posX = layout.MonsterImage.Width / targetMonsters.Count / 2 + 14;

            for (int i = 0; i < targetMonsters.Count; i++)
            {
                var targetMonster = currentDungeon.Monsters[i];

                if (isSelect)
                {
                    DrawString($"《x{posX * (i + 1) + 4},y{layout.MonsterImage.Y + 2},tGreen》[{i + 1}] 《》{targetMonster.Name} 《tyellow》Lv.{targetMonster.Level}");
                }
                else
                {
                    DrawString($"《x{posX * (i + 1) + 4},y{layout.MonsterImage.Y + 2}》    {targetMonster.Name} 《tyellow》Lv.{targetMonster.Level}");
                }

                DrawStatBar(targetMonster.StatData.MaxHealth, targetMonster.StatData.CurrentHealth, "red", posX * (i + 1) + 4, layout.MonsterImage.Y + 3);


                var targetImage = GetMonsterImage(targetMonsters[i]);

                if (damageTarget != null)
                {
                    if(damageTarget == targetMonster)
                    {
                        DrawDirectImage(targetImage, posX * (i + 1), layout.MonsterImage.Y + 4, ConsoleColor.Red);
                    }
                    else
                    {
                        DrawDirectImage(targetImage, posX * (i + 1), layout.MonsterImage.Y + 4, ConsoleColor.DarkGray);
                    }
                }
                else
                {
                    DrawDirectImage(targetImage, posX * (i + 1), layout.MonsterImage.Y + 4, ConsoleColor.DarkGray);
                }
            }
        }



        void DrawPlayerInfo(bool isSelect)
        {
            DrawString($"《x{layout.PlayerInfo.X + 2},y{layout.PlayerInfo.Y + 2}》{playerData.Name}《tyellow》Lv.{playerData.Level}");

            DrawStatBar(playerData.StatData.MaxHealth, playerData.StatData.CurrentHealth, "darkRed", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 3);
            DrawStatBar(playerData.StatData.MaxMP, playerData.StatData.CurrentMP, "cyan", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 4);

            if (isSelect)
            {
                DrawString($"《x{layout.PlayerInfo.X + 1},y{layout.PlayerInfo.Y + 10},tGreen》[1] 《》공격");
                DrawString($"《x{layout.PlayerInfo.X + 1},y{layout.PlayerInfo.Y + 12},tGreen》[2] 《》스킬");
            }
            else
            {
                DrawString($"《x{layout.PlayerInfo.X + 1},y{layout.PlayerInfo.Y + 10}》    공격");
                DrawString($"《x{layout.PlayerInfo.X + 1},y{layout.PlayerInfo.Y + 12}》    스킬");
            }
        }



        void DrawClearText()
        {
            var reward = currentDungeon.Reward;

            DrawRemoveRect(layout.BattleInfo);

            DrawString($"《x{layout.BattleInfo.X + 40},y{layout.BattleInfo.Y + 2},tred》{currentDungeon.Name}《》 을 《tgreen》클리어《》하셨습니다!\n");
            DrawString($"《x{layout.BattleInfo.X + 40},y{layout.BattleInfo.Y + 3}》축하합니다!\n");
            DrawString($"《x{layout.BattleInfo.X + 40},y{layout.BattleInfo.Y + 4},tyellow》+ {reward.EXP} exp\n");
            DrawString($"《x{layout.BattleInfo.X + 40},y{layout.BattleInfo.Y + 5},tyellow》+ {reward.Gold} gold\n");

            int index = 0;

            foreach (var item in reward.ItemsNameDict)
            {
                DrawString($"《x{layout.BattleInfo.X + 40},y{layout.BattleInfo.Y + 4 + index}》+ {item.Key} + 《tmagenta》{item.Value} \n");
                index++;
            }
        }



        void DrawBattleInfo(string infoText, ConsoleColor textColor)
        {
            battInfoList.Add(infoText);

            if (battInfoList.Count > 6)
            {
                battInfoList.RemoveAt(0);
            }

            DrawRemoveRect(layout.BattleInfo);

            for (int i = battInfoList.Count; i > 0; i--)
            {
                if (i == battInfoList.Count)
                {
                    DrawString($"《x{layout.BattleInfo.X + 40}》《y{layout.BattleInfo.Y + 2 + battInfoList.Count - i},t{textColor.ToString()}》" + battInfoList[i - 1]);
                }
                else
                {
                    DrawString($"《x{layout.BattleInfo.X + 40}》《y{layout.BattleInfo.Y + 2 + battInfoList.Count - i}》" + battInfoList[i - 1]);
                }
            }
        }




        string GetMonsterImage(MonsterData targetMonster)
        {
            switch (targetMonster.Name)
            {
                case "임상엽": return TextContainer.sangyeop;
                case "최진안": return TextContainer.jinan;
                case "이수민": return TextContainer.sumin;
                case "장유현": return TextContainer.yuhyeon;
                case "이민혁": return TextContainer.minhyuk;
            }
            return "";
        }
    }
}
