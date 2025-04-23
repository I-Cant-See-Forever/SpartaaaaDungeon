


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

            //gameManager.SaveGame();

        /*    var sceneController = gameManager.SceneController;


            gameManager.LoadGame();*/

           
            /*//test
            sceneController.ChangeScene<QuestScene>();


            while (true)
            {
                sceneController.UpdateScene();
            }*/
        }
    }
}
