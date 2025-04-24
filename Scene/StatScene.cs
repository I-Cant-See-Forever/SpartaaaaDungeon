
namespace SprtaaaaDungeon
{
    public class StatScene : Scene
    {
        PlayerData playerdata = GameManager.Instance.PlayerData;
        MenuInfoLayout layout = new();
        int StatRange = 15;

        public StatScene(SceneController controller) : base(controller)
        {
        }

        public override void End()
        {

        }

        public override void Start()
        {
            string title = $"《x{layout.Left.X},y{layout.Right.Y},tyellow》" +
                $"███████╗████████╗ █████╗ ████████╗\r\n" +
                $"██╔════╝╚══██╔══╝██╔══██╗╚══██╔══╝\r\n" +
                $"███████╗   ██║   ███████║   ██║   \r\n" +
                $"╚════██║   ██║   ██╔══██║   ██║   \r\n" +
                $"███████║   ██║   ██║  ██║   ██║   \r\n" +
                $"╚══════╝   ╚═╝   ╚═╝  ╚═╝   ╚═╝   ";
            string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Left.X},tyellow》");

            DrawString(replaceTitle);

            DrawString($"《x{layout.Right.X},y{StatRange}》Lv. {playerdata.Level:D2}");
            DrawString($"《x{layout.Right.X},y{StatRange+1},tDarkCyan》{playerdata.Name} 《tGray》( {playerdata.ClassType} )");

            DrawAtkDef();

            DrawString($"《x{layout.Right.X},y{StatRange + 6}》체  력 : ");
            DrawStatBar(playerdata.Stat.MaxHealth, playerdata.Stat.CurrentHealth, "red", 6);
            
            DrawString($"《x{layout.Right.X},y{StatRange + 7}》마  력 : ");
            DrawStatBar(100, 50, "blue", 7);
            
            DrawString($"《x{layout.Right.X},y{StatRange+8}》경험치 : ");
            DrawStatBar(playerdata.maxExp, playerdata.currentExp, "green", 8);

            DrawString($"《x{layout.Right.X},y{StatRange+10}》Gold : {playerdata.Gold} G\n\n");
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
                    playerdata.Stat.CurrentHealth++;
                    playerdata.addExp(1);
                }
                if (Keyinput.Key == ConsoleKey.DownArrow)
                {
                    playerdata.Stat.CurrentHealth--;
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

        void DrawAtkDef()
        {
            float bonusAtk = 0;
            float bonusDef = 0;

            for (int i = 0; i < GameManager.Instance.InventoryItems.Count; i++)
            {
                bonusAtk += GameManager.Instance.InventoryItems[i].ItemData.StatData.Attack;
                bonusDef += GameManager.Instance.InventoryItems[i].ItemData.StatData.Defense;
            }

            string bonusAtkStr = bonusAtk == 0 ? "" : $"(+{bonusAtk})";
            string bonusDefStr = bonusDef == 0 ? "" : $"(+{bonusDef})";

            DrawString($"《x{layout.Right.X},y{StatRange + 3}》공격력 : {playerdata.Stat.Attack} {bonusAtkStr}");
            DrawString($"《x{layout.Right.X},y{StatRange + 4}》방어력 : {playerdata.Stat.Defense} {bonusDefStr}");
        }

        void DrawStatBar(float maxData, float curData, string color, int yNum)
        {
            int maxBar = 30;
            int currentBar = (int)(MathF.Ceiling(curData * (maxBar / maxData)));

            if (maxBar < currentBar)
            {
                currentBar = maxBar;
            }
            DrawString($"《x{layout.Right.X+9},y{StatRange + yNum}》《bWhite,l{maxBar}》 《》 {curData}/{maxData}\n");
            DrawString($"《x{layout.Right.X+9},y{StatRange + yNum}》《b{color},l{currentBar}》 ");
        }
    }
}