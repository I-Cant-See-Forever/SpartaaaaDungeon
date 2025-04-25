
using System.Reflection.Emit;

namespace SprtaaaaDungeon
{
    public class StatScene : Scene
    {
        PlayerData playerdata = GameManager.Instance.PlayerData;
        MenuInfoLayout layout = new();
        int StatLayoutX = 5;
        int StatLayoutY = 16;

        public StatScene(SceneController controller) : base(controller)
        {
        }

        public override void End()
        {

        }

        public override void Start()
        {
            Frame();
            DrawTitle();
            DrawStats();
            DrawHeroProfile();
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo Keyinput = Console.ReadKey(true);
                if (Keyinput.Key == ConsoleKey.Escape)
                {
                    controller.ChangeScene<TownScene>();
                }

                // 스탯 증가 감소 테스트용
                if (Keyinput.Key == ConsoleKey.UpArrow)
                {
                    playerdata.StatData.CurrentHealth++;
                    playerdata.addExp(1);
                }
                if (Keyinput.Key == ConsoleKey.DownArrow)
                {
                    playerdata.StatData.CurrentHealth--;
                }
            }
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

        void DrawTitle()
        {
            string title = $"《x{layout.Left.X},y{layout.Right.Y},tgreen》" +
                $"███████╗████████╗ █████╗ ████████╗\r\n" +
                $"██╔════╝╚══██╔══╝██╔══██╗╚══██╔══╝\r\n" +
                $"███████╗   ██║   ███████║   ██║   \r\n" +
                $"╚════██║   ██║   ██╔══██║   ██║   \r\n" +
                $"███████║   ██║   ██║  ██║   ██║   \r\n" +
                $"╚══════╝   ╚═╝   ╚═╝  ╚═╝   ╚═╝   ";
            string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Left.X},tgreen》");

            DrawString(replaceTitle);
        }

        void DrawHeroProfile()
        {
            string image = $"《x{layout.Right.X + 30},y{layout.Right.Y + 3},twhite》" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢟⠫⡑⡑⡐⡐⢌⢐⢌⢊⢛⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢟⠫⡪⣊⠪⡢⡑⡔⡌⢆⢆⢢⢪⢢⢕⠰⣈⠫⡻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢟⠕⡕⢕⠕⡜⡜⡜⡜⢜⢜⢜⢜⢔⠱⡑⡕⢕⠜⡜⢔⢑⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠅⢕⢘⠌⡊⠢⡑⢌⢊⠢⢑⢐⢑⢘⠌⢆⢊⠢⢣⢣⢑⠄⢕⢝⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠌⡨⢐⠠⢁⢂⠑⢄⢑⠄⢕⢐⠐⡐⠡⡡⢑⠄⠅⠅⢕⢐⠅⢕⢔⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢌⢂⠂⠅⠌⡐⠄⢕⢅⢣⢡⢱⠠⡑⠌⢌⢌⢢⠡⡡⠡⢑⠠⠡⡑⡐⡑⢽⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢐⢄⢑⠨⢐⢄⢣⢣⢣⡣⣣⢣⠣⡣⡩⢢⢣⢣⢣⢣⢑⢐⠨⢐⠐⡐⠌⢾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡐⠔⡄⣑⢐⠱⡱⡱⡣⡫⡪⡢⡣⡕⣎⢮⢪⢪⢪⢪⢲⢰⢡⠂⡂⠌⢌⠺⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡧⡣⡣⡪⡪⡣⡣⡪⡪⣪⠪⣊⠪⢌⠢⠡⢱⢨⢐⠢⡱⣹⢸⡐⡐⡨⡢⣑⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⡛⡹⣱⢱⡸⡐⡕⢌⠌⢪⣪⣗⢱⢱⢹⡰⡱⡹⡸⡪⣣⢯⡻⡮⣳⡱⡐⡐⢕⢼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣎⣗⣗⢵⢱⢱⢱⡹⡔⣗⣿⢸⢕⢧⡳⣹⣪⡳⡽⣵⡫⣏⢯⢺⢜⡜⡌⡎⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣳⣣⡳⣕⢗⡵⣳⢝⣯⢷⡳⣝⢮⠺⣕⢗⡯⡿⡵⡝⡮⣪⢳⢕⢧⡣⣣⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣺⡪⡯⣚⢕⢝⢎⢇⢏⠪⡪⢣⡪⡪⡎⡧⡳⡹⡜⡮⣪⢳⢳⢭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣪⢪⢢⢣⢣⢪⢢⢣⢣⢪⡪⡺⡵⡹⡜⡎⡞⣜⢮⢪⢳⡹⡜⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⢱⢱⢹⢸⢘⠜⡌⠎⢎⠎⠇⡇⡇⡗⡝⡜⣎⢎⢧⢣⢣⢣⠂⠠⠉⢋⠛⡛⠻⡻⠿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡵⡱⡱⡑⡔⡐⢌⢊⢆⢅⢇⢎⢎⢎⢎⢇⢇⢗⢕⢕⢕⢕⠅⢀⠈⠄⠂⠄⡁⠂⠅⡂⢍⢙⠻⣿⣿⣿\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⢛⠋⠅⠳⡱⡱⡱⡱⡑⡅⡇⡇⡇⡇⡧⡣⡳⡱⡱⡱⡱⡱⡱⡑⡁⠀⠠⠈⠄⠡⠀⠅⡁⡂⡂⠔⡐⠠⢑⢙\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢟⢉⠂⠅⡂⠌⢀⠁⠕⢕⢕⢕⢕⢕⢕⢵⡱⡣⡣⡣⡣⡣⡱⡸⡸⡸⡸⡈⠀⠀⠂⡈⠄⡁⠨⠐⡀⡂⡂⠅⡂⠅⢂⠐\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢑⠐⠠⠨⠐⡐⢈⠀⠄⢑⠀⠑⢕⢕⢝⢜⢕⢕⠝⡜⢜⢌⢎⢎⢎⢎⢎⠂⠀⡀⠈⡀⠄⠂⠠⢈⠐⡀⡂⠄⠅⡂⠡⢐⠨\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠅⠌⠄⡁⢂⠐⡀⠄⠂⡰⠀⠀⠀⠈⠈⠈⢘⢔⢕⢕⢕⢕⢕⢕⠕⠅⠁⠀⠀⠠⠀⠄⠂⠁⢐⠀⡂⠄⠂⠅⡂⠂⠨⢐⠐\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⣿⡇⠌⠄⡁⢂⠐⡀⠂⠄⠂⠠⡫⠀⠀⠐⠀⠀⠀⡀⠀⠈⠂⠡⠁⠁⠂⠀⠠⠀⠀⠁⢀⠐⠀⡁⢈⠀⡂⢐⠈⠨⢐⢀⠁⠌⡐⠈\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⣿⡿⠐⡈⠄⢂⠐⡀⢂⠡⠀⡁⢸⠅⠀⠠⠀⠀⢀⠀⢀⠀⠂⠀⡀⠠⠀⠀⠂⠀⢀⠀⠁⢀⠀⡁⠄⠀⡂⢐⠠⢈⠐⡀⠂⡀⠂⠄⠁\r\n" +
                            $"⣿⣿⣿⣿⣿⣿⢛⠨⢐⠠⠈⠄⠂⠄⢂⠠⠁⡀⢝⠀⠄⠀⠄⠀⠀⠀⠀⢀⠠⠀⠀⠀⡀⠐⠀⠠⠀⠀⠄⠠⠀⠄⠂⡁⠄⢐⠀⡂⢐⠀⡁⠄⠡⠈⠠\r\n";

            string replaceImage = image.Replace("\r\n", $"\n《x{layout.Right.X + 30},twhite》");

            DrawString(replaceImage);
        }

        void DrawStats()
        {
            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY}》Lv. 《tGreen》{playerdata.Level:D2}");
            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY + 1},tDarkCyan》{playerdata.Name} 《tGray》( 《tYellow》{playerdata.ClassType} 《》)");

            DrawAtkDefStats();

            DrawStatBar("체  력", playerdata.StatData.MaxHealth, playerdata.StatData.CurrentHealth, "red", 6);
            DrawStatBar("마  력", 100, 50, "blue", 7);
            DrawStatBar("경험치", playerdata.MaxExp, playerdata.CurrentExp, "green", 8);

            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY + 10}》Gold : 《tGreen》{playerdata.Gold} 《tYellow》G\n\n");
        }

        void DrawAtkDefStats()
        {
            float bonusAtk = 0;
            float bonusDef = 0;
            for (int i = 0; i < GameManager.Instance.InventoryItems.Count; i++)
            {
                if (GameManager.Instance.InventoryItems[i].IsEquip)
                {
                    bonusAtk += GameManager.Instance.InventoryItems[i].ItemData.StatData.Attack;
                    bonusDef += GameManager.Instance.InventoryItems[i].ItemData.StatData.Defense;
                    playerdata.StatData.Attack += bonusAtk;
                    playerdata.StatData.Defense += bonusDef;
                }
            }
            DrawBonusStats("공격력", playerdata.StatData.Attack, bonusAtk, 3);
            DrawBonusStats("방어력", playerdata.StatData.Defense, bonusDef, 4);
        }

        void DrawBonusStats(string label, float value, float bonusValue, int yNum)
        {
            string bonusStr = bonusValue == 0 ? "" : $"(《tGreen》+{bonusValue}《》)";

            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY + yNum}》{label} : 《tGreen》{value}《》");
            DrawString($" {bonusStr}");
        }

        void DrawStatBar(string label, float maxData, float curData, string color, int yNum)
        {
            int maxBar = 30;
            int currentBar = (int)(MathF.Ceiling(curData * (maxBar / maxData)));

            if (maxBar < currentBar)
            {
                currentBar = maxBar;
            }

            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY + yNum}》{label} : ");
            DrawString($"《x{layout.Left.X + StatLayoutX + 7},y{StatLayoutY + yNum}》《bWhite,l{maxBar}》 《tGreen》 {curData}《》/《tGreen》{maxData}\n");
            DrawString($"《x{layout.Left.X + StatLayoutX + 7},y{StatLayoutY + yNum}》《b{color},l{currentBar}》 ");
        }
    }
}