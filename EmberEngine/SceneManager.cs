using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine
{
    public class SceneManager
    {
        private static List<Scene> scenes;

        public static Scene currentScene { get; private set; }

        public static void Init()
        {
            scenes = new List<Scene>();
        }

        public static void LoadScene(string name)
        {
            Scene foundScene = null;
            foreach (Scene scene in scenes)
            {
                if (scene.name == name)
                {
                    foundScene = scene;
                }
            }



            if (foundScene == null)
            {
                throw new Exception("Invalid scene");
            }

            currentScene = foundScene;
        }

        public static void Render()
        {
            //Console.WriteLine(Globals.application._gl);
            currentScene.Render();
        }

        public static void Update(double dt)
        {
            currentScene.Update(dt);
        }

        public static void AddScene(Scene testScene)
        {
            scenes.Add(testScene);
        }
    }
}
