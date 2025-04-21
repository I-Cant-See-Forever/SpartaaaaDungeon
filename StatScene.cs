namespace SprtaaaaDungeon
{
    internal class StatScene : Scene
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

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {playerdata.Level:D2}");
            Console.WriteLine($"{playerdata.Name} ( {playerdata.ClassType} )");
            Console.WriteLine();
            Console.WriteLine(BonusAtk == 0 ? $"공격력 : {playerdata.Stat.Attack}" : $"공격력 : {playerdata.Stat.Attack} (+{BonusAtk})");
            Console.WriteLine(BonusDef == 0 ? $"방어력 : {playerdata.Stat.Defense} " : $"방어력 : {playerdata.Stat.Defense} (+{BonusDef})");
            Console.WriteLine($"체  력 : {playerdata.Stat.CurrentHealth}");

            Console.WriteLine($"Gold : {playerdata.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }

        public override void Update()
        {
            int input = int.Parse(Console.ReadLine());
            if (input == 0)
            {
                controller.ChangeScene<TemplateScene>();
            }
        }
    }
}