using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class QuestMainScene : Scene
    {
        (string, Rectangle)[] menuTextRect;

        int animIndex = 0;

        QuestController questController;

        TitleLayout layout = new();

        Stopwatch animWatch = new();


        public QuestMainScene(SceneController controller) : base(controller)
        {
            questController = GameManager.Instance.QuestController;

            menuTextRect = new (string, Rectangle)[]
            {
                new("퀘스트 받기", new()),
                new("진행중인 목록", new()),
                new("완료된 목록", new()),
            };

            for (int i = 0; i < menuTextRect.Length; i++)
            {
                menuTextRect[i].Item2 =
                    new Rectangle(
                        layout.Menu.X + 9,
                        layout.Menu.Y + 4 + i * 2,
                        layout.Menu.Width,
                        1);
            }
        }

        public override void Start()
        {
            questController.SelectQuestIndex = 0;
            Frame();
            DrawTitle();
            DrawImage(questController.SelectTypeIndex);

        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                bool isCorretInput = false;
                int tempSelectNum = questController.SelectTypeIndex;

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        questController.SelectTypeIndex = GetMoveSelectIndex(questController.SelectTypeIndex, -1, menuTextRect.Length - 1);
                        isCorretInput = true;

                        break;
                    case ConsoleKey.DownArrow:
                        questController.SelectTypeIndex = GetMoveSelectIndex(questController.SelectTypeIndex, +1, menuTextRect.Length - 1);
                        isCorretInput = true;
                        break;
                    case ConsoleKey.Enter:
                        controller.ChangeScene<QuestListScene>();
                        break;
                    case ConsoleKey.Escape:
                        controller.ChangeScene<TownScene>();
                        break;
                }

                if (isCorretInput)
                {
                    DrawRemoveRect(menuTextRect[tempSelectNum].Item2);
                    DrawRemoveRect(layout.Image);
                    DrawMenuText(questController.SelectTypeIndex);
                    DrawImage(questController.SelectTypeIndex);
                }
            }
        }

        public override void End()
        {
        }

        void Frame() // 테두리 그리는 함수!
        {
            DrawString($"《x0,y0》┏━《l{Console.WindowWidth - 5}》━《》━┓");

            for (int y = 1; y < Console.WindowHeight - 1; y++)
            {
                DrawString($"《x0,y{y}》┃《x{Console.WindowWidth - 2}》┃");
            }

            DrawString($"《x0,y{Console.WindowHeight - 1}》┗━《l{Console.WindowWidth - 5}》━《》━┛");
        }

        void DrawImage(int num)
        {
            if (num == 1)
            {
                string title = $"《x{layout.Image.X + 25},y{layout.Image.Y - 5},tgray》" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      \r\n" +
                $"⠀⠀⠀⢷⣗⢷⢦⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     \r\n" +
                $"⠀⠀⠀⠈⢻⣷⣍⠷⣿⢷⣤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⢀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⢿⣽⣮⡘⢟⣿⢿⣦⡀⠀⠀⠀⠀⠀⣰⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣿⣽⣟⣿⣻⣽⠫⢠⣲⣶⣖⡄⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠐⣻⣽⣿⡔⠹⣻⣷⣷⣢⠀⠀⠀⢼⣟⣯⣿⣯⣿⣯⡿⣯⣿⣯⡿⣯⣿⣯⡿⣯⣿⣯⣿⡾⣯⣿⣽⣟⡇⢰⣟⣯⣷⣿⣻\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠨⣷⢿⣻⣥⠘⢷⣟⣿⣖⡀⢀⡿⣿⣽⣾⢷⣿⢾⡿⣿⢾⣷⢿⣻⡷⣟⣿⢿⡾⣷⣟⣿⣻⣷⢿⣽⠀⠁⠉⠑⠉⠊⠉\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⢺⣟⣿⣽⡮⡈⢿⣽⣯⣷⡀⠻⣯⣷⠙⠉⠊⠉⠋⠉⠙⠘⠉⢉⠩⠉⠉⠍⠋⠙⠘⢹⣽⣾⢿⡯⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢿⣯⣷⣿⣖⡈⢿⣾⣯⣷⡈⢹⣾⢷⡿⣾⢷⡿⣾⢷⣷⢿⣶⢷⡿⣾⣾⡾⣾⣾⢾⣷⢿⣿⡍⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠩⣷⢿⡾⣟⣦⠈⡷⣿⡾⣧⠐⢽⡿⣟⡿⣟⣿⣟⣿⣻⡿⣽⢿⣻⡿⣾⢿⣻⡾⣿⣻⣟⣷⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣟⣿⣟⣿⢧⠘⣽⣿⣻⣅⠸⣻⡄⡁⡄⡠⡀⡄⡠⣈⣀⣁⣀⣈⣀⣁⡀⣌⣿⣯⣿⣻⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⡷⣟⣿⣻⡥⠘⣯⣿⢷⠀⣫⡿⣿⢿⣿⣻⡿⣿⢷⣿⢾⣷⢿⣾⢷⣿⣻⣾⢷⣟⡯⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣟⣯⡿⣿⣅⠪⣿⢿⡅⢘⣿⡻⡿⡽⡯⡿⣟⢿⡻⡿⡽⣟⢿⡻⡽⣿⢾⣿⣻⡍⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠫⣿⣻⣯⣷⡌⢺⣿⡳⠐⣿⣌⣄⣠⣠⣠⣀⣄⣄⣄⣄⣄⣄⣄⣄⣿⣿⣽⢿⡂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⡽⣯⣯⡂⣻⡃⢬⣿⣽⣯⣿⣾⢷⡿⣷⡿⣾⡷⣿⣾⣻⣾⡷⣿⡾⣟⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⢑⠙⠹⣷⡀⢺⣷⢟⠷⡻⢞⢟⢟⠯⢟⢟⢟⠿⡺⡻⢷⣿⣻⣟⡗⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣺⣟⣯⠄⢹⣧⠐⣻⣧⣄⣤⣠⣠⣠⣄⣤⣠⣠⣠⣠⣄⣾⣯⡿⣯⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⣿⣽⣗⠄⣻⡥⠘⣾⢿⣽⣿⣽⢿⣽⣯⣿⣯⣿⣯⡿⣷⣟⣿⣻⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠨⣾⡷⣟⣿⡥⠐⠟⠄⢺⣻⣷⢿⣾⣟⣿⡾⣷⡿⣾⡷⣿⣻⣽⣯⡿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡸⣟⣿⣻⣯⣿⣦⢶⣵⡿⣯⡿⣟⣷⣿⣽⣟⣿⣻⣟⣿⣻⣯⣿⣾⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⢀⢄⡠⣀⡠⣀⣀⣈⣈⣈⣈⣀⣁⣈⣁⣈⣈⣈⣈⣈⣀⣁⢄⣁⢌⡀⢑⣽⣿⣽⣾⢷⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⢘⣿⣻⣯⣿⣻⣽⣿⣽⢿⣽⣿⣽⢿⣽⣿⣽⢿⣽⣿⣽⣟⣿⣻⣟⡧⠐⣿⢾⣻⣾⢿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢟⣿⣽⣯⣿⢷⣿⢾⣟⣿⣾⣻⣿⣽⡾⣟⣿⣽⣾⢷⡿⣯⣿⣯⣿⢄⠘⠻⠯⠫⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠃⠋⠊⠋⠊⠋⠙⠘⠑⠙⠘⠑⠙⠉⠃⠋⠊⠋⠙⠉⠊⠊⠍⠋⠂⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n";

                string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Image.X + 25},tgray》");

                DrawString(replaceTitle);
            }

            else if (num == 0)
            {
                string title = $"《x{layout.Image.X + 25},y{layout.Image.Y - 5},tgray》" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣖⣶⡆⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢹⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⢿⣽⡗⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣽⣯⣿⣽⣷⢿⣯⡿⣷⣟⣯⣿⢷⣟⣯⣿⢷⣟⣯⣿⢷⣟⣿⣻⣽⡗⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢹⣷⢿⣷⡻⡷⡟⡿⢯⢿⢻⠽⡿⠾⡟⡿⡻⡾⡟⡿⡽⡟⡿⣻⣯⣿⣽⡧⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣻⣽⣇⣄⣀⢄⣀⢄⣀⣄⡠⣀⣄⡠⣀⣀⣀⣀⣀⣠⢨⣿⣽⣷⢿⣇⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢹⣯⣿⣽⡿⣾⣟⣿⣟⣿⣻⣾⢿⣻⣷⢿⣻⣟⣿⣯⣿⣻⣿⣽⡾⣟⣿⡇⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⣹⡿⣾⣯⡿⠯⡟⠷⢟⠯⠿⠽⡟⠯⢿⢻⠻⠽⠷⡟⠾⠯⢷⣟⣿⣻⣽⡏⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢼⣿⣻⣾⣧⣠⣠⣠⣠⣠⣠⣄⣄⣄⣄⣄⣄⣔⣠⣠⣠⣐⣰⣟⣯⣿⣽⡗⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢺⣯⡿⣷⣟⣯⣿⣯⣿⣻⣽⣾⣟⣯⣿⣯⣿⣽⣯⡿⣯⣿⣯⣿⣽⣷⢿⡧⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⣠⣠⠀⢽⣟⣿⣯⡿⡻⡾⡷⣟⢯⢿⢾⡻⡽⡾⡷⡿⡽⡾⡿⢿⢾⢷⢿⡷⣿⣟⡧⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⣞⣿⣽⠀⢺⡿⣷⢿⣇⢄⢄⡠⣀⡠⣀⢄⡠⣀⢄⣀⢄⣀⢄⡠⣀⢄⣨⡿⣟⣿⣽⡧⠀⠀⠀\r\n" +
                $"⠀⠀⠀⢨⣿⣯⡿⠀⣹⣿⣻⣿⣻⡿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⢷⣿⣻⣯⣷⡧⠀⠀\r\n" +
                $"⠀⠀⠀⢸⣷⢿⣻⠀⢺⣯⣿⡾⣟⣿⣻⢽⡯⡷⡿⣽⢯⡷⡿⣽⢯⡷⡿⡽⣯⢿⢿⣽⣯⣿⣾⠧⠀⠀\r\n" +
                $"⠀⠀⠀⢸⣿⣻⡿⠀⣹⣿⢾⣿⣇⡀⣀⣀⢈⡀⣁⢀⣀⣀⢁⣀⣀⡀⣁⢈⢀⡈⡨⣾⢿⣾⣻⡏⠀⠀\r\n" +
                $"⠀⠀⠀⢸⣿⣽⣿⠀⠺⡛⠏⠗⠫⢋⢃⠋⢟⣿⣽⡿⣾⡾⣿⡾⣾⣟⣿⣻⣟⣯⣿⣟⣿⣽⣟⡗⠀⠀\r\n" +
                $"⠀⠀⠀⢸⣿⢾⣷⢀⣴⢶⡿⣾⣯⡿⣟⡧⠈⣿⡾⣿⣻⣟⣯⣿⣯⣿⣽⣿⣽⢿⣾⣻⣽⣷⢿⡗⠀⠀\r\n" +
                $"⠀⠀⠀⢸⣿⣻⡿⣞⣿⣻⣟⣯⡷⡿⡛⢠⣸⣿⣻⣟⣯⣿⣻⣾⢷⡿⣷⢿⣾⣟⣯⣿⣽⣾⢿⣓⠀⠀\r\n" +
                $"⠀⠀⠀⣱⡿⣯⣿⣟⣯⣿⡯⠃⢨⣤⣖⣯⣿⣽⣯⣿⣯⣿⣽⣟⣿⣟⣿⣟⣷⡿⣯⣷⣿⣽⢿⡇⠀⠀\r\n" +
                $"⠀⠀⠀⣺⡿⣿⢾⣻⣽⣾⢿⡀⠹⡷⣟⡿⡾⡿⡾⣷⢿⢾⡯⡿⡷⣿⢽⡯⣷⢿⣟⡷⣿⢾⢿⡏⠀⠀\r\n" +
                $"⠀⠀⠀⢸⡿⣟⣿⣻⣽⣾⢿⣯⣀⡡⣀⣁⣁⡁⠁⠀⠁⠁⠈⠈⠈⠀⠁⠈⠀⠁⠀⠈⠀⠁⠁⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⢟⣿⣻⣽⡿⣾⢿⣯⣿⣻⣿⣽⣟⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠉⠻⠳⢟⠟⠻⠝⢞⠟⠞⠃⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"⠀⠀⠀⠀⢘⣾⣾⢾⣶⢷⡷⣷⡷⣾⡾⣾⡂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"⠀⠀⠀⠀⠈⠙⠊⠋⠋⠋⠋⠋⠋⠋⠋⠋⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n";

                string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Image.X + 25},tgray》");

                DrawString(replaceTitle);
            }

        }



        void DrawTitle()
        {
            string title = $"《x{layout.Title.X},y{layout.Title.Y},tyellow》" +
            $" ██████╗ ██╗   ██╗███████╗███████╗████████╗\r\n" +
            $"██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝\r\n" +
            $"██║   ██║██║   ██║█████╗  ███████╗   ██║   \r\n" +
            $"██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║   \r\n" +
            $"╚██████╔╝╚██████╔╝███████╗███████║   ██║   \r\n" +
            $" ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝  ";
            string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Title.X},tyellow》");

            DrawString(replaceTitle);

            DrawMenuText(questController.SelectTypeIndex);
        }

        void DrawMenuText(int spotLightIndex)
        {

            string[] backSpotlight = new string[menuTextRect.Length];
            string[] selectSign = new string[menuTextRect.Length];


            backSpotlight[spotLightIndex] = "tmagenta";
            selectSign[spotLightIndex] = "▶ ";


            for (int i = 0; i < menuTextRect.Length; i++)
            {
                DrawString($"《x{menuTextRect[i].Item2.X},y{menuTextRect[i].Item2.Y},{backSpotlight[i]}》{selectSign[i]}{menuTextRect[i].Item1}");
            }
        }
    }
}
