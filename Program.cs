namespace SprtaaaaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            GameManager gameManager = new GameManager();

            gameManager.StartGame(true);

            Console.CursorVisible = false;
            SceneController sceneController = new SceneController();

            //test

            while (true)
            {
                sceneController.UpdateScene();
            }
        }
    }
}
