namespace SprtaaaaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame(true);

            SceneController sceneController = new SceneController();
            // 이 위는 절대 건들지말고

            //<> 이 괄호 안만 원하는 클래스 씬으로 바꾸면됨. 근데
            //그게 SceneController 에 있어야됨.
            sceneController.ChangeScene<ShopScene>();   //바꿀 칸 


            //여기도 건들지말것.
            while (true)
            {
                sceneController.UpdateScene();
            }


            //SceneController sceneController = new SceneController();

            //sceneController.ChangeScene<TemplateScene>(); //GameStartTemplate
        }
    }
}
