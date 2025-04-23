using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class SceneController
    {
        Scene currentScene;
        Scene[] sceneContainer;
        DungeonPlayer dungeonPlayer;
        DungeonController dungeonController;
        public SceneController()
        {
            PlayerData pd = GameManager.Instance.PlayerData;
            dungeonPlayer = new DungeonPlayer(pd);
            dungeonPlayer.SetDungeonPlayer();
            dungeonController = new DungeonController(dungeonPlayer);
            dungeonController.MakeMonsterLists();

            sceneContainer = new Scene[]
            {
                new TemplateScene(this),
                new DungeonStartScene(this, dungeonController,dungeonPlayer),
                new DungeonBattleScene(this, dungeonController, dungeonPlayer),
                new DungeonAttackScene(this, dungeonController, dungeonPlayer),
                new DungeonAttackResultScene(this, dungeonController, dungeonPlayer),
                new DungeonTakeHitScene(this, dungeonController, dungeonPlayer),
                new TownScene(this),
                new StatScene(this),
                new QuestScene(this)
            };
        }

       
    

        public void UpdateScene() => currentScene.Update();


        public void ChangeScene<T>() where T : Scene 
        {
            foreach (var item in sceneContainer)
            {
                if (item is T changedScene) 
                {
                    currentScene?.End();

                    currentScene = changedScene;

                    Console.Clear();

                    changedScene.Start();

                    return;
                }
            }
        }
    }
}
