﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class SceneController
    {
        Scene previousScene;
        Scene currentScene;
        Scene[] sceneContainer;

        public Scene PreviousScene => previousScene;
        public Scene CurrentScene => currentScene;

        public SceneController()
        {
            sceneContainer = new Scene[]
            {
                new TownScene(this),
                new StatScene(this),
                new QuestMainScene(this),
                new QuestListScene(this),
                new NameScene(this),
                new ClassScene(this),
                new InventoryScene(this),
                new ShopScene(this),
                new DungeonMainScene(this),
                new DungeonBattleScene(this),
                new GameEndScene(this),
                new TitleScene(this),
                new ProfileScene(this)
            };
        }


        public void UpdateScene() => currentScene.Update();

        public void ChangeScene<T>() where T : Scene
        {
            foreach (var item in sceneContainer)
            {
                if (item is T changedScene)
                {
                    if (GameManager.Instance.IsCreatedPlayer)
                    {
                        GameManager.Instance.SaveGame();
                    }

                    previousScene = currentScene;

                    previousScene?.End();

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
                if (item is T)
                {
                    if(GameManager.Instance.IsCreatedPlayer)
                    {
                        GameManager.Instance.SaveGame();
                    }

                    previousScene = currentScene;

                    previousScene?.End();

                    currentScene = scene;

                    Console.Clear();

                    scene.Start();

                    return;
                }
            }
        }

        public T GetScene<T>() where T : Scene
        {
            foreach (var item in sceneContainer)
            {
                if (item is T target)
                {
                    return target;
                }
            }

            return null;
        }    
    }
}
