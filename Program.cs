namespace SprtaaaaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SceneController sceneController = new SceneController();

            sceneController.ChangeScene<TemplateScene>();

            while (true)
            {
                sceneController.UpdateScene();
            }
        }
    }
}
