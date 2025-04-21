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
            PlayerData data = GameManager.Instance.PlayerData;

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {data.Level:D2}");
            Console.WriteLine($"{data.Name} ( {data.ClassType} )");
            Console.WriteLine();
            Console.WriteLine($"공격력 : {data.Stat.Attack}");
            Console.WriteLine($"방어력 : {data.Stat.Defense}");
            Console.WriteLine($"체  력 : {data.Stat.CurrentHealth}");

            Console.WriteLine($"Gold : {data.Gold} G");
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