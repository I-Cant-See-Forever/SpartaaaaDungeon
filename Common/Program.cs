


namespace SprtaaaaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            GameManager gameManager = new GameManager();

            gameManager.StartGame(true);

            SceneController sceneController = new SceneController();

            //test
            //sceneController.ChangeScene<SceneClassType>();
            sceneController.ChangeScene<DungeonStartScene>();

            while (true)
            {
                sceneController.UpdateScene();
            }
        }
    }
}
