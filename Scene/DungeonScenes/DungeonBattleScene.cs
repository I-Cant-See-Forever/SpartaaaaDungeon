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


        public DungeonBattleScene(SceneController controller) : base(controller) 
        {
            dungeonController = GameManager.Instance.DungeonController;

            playerData = GameManager.Instance.PlayerData;
        }


        public override void Start()
        {
            currentDungeon = dungeonController.CurrentDungeon;


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
                                SelectBehavior(selectNum); break;
                            case SelectPhase.Monster:
                                SelectMonster(selectNum, false); break;

                        }
                    }
                }
            }
        }

        public override void End()
        {
        }


        void StartMonsterPhase()
        {
            isMonsterPhase = true;


            Thread.Sleep(1000);

            DrawRemoveRect(layout.BattleInfo);


            for (int i = 0; i < currentDungeon.Monsters.Count; i++)
            {
                dungeonController.TakeDamage(currentDungeon.Monsters[i], out float attackDamage);
                DrawBattleResult(playerData.Name, attackDamage, i);

                if(dungeonController.IsPlayerDead())
                {
                    GameManager.Instance.GameOver();
                }
                Thread.Sleep(1000);
            }

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }


            DrawPlayerInfo(true);

            isMonsterPhase = false;
        }


        void SelectBehavior(int input)
        {
            switch (input)
            {
                case 1:
                    selectPhase = SelectPhase.Monster;

                    DrawRemoveRect(layout.BattleInfo);
                    DrawMonsterInfo(true);
                    DrawPlayerInfo(false);
                    break;
            }
        }

        void SelectMonster(int input, bool isSkill)
        {
            if (input > 0 && input < currentDungeon.Monsters.Count + 1)
            {
                selectPhase = SelectPhase.Behavior;

                MonsterData targetMonster = currentDungeon.Monsters[input - 1];

                if (isSkill)
                {

                }
                else
                {
                    dungeonController.TryBasicAttack(input - 1, out float attackDamage);

                    DrawBattleResult(targetMonster.Name, attackDamage);
                }

                dungeonController.IsMonsterDead(targetMonster, out bool isClear);

                DrawMonsterInfo(false);

                if (isClear)
                {
                    Thread.Sleep(1000);

                    DrawClearText();

                    Console.ReadLine();

                    controller.ChangeScene<DungeonMainScene>();
                }
                else
                {
                    StartMonsterPhase();
                }
            }
        }


        void DrawBattleResult(string targetName, float attackDamage, int deltaY = 0)
        {

            if (attackDamage > 0)
            {
                DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4 + deltaY}》{targetName} 에게 {attackDamage}의 데미지를 입혔습니다!");

            }
            else
            {
                DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4 + deltaY}》{targetName}을 향한 공격은 실패했습니다..");
            }
        }




        void DrawMonsterInfo(bool isSelect)
        {
            DrawRemoveRect(layout.MonsterInfo);


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

                DrawStatBar(targetMonster.MaxHealth, targetMonster.CurrentHealth, "red", layout.MonsterInfo.X + 2, layout.MonsterInfo.Y + i * 3 + 3);
            }
        }

        void DrawPlayerInfo(bool isSelect)
        {
            DrawRemoveRect(layout.PlayerInfo);

            DrawString($"《x{layout.PlayerInfo.X + 2},y{layout.PlayerInfo.Y}》{playerData.Name}");

            DrawStatBar(playerData.Stat.MaxHealth, playerData.Stat.CurrentHealth, "green", layout.PlayerInfo.X + 2, layout.PlayerInfo.Y + 1);

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

            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y}》{currentDungeon.Name} 을 클리어 하셨습니다!\n");
            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 1}》축하합니다!\n");
            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 2}》+ {reward.EXP} exp\n");
            DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 3}》+ {reward.Gold} gold\n");

            int index = 0;

            foreach (var item in reward.Items)
            {
                DrawString($"《x{layout.BattleInfo.X + 5},y{layout.BattleInfo.Y + 4 + index}》+ {item.Key.Name} + {item.Value} \n");
                index++;
            }
        }
    }
}
