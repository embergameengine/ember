using ImGuiNET;
using EmberEngine.GUI;
using EmberEngine;
using EmberEngine.Components;
using System.Diagnostics;
using System.Reflection;

namespace AuroraEditor.GUI.Windows
{
    public class Files : GUIWindow
    {
        public Files()
        {
            windowName = "files";
        }

        public override void RenderContent()
        {
            ImGui.Text("Assets");
            ImGui.Indent();
            ImGui.Text("Scenes");
            ImGui.Indent();
            foreach (string file in Directory.EnumerateFiles(Path.Join(EditorManager.projectLocation, "Assets", "Scenes")))
            {
                if (ImGui.Selectable(Path.GetFileName(file))) {
                    if (file.EndsWith(".scene"))
                    {
                        SceneManager.LoadScene("UnloadedScene");

                        /*Scene.FromFile(file, (scene) =>
                        {
                            SceneManager.AddScene(scene);

                            SceneManager.LoadScene(scene.name);
                            return 0;
                        });*/

                        Assembly editorAsm = Assembly.Load(File.ReadAllBytes(EditorManager.projectLocation + "\\CSharp\\bin\\Debug\\net6.0\\CSharp.dll"));

                        Scene scene = Scene.FromFile(file, editorAsm);
                        Console.WriteLine("Loaded");
                        SceneManager.AddScene(scene);
                        SceneManager.LoadScene(scene.name);

                        Output.SetOutput("Loaded scene " + scene.name, 5000);
                    }
                }
            }
            ImGui.Unindent();

            ImGui.Text("Models");
            ImGui.Indent();
            foreach (string file in Directory.EnumerateFiles(Path.Join(EditorManager.projectLocation, "Assets", "Models")))
            {
                if (ImGui.Selectable(Path.GetFileName(file)))
                {
                    if (file.EndsWith(".gltf"))
                    {
                        GameObject model = new GameObject(file.Split(".")[0]);

                        model.AddComponent(new MeshRenderer(file, "Shaders/default.vert", "Shaders/default.frag", false));

                        SceneManager.currentScene.AddObject(model);

                        Output.SetOutput("Loaded model " + Path.GetFileName(file), 5000);
                    }
                }
            }
            ImGui.Unindent();

            ImGui.Unindent();

            if (ImGui.Selectable("Code")) {
                switch (EditorManager.editorType)
                {
                    case EditorType.VSCode:
                        Output.SetOutput("Opening code folder in VSCode...", 2000);

                        string codePath = Path.Join(Environment.GetEnvironmentVariable("LocalAppData"), "Programs", "Microsoft VS Code", "Code.exe");

                        ProcessStartInfo info = new ProcessStartInfo()
                        {
                            Arguments = Path.Join(EditorManager.projectLocation, "CSharp").Replace("/", "\\"),
                            FileName = codePath,
                            WorkingDirectory = Environment.CurrentDirectory
                        };

                        Process.Start(info);

                        break;
                }
            }

            ImGui.Indent();

            foreach (string file in Directory.GetFiles(Path.Join(EditorManager.projectLocation, "CSharp")))
            {
                if (File.Exists(file))
                {
                    if (ImGui.Selectable(Path.GetFileName(file)))
                    {
                        switch (EditorManager.editorType)
                        {
                            case EditorType.VSCode:
                                Output.SetOutput($"Opening {Path.GetFileName(file)} in VSCode...", 2000);
                                string codePath = Path.Join(Environment.GetEnvironmentVariable("LocalAppData"), "Programs", "Microsoft VS Code", "Code.exe");

                                ProcessStartInfo info = new ProcessStartInfo()
                                {
                                    Arguments = file,
                                    FileName = codePath,
                                    WorkingDirectory = Environment.CurrentDirectory
                                };

                                Process.Start(info);
                                break;
                        }
                    }
                }
            }
        }
    }
}
