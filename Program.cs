using SprtaaaaDungeon.Dungeon.DungeonScenes;

namespace SprtaaaaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            gameManager.StartGame(true);


            SceneController sceneController = new SceneController();

            sceneController.ChangeScene<TemplateScene>(); //GameStartTemplate
            


            while (true)
            {
                sceneController.UpdateScene();
            }
        }
    }
}
