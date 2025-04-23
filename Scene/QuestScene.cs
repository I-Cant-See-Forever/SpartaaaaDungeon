using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestScene : Scene
    {
        TitleLayout titleLayout = new TitleLayout();
        MenuInfoLayout menuInfoLayout = new MenuInfoLayout();

        List<QuestData> questDatas;


        public enum Phase
        {
            Main,
            QuestList,
            QuestInfo
        }
        Phase phase;



        public QuestScene(SceneController controller) : base(controller)
        {
            questDatas = GameManager.Instance.GameQuestDatas;
        }

        public override void Start()
        {
            phase = Phase.Main;

            DrawTitle();

            /*  DrawString(
                  $"{questDatas[0].Title}\n\n" +
                  $"{questDatas[0].Description}\n\n" +
                  $"{questDatas[0].Condition.Description}  " +
                  $"{questDatas[0].Condition.ProgressText()}"
                  );*/
        }

        public override void Update()
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (phase)
                {
                    case Phase.Main:
                        if (input.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            phase = Phase.QuestList;
                            DrawQuestList();
                        }
                        break;
                    case Phase.QuestList:
                        if(input.Key == ConsoleKey.Escape)
                        {
                            phase = Phase.Main;
                            Console.Clear();
                            DrawTitle();
                        }
                        break;
                }
            }

/*
            GameManager.Instance.QuestController.UpdateHuntQuest("");

            if (questDatas[0].Condition.IsAchive())
            {
                DrawString(
                $"{questDatas[0].Title}\n\n" +
                $"{questDatas[0].Description}\n\n" +
                $"{questDatas[0].Condition.Description}  " +
                $"{questDatas[0].Condition.ProgressText()}\n\n\n" +
                $"완료!\n\n\n" +
                $"보상\n");

                DrawRewardText(questDatas[0].Reward);

                GameManager.Instance.QuestController.AddReward(questDatas[0].Reward);

                DrawString($"{GameManager.Instance.PlayerData.Gold}");
            }
            else
            {
                DrawString(
                  $"{questDatas[0].Title}\n\n" +
                  $"{questDatas[0].Description}\n\n" +
                  $"{questDatas[0].Condition.Description}  " +
                  $"{questDatas[0].Condition.ProgressText()}"
                  );

                DrawString($"{GameManager.Instance.PlayerData.Gold}");
            }*/
        }

        public override void End()
        {
        }



        void DrawRewardText(QuestReward reward)
        {
            foreach (var item in reward.ItemCountDict)
            {
                DrawString($"{item.Key} + {item.Value}\n\n");
            }

            if (reward.Gold > 0)
            {
                DrawString($"+ {reward.Gold} G\n\n");
            }

            if (reward.Exp > 0)
            {
                DrawString($"+ {reward.Exp} EXP\n\n");
            }
        }

        void DrawTitle()
        {
            string title = $"《x{titleLayout.Title.X},y{titleLayout.Title.Y},tyellow》" +
            $" ██████╗ ██╗   ██╗███████╗███████╗████████╗\r\n" +
            $"██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝\r\n" +
            $"██║   ██║██║   ██║█████╗  ███████╗   ██║   \r\n" +
            $"██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║   \r\n" +
            $"╚██████╔╝╚██████╔╝███████╗███████║   ██║   \r\n" +
            $" ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝  ";
            string replaceTitle = title.Replace("\r\n", $"\n《x{titleLayout.Title.X},tyellow》");

            DrawString(replaceTitle);


            DrawString($"《x{titleLayout.Menu.X + 9},y{titleLayout.Menu.Y + 4},tMagenta》▶ 목록 보기");
        }

        void DrawQuestList()
        {
            for (int i = 0; i < questDatas.Count; i++)
            {
                DrawString($"《x{menuInfoLayout.Right.X + 1},y{menuInfoLayout.Right.Y + 1 + (i * 2)},tmagenta》" +
                    $"   {questDatas[i].Title}\n");
                DrawString($"《x{menuInfoLayout.Right.X},y{menuInfoLayout.Right.Y + 1 + (i * 2) + 1}》┣《l{menuInfoLayout.Right.Width - 2}》-《》┫");
            }

            /*DrawString($"《x{menuInfoLayout.Left.X + 9},y{titleLayout.Menu.Y + 4},tdarkgray》▶ 목록 보기");

        

            DrawString($"《x{menuInfoLayout.Right.X},y{menuInfoLayout.Right.Y}》┏《l{menuInfoLayout.Right.Width - 2}》━《》┓");
            DrawString($"《x{menuInfoLayout.Right.X},y{menuInfoLayout.Right.Height - 1}》┗《l{menuInfoLayout.Right.Width - 2}》━《》┛");

            for (int i = 1; i < menuInfoLayout.Right.Height - 1; i++)
            {
                DrawString($"《x{menuInfoLayout.Right.X},y{menuInfoLayout.Right.Y + i}》┃《x{menuInfoLayout.Right.X + menuInfoLayout.Right.Width - 2}》┃");
            }*/
        }
        void DrawQusetInfo()
        {

        }
    }

   
}
