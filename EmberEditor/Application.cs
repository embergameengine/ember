using EmberEngine;
using EmberEngine.GUI;
using AuroraEditor.GUI.Windows;
using System.Numerics;
using Silk.NET.Core;
using ImGuiNET;

namespace AuroraEditor
{
    internal class Application : ApplicationBase
    {
        MenuBar mainMenuBar;
        List<Type> windowClasses;
        List<GUIWindow> windows;

        public Application() : base(new Vector2(1920, 1080), "ember editor", true)
        {
            windowClasses = new List<Type>();
            windows = new List<GUIWindow>();

            

            mainMenuBar = new MenuBar(MenuBarType.MainMenuBar);

            Menu fileMenu = new Menu("file");

            MenuItem save = new MenuItem("save");
            save.OnClick += () =>
            {
                Scene currentScene = SceneManager.currentScene;

                currentScene.Save(Path.Combine(EditorManager.projectLocation, "Assets", "Scenes", currentScene.name + ".scene"));
            };
            MenuItem open = new MenuItem("open");
            open.OnClick += () =>
            {
                Console.WriteLine("Open");
            };
            MenuItem newProject = new MenuItem("new");
            newProject.OnClick += () =>
            {
                Console.WriteLine("New");
            };

            fileMenu.AddMenuItem(newProject);
            fileMenu.AddMenuItem(save);
            fileMenu.AddMenuItem(open);

            mainMenuBar.AddMenu(fileMenu);

            Menu windowMenu = new Menu("window");

            windowClasses = new List<Type>()
            {
                typeof(Objects),
                typeof(Inspector),
                typeof(Files),
                typeof(SceneConfig),
                typeof(Performance),
                typeof(ProjectCreator),
                typeof(OpenProject),
                typeof(GameControl),
                typeof(Output)
            };

            windows = new List<GUIWindow>();

            foreach (Type type in windowClasses)
            {
                GUIWindow window = Activator.CreateInstance(type) as GUIWindow;
                MenuItem windowItem = new MenuItem(window.windowName);

                windowItem.OnClick += () =>
                {
                    GUIWindow window = Activator.CreateInstance(type) as GUIWindow;

                    window.Open();

                    windows.Add(window);
                };

                windowMenu.AddMenuItem(windowItem);
            }

            mainMenuBar.AddMenu(windowMenu);
        }
        
        public override void Update(double dt)
        {
            foreach (GUIWindow window in windows)
            {
                window.Update(dt);
            }
        }

        public override void RenderUI()
        {
            mainMenuBar.Render();

            foreach (GUIWindow window in windows)
            {
                window.Render();
            }
        }
    }
}
