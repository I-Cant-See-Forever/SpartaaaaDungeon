namespace SprtaaaaDungeon
{
    public class StatScene : Scene
    {
        public StatScene(SceneController controller) : base(controller)
        {
        }

        public override void End()
        {

        }

        public override void Start()
        {
            float BonusAtk = 0;
            float BonusDef = 0;

            for (int i = 0; i<GameManager.Instance.InventoryItems.Count; i++)
            {
                BonusAtk += GameManager.Instance.InventoryItems[i].ItemData.StatData.Attack;
                BonusDef += GameManager.Instance.InventoryItems[i].ItemData.StatData.Defense;
            }

            PlayerData playerdata = GameManager.Instance.PlayerData;

            DrawString("《tDarkYellow》상태 보기\n");
            DrawString("《tGray》캐릭터의 정보가 표시됩니다.\n\n");

            DrawString($"《tGreen》Lv. 《tGray》{playerdata.Level:D2}\n");
            DrawString($"《tDarkCyan》{playerdata.Name} 《tGray》( {playerdata.ClassType} )\n\n");

            DrawString(BonusAtk == 0 ? $"공격력 : {playerdata.Stat.Attack}\n" : $"공격력 : {playerdata.Stat.Attack} (+{BonusAtk})\n");
            DrawString(BonusDef == 0 ? $"방어력 : {playerdata.Stat.Defense}\n" : $"방어력 : {playerdata.Stat.Defense} (+{BonusDef})\n");
            DrawString($"체  력 : {playerdata.Stat.CurrentHealth}\n");
            DrawString($"Gold : {playerdata.Gold} G\n\n");
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
    }
}