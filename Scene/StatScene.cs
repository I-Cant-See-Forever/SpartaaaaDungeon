
using System.Reflection.Emit;

namespace SprtaaaaDungeon
{
    public class StatScene : Scene
    {
        int StatLayoutX = 5;
        int StatLayoutY = 16;

        PlayerData playerdata = GameManager.Instance.PlayerData;

        MenuInfoLayout layout = new();


        public StatScene(SceneController controller) : base(controller)
        {
        }


        public override void Start()
        {
            DrawFrame();

            DrawDirectImage(TextContainer.statTitle, layout.Left.X, layout.Left.Y, ConsoleColor.Green);

            DrawStats();

            DrawProfileImage();
        }

        public override void End()
        {

        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo Keyinput = Console.ReadKey(true);

                switch(Keyinput.Key)
                {
                    case ConsoleKey.Escape:
                        controller.ChangeScene(controller.PreviousScene);
                        break;
                    case ConsoleKey.P:
                        controller.ChangeScene(controller.PreviousScene);
                        break;
                }
            }
        }



        void DrawProfileImage()
        {
            string[] profileImages = new string[]
            {
                $"《x{layout.Right.X + 30},y{layout.Right.Y + 3},twhite》" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠟⡋⠍⠝⠩⠩⠙⡙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠓⡁⠡⠐⢈⠠⠁⠌⡐⢀⠂⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠅⠄⠡⢈⠠⠐⢈⠠⠐⡀⡊⠠⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⣿⣿⣿⣿⣿⡣⠈⠄⠡⢀⢂⢐⠠⡂⡐⡐⢄⢑⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⢮⢿⢿⢿⣻⣻⡣⡈⢬⢨⢠⢢⡰⣸⣂⠢⣸⣐⡄⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣗⣗⢽⢽⣝⣗⣗⣗⡗⠅⡧⡣⣂⣸⡘⣮⣢⣁⣔⣗⣇⢷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⡿⡟⣟⣕⣗⣗⣗⣝⣗⡗⡷⣽⣿⣿⡔⡕⣝⣞⢾⡸⣺⣺⣺⣺⡺⡮⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⡿⡑⢜⢞⢮⢮⢮⣺⣜⣜⣾⣾⣿⣿⣿⣿⡧⡣⡯⣳⢳⢳⢳⣳⡳⡽⡝⡝⡟⡟⡟⡟⡟⡟⣟⢿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⢐⠅⡪⢐⢅⢣⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣮⢪⢪⡪⡯⣳⡳⡽⡽⡝⡜⡸⡘⡌⢆⢕⢱⢘⠜⡜⡜⡝⡝⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⠠⡑⢌⠢⡢⢻⣿⣿⣿⣿⣿⣿⣿⡿⡿⣿⡿⡛⠍⠌⠪⡪⡪⡪⡫⣯⠻⡘⡌⡢⡑⢜⢐⠕⡸⡰⡱⡜⡮⡪⣪⢢⢣⠻⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⠐⢌⠢⡑⣼⣾⡿⡿⡿⡛⣛⢛⢋⠎⡌⡢⡑⡐⠨⠨⡐⡨⠘⢌⢍⠢⡑⠜⢌⢂⠪⡂⢕⢨⢪⢪⢪⢪⢪⢪⡪⡳⡱⡱⡱⡹⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⢈⠢⡑⢌⢋⠢⡁⡊⡢⢊⠔⡡⠣⡑⢌⠢⡑⢌⠌⢄⠑⡌⢌⠢⡊⢌⢜⠨⡂⠕⡨⢐⠐⡕⡕⡥⡱⡱⡱⡱⡱⡩⣣⢹⢸⢸⢸⢿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⡀⢪⠨⠂⠢⠨⡂⠜⡐⢔⢑⠔⡁⡊⡢⡑⢌⠢⡑⡱⢨⢈⠢⡃⡎⡢⠢⡑⢌⢂⢂⢂⠡⡕⡕⡕⣕⡅⡇⣇⢧⢳⢕⢇⢇⢇⢇⢟⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⠨⢐⠡⠡⠡⡑⢌⠢⡨⠨⠢⡑⢔⢐⢐⠨⢂⠕⡨⢐⠑⢌⢆⢊⠢⡑⡱⡈⡂⡂⡂⡐⢄⠕⡅⡏⣦⡹⢇⢿⢼⢸⢸⡠⡣⡪⡪⡪⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⠈⠔⡨⢘⢐⠔⢄⠑⠌⡪⠨⡊⡢⡑⢔⢑⢄⠑⢌⠢⡑⢔⢈⠢⠑⢌⠢⡊⠔⡐⡔⢜⠄⠅⡇⣳⡳⡯⡯⣯⣟⢇⠇⡇⣇⢇⢳⠸⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⡡⡑⢌⢂⠆⠅⢕⠡⡡⢂⠡⡑⢔⠨⡂⢕⢐⠅⢕⠨⠨⠢⡑⢌⠪⡐⡐⠨⠐⢌⢎⢢⢱⢁⠎⢄⠋⠯⡯⡳⡹⡸⡨⢪⢪⠐⠤⡑⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣾⣶⣥⣎⣌⣆⣵⣬⣶⣶⣾⢄⠕⢌⠢⡂⠅⠕⡡⠣⡑⢌⠢⡑⢔⠨⠠⠁⡇⡣⡱⡱⢵⢕⣅⢣⠱⡘⡌⢎⢆⠣⢣⢑⠜⢜⠔⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢕⢈⠢⡑⢌⠪⡨⡐⡡⡈⠢⡑⢌⠢⡊⠄⡁⠕⢕⢜⠔⢅⡳⡽⣝⢧⣇⣎⡎⡆⡏⡆⠕⡕⢅⢇⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⡢⠡⡊⡐⢅⠢⡑⠔⢌⠪⡐⡐⠅⡊⠢⡂⡈⢆⢧⣳⡳⡽⣝⢮⣳⣳⡳⣇⢇⢇⢇⢇⢪⢊⢆⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⡪⢐⠌⡢⢑⠌⢔⠡⠡⡑⢌⠢⡑⢌⠢⢂⢂⠐⢕⢜⢮⢯⢮⣳⣳⡳⡝⠎⡎⡎⡎⡪⡪⡪⣢⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⡪⢐⠅⡪⢐⠅⢕⠨⡨⢂⠅⢕⠨⡂⠕⡡⢂⠌⢘⡘⡜⡝⡕⡗⡓⡕⡌⡎⡎⡆⣜⢸⢼⣾⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⢈⠢⡑⢌⠢⠡⡑⢌⠢⡑⢌⠢⡑⠌⢌⠢⡡⢊⢠⢪⢐⢅⠪⡘⡜⡜⡜⢜⠨⠸⡸⡵⡙⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⢕⠨⢂⠅⢕⠨⡂⢕⠨⡂⢕⠨⡊⡢⡑⢌⢂⣎⠪⡐⣐⢑⠸⡸⢼⡽⡆⡕⠨⠸⡘⣎⢎⢿⣿⣿⣿⣿\r\n",

                $"《x{layout.Right.X + 30},y{layout.Right.Y + 3},twhite》" +
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
                $"⣿⣿⣿⣿⣿⣿⢛⠨⢐⠠⠈⠄⠂⠄⢂⠠⠁⡀⢝⠀⠄⠀⠄⠀⠀⠀⠀⢀⠠⠀⠀⠀⡀⠐⠀⠠⠀⠀⠄⠠⠀⠄⠂⡁⠄⢐⠀⡂⢐⠀⡁⠄⠡⠈⠠\r\n",

                $"《x{layout.Right.X + 30},y{layout.Right.Y + 3},twhite》" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠟⠛⠛⠛⠛⠛⠿⠿⢿⣿⣿⣿⣿⣿⠃⡀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠻⠙⠁⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠻⢿⠃⣸⣇⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⡀⠘⠛⠿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⠃⠀⠀⠀⠀⠈⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠛⠋⠀⠀⠀⠀⠀⠀⠀⠈⠹⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠀⢰⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⠀⠀⠀⠀⠀⢸⣇⣀⣀⣄⣤⣤⣤⣤⣤⣠⡸⠿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⠀⢀⡠⡴⠄⠁⠀⠀⠀⠉⡻⣿⣿⡿⠏⠀⠀⠀⠀⠤⠴⠤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⡿⠻⠋⠛⠀⠂⢠⣤⣴⣴⣤⣤⡤⣤⣤⣄⠈⠏⠠⣶⣶⡶⡶⣶⣶⣶⣶⣶⣶⡆⠀⣤⣴⣦⣤⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⡟⠁⣴⣿⣷⠀⡆⢸⣿⣿⣿⣿⠏⠀⠀⣿⡇⢀⣤⠀⣿⣿⠁⠀⢸⣿⣿⣿⣿⣿⠃⢸⣿⣿⣿⣿⣷⠀⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⠀⣇⠨⣿⣿⣿⣿⣧⣠⣼⣿⠀⣸⣿⡆⠸⣿⣤⣴⣿⣿⣿⣿⣿⡟⢀⣿⣿⣿⣿⣿⡿⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣷⡀⢻⣿⣿⡀⣿⣄⣈⣉⣉⣉⣉⣉⣉⣁⠀⣾⣿⣿⣄⣉⣉⣉⣉⣉⣉⣉⣩⣡⣼⣿⣿⣿⣿⡿⠁⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠙⢿⡆⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⢐⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⠁⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⠀⣿⣿⣿⣿⣿⣿⣿⣿⡇⠘⡉⣁⣤⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣆⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⠈⢻⣿⣿⣿⣿⣷⣤⣉⣉⣉⣤⣼⣿⣿⣿⣿⣿⣿⡟⠁⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣄⠙⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠃⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣤⣈⠙⠻⢿⢿⣿⣿⣿⡿⡿⠻⠋⢀⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n" +
                $"⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣤⣤⣠⣀⣄⣤⣤⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿\r\n"

            };

            string replaceProfileImage = profileImages[(int)playerdata.ProfileType].Replace("\r\n", $"\n《x{layout.Right.X + 30},twhite》");

            DrawString(replaceProfileImage);

        }

        void DrawStats()
        {
            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY}》Lv. 《tGreen》{playerdata.Level:D2}");
            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY + 1},tDarkCyan》{playerdata.Name} 《tGray》( 《tYellow》{playerdata.ClassType} 《》)");

            DrawAtkDefStats();

            DrawStatBar("체  력", playerdata.StatData.MaxHealth, playerdata.StatData.CurrentHealth, "green", 6);
            DrawStatBar("마  력", playerdata.StatData.MaxMP, playerdata.StatData.CurrentMP, "cyan", 7);
            DrawStatBar("경험치", playerdata.MaxExp, playerdata.CurrentExp, "yellow", 8);

            DrawString($"《x{layout.Left.X + StatLayoutX},y{StatLayoutY + 10}》Gold : 《tGreen》{playerdata.Gold} 《tYellow》G\n\n");
        }

        void DrawAtkDefStats()
        {
            float bonusAtk = 0;
            float bonusDef = 0;

            var equipItems = GameManager.Instance.InventoryController.EquipItems;

            for (int i = 0; i < equipItems.Count; i++)
            {
                bonusAtk += equipItems[i].StatData.Attack;
                bonusDef += equipItems[i].StatData.Defense;
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