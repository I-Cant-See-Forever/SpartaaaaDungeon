

namespace SprtaaaaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            gameManager.StartGame(true);

            Console.CursorVisible = false;
            SceneController sceneController = new SceneController();

            



            while (true)
            {
                sceneController.UpdateScene();
            }
        }
    }
}
