using EmberEngine;

namespace ReplaceWithGameName
{
    class Program {
        public static void Main(string[] args) {
            // Initialize your application
            Application application = new Application();

            // Subscribe to the OnLoad event to load in scenes.
            application.OnLoad += OnLoad;

            // Start the game
            application.Run();
        }

        private static void OnLoad()
        {
            // Load in sample scene
            Scene scene = Scene.FromFile("./Scenes/Test.scene");

            // Add the scene to the game
            SceneManager.AddScene(scene);

            // Load the scene by name
            SceneManager.LoadScene(scene.name);
        }
    }
}