using AuroraEditor.Components;
using EmberEngine;
using EmberEngine.Components;
using System.Numerics;

namespace AuroraEditor
{
    public class Program
    {
        static Application application;

        static void Main(string[] args)
        {
            application = new Application();

            application.OnLoad += OnLoad;
            application.OnUpdate += OnUpdate;

            application.Run();
        }

        private static void OnUpdate()
        {
            if (loadFinished)
            {
                SceneManager.LoadScene("WelcomeToEditor");
                loadFinished = false;
            }
        }

        public static bool loadFinished = false;

        private static void OnLoad()
        {
            /*
            Scene scene = new Scene("WelcomeToEditor", new Vector4(0.17f, 0.17f, 0.17f, 1f));

            

            GameObject welcomeText = new GameObject("Welcome Text");

            welcomeText.AddComponent(new MeshRenderer("Models/welcomeText.gltf", "Shaders/default.vert", "Shaders/default.frag", false));

            welcomeText.transform.rotation.X = 90;

            scene.AddObject(welcomeText);

            GameObject camera = new GameObject("Camera");

            camera.transform.position = new Vector3(0, 3, -10);

            camera.AddComponent(new Camera());

            scene.AddObject(camera);

            scene.Save(scene.name);

            SceneManager.AddScene(scene);

            SceneManager.LoadScene(scene.name);
            */

            UIThemeManager.SetDarkStyle();

            EditorManager.projectLocation = "../../../../TestProject";

            AudioManager.Mute();

            Scene scene = Scene.FromFile("Scenes/Test.scene");
            Console.WriteLine("Loaded");
            SceneManager.AddScene(scene);

            EditorManager.editorType = EditorType.VSCode;


            SceneManager.LoadScene(scene.name);
            
        }
    }
}