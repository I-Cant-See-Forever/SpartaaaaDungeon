
namespace SprtaaaaDungeon
{
    public class StatScene : Scene
    {
        PlayerData playerData;
        public StatScene(SceneController controller) : base(controller)
        {
        }

        public override void End()
        {

        }

        public override void Start()
        {
            playerData = GameManager.Instance.PlayerData;

            float BonusAtk = 0;
            float BonusDef = 0;

            for (int i = 0; i<GameManager.Instance.InventoryItems.Count; i++)
            {
                BonusAtk += GameManager.Instance.InventoryItems[i].ItemData.StatData.Attack;
                BonusDef += GameManager.Instance.InventoryItems[i].ItemData.StatData.Defense;
            }

            PlayerData playerdata = GameManager.Instance.PlayerData;

            DrawString("《x3,y3》《tDarkYellow》상태 보기\n");
            DrawString("《x3,y4》《tGray》캐릭터의 정보가 표시됩니다.\n\n");

            DrawString($"《x3,y5》《tGreen》Lv. 《tGray》{playerdata.Level:D2}\n");
            DrawString($"《x3,y6》《tDarkCyan》{playerdata.Name} 《tGray》( {playerdata.ClassType} )\n\n");

            DrawString(BonusAtk == 0 ? $"《x3,y7》공격력 : {playerdata.Stat.Attack}\n" : $"《x3,y7》공격력 : {playerdata.Stat.Attack} (+{BonusAtk})\n");
            DrawString(BonusDef == 0 ? $"《x3,y8》방어력 : {playerdata.Stat.Defense}\n" : $"《x3,y8》방어력 : {playerdata.Stat.Defense} (+{BonusDef})\n");
            DrawString($"《x3,y9》체  력 : {playerdata.Stat.CurrentHealth}\n");
            DrawString($"《x3,y10》Gold : {playerdata.Gold} G\n\n");
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
    }
}