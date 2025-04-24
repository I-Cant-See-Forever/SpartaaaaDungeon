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
     
        public SceneController()
        {
            sceneContainer = new Scene[]
            {
                new TemplateScene(this),
                new DungeonStartScene(this),
                new DungeonBattleScene(this),
                new DungeonAttackScene(this),
                new DungeonAttackResultScene(this),
                new DungeonTakeHitScene(this),
                new TownScene(this),
                new StatScene(this),
                new QuestMainScene(this),
                new QuestInfoScene(this),
                new NameScene(this),
                new ClassScene(this),
                new InventoryScene(this),
                new ShopScene(this),
                new DungeonWinResultScene(this),
                new DungeonLoseResultScene(this),
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

        public void ChangeScene<T>(T scene) where T : Scene
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

        public bool TryGetScene<T>(out T targetScene) where T : Scene
        {
            targetScene = null;

            foreach (var item in sceneContainer)
            {
                if (item is T)
                {
                    targetScene = (T)item;
                }
            }

            return targetScene != null;
        }    
    }
}
