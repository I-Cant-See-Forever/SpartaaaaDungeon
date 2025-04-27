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


        QuestController questController;

        TitleLayout layout = new();



        Rectangle rect = new Rectangle(40, 2, 70, 24);

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
                        layout.Menu.X + 6,
                        layout.Menu.Y + 4 + i * 2,
                        layout.Menu.Width,
                        1);
            }
        }

        public override void Start()
        {
            questController.SelectQuestIndex = 0;

            DrawDirectImage(TextContainer.questTitle, layout.Title.X, layout.Title.Y, ConsoleColor.Yellow);
            DrawMenuText(questController.SelectTypeIndex);
            DrawImages();
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
                    DrawMenuText(questController.SelectTypeIndex);
                    DrawRemoveRect(rect);
                    DrawImages();
                }
            }
        }

        public override void End()
        {
        }

        void DrawImages()
        {
            string[] images = new string[]
            {
                $"《x{layout.Image.X + 25},y{layout.Image.Y - 5},tyellow》" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      \r\n" +
                $"⠀⠀《twhite》⠀⢷⣗⢷⢦⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀     \r\n" +
                $"⠀⠀《twhite》⠀⠈⢻⣷⣍⠷⣿⢷⣤⠀⠀⠀⠀⠀⠀《tyellow》⠀⠀⠀⠀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⢀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⢿⣽⣮⡘⢟⣿⢿⣦⡀⠀⠀⠀《tyellow》⠀⠀⣰⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣿⣽⣟⣿⣻⣽⠫⢠⣲⣶⣖⡄⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠐⣻⣽⣿⡔⠹⣻⣷⣷⣢⠀⠀《tyellow》⠀⢼⣟⣯⣿⣯⣿⣯⡿⣯⣿⣯⡿⣯⣿⣯⡿⣯⣿⣯⣿⡾⣯⣿⣽⣟⡇⢰⣟⣯⣷⣿⣻\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠨⣷⢿⣻⣥⠘⢷⣟⣿⣖⡀《tyellow》⢀⡿⣿⣽⣾⢷⣿⢾⡿⣿⢾⣷⢿⣻⡷⣟⣿⢿⡾⣷⣟⣿⣻⣷⢿⣽⠀⠁⠉⠑⠉⠊⠉\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⢺⣟⣿⣽⡮⡈⢿⣽⣯⣷⡀《tyellow》⠻⣯⣷⠙⠉⠊⠉⠋⠉⠙⠘⠉⢉⠩⠉⠉⠍⠋⠙⠘⢹⣽⣾⢿⡯⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⠀⢿⣯⣷⣿⣖⡈⢿⣾⣯⣷⡈《tyellow》⢹⣾⢷⡿⣾⢷⡿⣾⢷⣷⢿⣶⢷⡿⣾⣾⡾⣾⣾⢾⣷⢿⣿⡍⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⠀⠩⣷⢿⡾⣟⣦⠈⡷⣿⡾⣧《tyellow》⠐⢽⡿⣟⡿⣟⣿⣟⣿⣻⡿⣽⢿⣻⡿⣾⢿⣻⡾⣿⣻⣟⣷⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⠀⠀⢹⣟⣿⣟⣿⢧⠘⣽⣿⣻⣅《tyellow》⠸⣻⡄⡁⡄⡠⡀⡄⡠⣈⣀⣁⣀⣈⣀⣁⡀⣌⣿⣯⣿⣻⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⠀⠀⠀⢻⡷⣟⣿⣻⡥⠘⣯⣿⢷《tyellow》⠀⣫⡿⣿⢿⣿⣻⡿⣿⢷⣿⢾⣷⢿⣾⢷⣿⣻⣾⢷⣟⡯⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣟⣯⡿⣿⣅⠪⣿⢿⡅《tyellow》⢘⣿⡻⡿⡽⡯⡿⣟⢿⡻⡿⡽⣟⢿⡻⡽⣿⢾⣿⣻⡍⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠫⣿⣻⣯⣷⡌⢺⣿⡳《tyellow》⠐⣿⣌⣄⣠⣠⣠⣀⣄⣄⣄⣄⣄⣄⣄⣄⣿⣿⣽⢿⡂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀《twhite》⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⡽⣯⣯⡂⣻⡃《tyellow》⢬⣿⣽⣯⣿⣾⢷⡿⣷⡿⣾⡷⣿⣾⣻⣾⡷⣿⡾⣟⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⢑《twhite》⠙⠹⣷⡀《tyellow》⢺⣷⢟⠷⡻⢞⢟⢟⠯⢟⢟⢟⠿⡺⡻⢷⣿⣻⣟⡗⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣺⣟⣯⠄《twhite》⢹⣧《tyellow》⠐⣻⣧⣄⣤⣠⣠⣠⣄⣤⣠⣠⣠⣠⣄⣾⣯⡿⣯⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⣿⣽⣗⠄《twhite》⣻⡥《tyellow》⠘⣾⢿⣽⣿⣽⢿⣽⣯⣿⣯⣿⣯⡿⣷⣟⣿⣻⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠨⣾⡷⣟⣿⡥《twhite》⠐⠟《tyellow》⠄⢺⣻⣷⢿⣾⣟⣿⡾⣷⡿⣾⡷⣿⣻⣽⣯⡿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡸⣟⣿⣻⣯⣿⣦⢶⣵⡿⣯⡿⣟⣷⣿⣽⣟⣿⣻⣟⣿⣻⣯⣿⣾⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⢀⢄⡠⣀⡠⣀⣀⣈⣈⣈⣈⣀⣁⣈⣁⣈⣈⣈⣈⣈⣀⣁⢄⣁⢌⡀⢑⣽⣿⣽⣾⢷⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⢘⣿⣻⣯⣿⣻⣽⣿⣽⢿⣽⣿⣽⢿⣽⣿⣽⢿⣽⣿⣽⣟⣿⣻⣟⡧⠐⣿⢾⣻⣾⢿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⢟⣿⣽⣯⣿⢷⣿⢾⣟⣿⣾⣻⣿⣽⡾⣟⣿⣽⣾⢷⡿⣯⣿⣯⣿⢄⠘⠻⠯⠫⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠃⠋⠊⠋⠊⠋⠙⠘⠑⠙⠘⠑⠙⠉⠃⠋⠊⠋⠙⠉⠊⠊⠍⠋⠂⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ \r\n" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀                                      ⠀⠀⠀⠀⠀ \r\n",

                $"《x{layout.Image.X + 25},y{layout.Image.Y - 5},tyellow》" +
                $"⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣶⢶⡶⣖⣶⡆⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢹⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⢿⣽⡗⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣽⣯⣿⣽⣷⢿⣯⡿⣷⣟⣯⣿⢷⣟⣯⣿⢷⣟⣯⣿⢷⣟⣿⣻⣽⡗⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢹⣷⢿⣷⡻⡷⡟⡿⢯⢿⢻⠽⡿⠾⡟⡿⡻⡾⡟⡿⡽⡟⡿⣻⣯⣿⣽⡧⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣻⣽⣇⣄⣀⢄⣀⢄⣀⣄⡠⣀⣄⡠⣀⣀⣀⣀⣀⣠⢨⣿⣽⣷⢿⣇⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢹⣯⣿⣽⡿⣾⣟⣿⣟⣿⣻⣾⢿⣻⣷⢿⣻⣟⣿⣯⣿⣻⣿⣽⡾⣟⣿⡇⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⣹⡿⣾⣯⡿⠯⡟⠷⢟⠯⠿⠽⡟⠯⢿⢻⠻⠽⠷⡟⠾⠯⢷⣟⣿⣻⣽⡏⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢼⣿⣻⣾⣧⣠⣠⣠⣠⣠⣠⣄⣄⣄⣄⣄⣄⣔⣠⣠⣠⣐⣰⣟⣯⣿⣽⡗⠀⠀      ⠀\r\n" +
                $"       ⠀⠀⠀⠀⠀⠀⠀⠀⢺⣯⡿⣷⣟⣯⣿⣯⣿⣻⣽⣾⣟⣯⣿⣯⣿⣽⣯⡿⣯⣿⣯⣿⣽⣷⢿⡧⠀⠀      ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⠀⠀⣠⣠⠀《tyellow》⢽⣟⣿⣯⡿⡻⡾⡷⣟⢯⢿⢾⡻⡽⡾⡷⡿⡽⡾⡿⢿⢾⢷⢿⡷⣿⣟⡧⠀  ⠀⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⠀⣞⣿⣽⠀《tyellow》⢺⡿⣷⢿⣇⢄⢄⡠⣀⡠⣀⢄⡠⣀⢄⣀⢄⣀⢄⡠⣀⢄⣨⡿⣟⣿⣽⡧⠀  ⠀⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⢨⣿⣯⡿⠀《tyellow》⣹⣿⣻⣿⣻⡿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⣟⣿⢷⣿⣻⣯⣷⡧⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⢸⣷⢿⣻⠀《tyellow》⢺⣯⣿⡾⣟⣿⣻⢽⡯⡷⡿⣽⢯⡷⡿⣽⢯⡷⡿⡽⣯⢿⢿⣽⣯⣿⣾⠧⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⢸⣿⣻⡿⠀《tyellow》⣹⣿⢾⣿⣇⡀⣀⣀⢈⡀⣁⢀⣀⣀⢁⣀⣀⡀⣁⢈⢀⡈⡨⣾⢿⣾⣻⡏⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⢸⣿⣽⣿⠀《tyellow》⠺⡛⠏⠗⠫⢋⢃⠋⢟⣿⣽⡿⣾⡾⣿⡾⣾⣟⣿⣻⣟⣯⣿⣟⣿⣽⣟⡗⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⢸⣿⢾⣷⢀⣴⢶⡿⣾⣯⡿⣟⡧《tyellow》⠈⣿⡾⣿⣻⣟⣯⣿⣯⣿⣽⣿⣽⢿⣾⣻⣽⣷⢿⡗⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⢸⣿⣻⡿⣞⣿⣻⣟⣯⡷⡿⡛《tyellow》⢠⣸⣿⣻⣟⣯⣿⣻⣾⢷⡿⣷⢿⣾⣟⣯⣿⣽⣾⢿⣓⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⣱⡿⣯⣿⣟⣯⣿⡯⠃《tyellow》⢨⣤⣖⣯⣿⣽⣯⣿⣯⣿⣽⣟⣿⣟⣿⣟⣷⡿⣯⣷⣿⣽⢿⡇⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⣺⡿⣿⢾⣻⣽⣾⢿⡀《tyellow》⠹⡷⣟⡿⡾⡿⡾⣷⢿⢾⡯⡿⡷⣿⢽⡯⣷⢿⣟⡷⣿⢾⢿⡏⠀  ⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⢸⡿⣟⣿⣻⣽⣾⢿⣯⣀⡡⣀⣁⣁⡁⠁⠀⠁⠁⠈⠈⠈⠀⠁⠈ ⠁⠈⠀⠁⠁⠁⠁⠀      ⠀⠀\r\n" +
                $"       ⠀《tWhite》⠀⠀⠀⢟⣿⣻⣽⡿⣾⢿⣯⣿⣻⣿⣽⣟⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      ⠀ \r\n" +
                $"       ⠀《tWhite》⠀⠀⠀⠀⠉⠻⠳⢟⠟⠻⠝⢞⠟⠞⠃⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      ⠀  \r\n" +
                $"       ⠀《tblue》⠀⠀⠀⢘⣾⣾⢾⣶⢷⡷⣷⡷⣾⡾⣾⡂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀        \r\n" ,

                $"《x{layout.Image.X + 25},y{layout.Image.Y - 5},tyellow》" +
                $"          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      \r\n" +
                $"          ⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⢀⣾⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⣟⣿⣻⡿⣽⠃⣄⣶⣶⣦⡀⠀⠀⠀⠀ \r\n" +
                $"          ⠀⠀⠀⠀⠀⣺⣯⣿⣽⣿⣽⢿⣽⣿⣽⢿⣽⣿⣽⢿⣽⣿⣽⢿⣽⣿⣽⣟⣿⡇⢰⣿⣽⡾⣿⣽⠀⠀⠀⠀ \r\n" +
                $"          ⠀⠀⠀⠀⠀⣽⡿⣾⢷⣿⢾⣟⣿⣾⣻⣿⣽⡾⣟⣿⣽⡾⣟⣿⣽⣾⣯⡿⣷⡇⠀⠀⠀⠁⠁⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⢾⣿⣻⡯⠊⠉⠉⠁⠉⠁⠉⠈⠉⠉⠉⠈⠉⠉⠉⠈⠑⣿⣻⣯⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣻⣽⣟⣿⡾⣷⢿⣯⡿⣟⣿⢿⣻⣯⣿⣻⣿⣽⣿⣽⣟⣿⣽⣯⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣽⣯⣿⣽⢿⣻⣿⣽⢿⣻⣟⣿⣯⣿⣽⢿⡾⣷⢿⡾⣿⣽⣷⢿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⢷⡿⣷⡯⣀⢀⡀⣀⣀⣀⣀⢀⡀⣀⢀⣀⣀⣀⢁⣀⢠⡿⣾⢿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⢿⣻⣟⣿⣻⣟⣿⢿⡾⣷⡿⣟⣿⣻⣿⣽⡾⣟⣿⣽⢿⣻⣿⣻⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣿⣻⣯⡿⣯⢿⣻⣟⣿⣻⣟⡿⣯⢿⢷⣟⡿⣟⡯⡿⣟⣯⣿⣽⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣯⣿⣽⣯⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⡠⣟⣿⡾⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣷⢿⣾⢷⣿⣻⣟⣯⣿⣯⡿⣯⣿⣻⣟⣯⣿⣟⣿⣻⣟⣿⢷⡿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣟⣿⣽⡿⠹⠛⠝⠛⠞⢳⣿⠯⠋⣃⣉⠙⢑⠙⠙⢻⣽⣟⣿⣟⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣿⣽⣷⣯⣦⣴⣴⣴⣴⣻⠅《tred》⣔⣿⢽⠻⠻⠻⢿⣻⡤《tyellow》⠘⣯⣿⣾⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⡿⣾⡷⣟⣷⣿⣽⡾⣟⠇《tred》⣰⡿⠁⣥⣶⢷⣶⣆⡉⢿⡆《tyellow》⠸⣿⢾⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣿⣻⣟⣿⣽⣾⢷⣿⡏《tred》⢠⣿⠅⣸⢿⣻⡿⣷⢿⣧⢘⣿⡂《tyellow》⢹⡿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣿⣽⣿⣽⣷⢿⡿⣷⣗《tred》⠀⢿⣇⠘⣿⣻⣟⣿⣻⠇⣰⡿⠁《tyellow》⣼⡿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  \r\n" +
                $"          ⠀⠀⠀⠀⠀⣷⡿⣾⡷⣿⣟⣿⢿⣽⣧《tred》⠘⣽⣦⣄⡉⡋⢋⣠⣶⡟《tyellow》⠠⣽⣯⣿⡇⢠⡤⣄⣤⢤⣄⠄⠀⠀⠀ \r\n" +
                $"          ⠀⠀⠀⠀⠀⢳⣿⣻⣟⣯⣿⣽⣿⣽⣾⣦⣈《tred》⠚⠛⠟⠻⠻⠻⠓《tyellow》⣁⣾⣟⣷⡿⣧⠈⢿⣿⣽⢿⠯⠀⠀⠀⠀ \r\n" +
                $"          ⠀⠀⠀⠀⠀⠀⠛⢯⡿⣯⡷⣿⢾⡷⣿⢽⣟⣿⠂《tred》⣲⣶⢶⣴⠀《tyellow》⢾⣯⢿⡷⣿⣻⣦⡈⠚⠛⠉⠁⠀⠀⠀⠀ \r\n" +
                $"          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tred》⣺⣟⣿⣽⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      \r\n" +
                $"          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tred》⣺⣿⡽⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      \r\n" +
                $"          ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀《tred》⠺⠁⠈⠻⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀      \r\n"
            };
            string replaceTitle = images[questController.SelectTypeIndex].Replace("\r\n", $"\n《x{layout.Image.X + 25},tyellow》");

            DrawString(replaceTitle);
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
