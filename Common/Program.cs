


namespace SprtaaaaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            SaveManager saveManager = new SaveManager();
            GameManager gameManager = new GameManager();

            gameManager.StartGame();

            var sceneController = gameManager.SceneController;

            //test
            sceneController.ChangeScene<TitleScene>();


            while (true)
            {
                sceneController.UpdateScene();
            }
        }
    }
}
