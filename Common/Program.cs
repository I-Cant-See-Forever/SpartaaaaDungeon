


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

            
            var sceneController = gameManager.SceneController;
            //test
            sceneController.ChangeScene<StatScene>();


            while (true)
            {
                sceneController.UpdateScene();
            }
        }
    }
}
