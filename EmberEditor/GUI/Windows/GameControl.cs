using ImGuiNET;
using EmberEngine.GUI;
using System.Diagnostics;
using System.Reflection;

namespace AuroraEditor.GUI.Windows
{
    public class GameControl : GUIWindow
    {
        Process gameProcess; 

        public GameControl()
        {
            windowName = "game control";
        }

        public override void RenderContent()
        {
            if (ImGui.Button("Play"))
            {
                Output.SetOutput($"Compiling project...", 100);

                ProcessStartInfo buildInfo = new ProcessStartInfo();
                buildInfo.WorkingDirectory = EditorManager.projectLocation + "\\CSharp\\";
                buildInfo.Arguments = "/c .\\build.bat";
                buildInfo.FileName = "C:\\Windows\\System32\\cmd.exe";

                Process.Start(buildInfo);

                Output.SetOutput($"Starting game...", 100);


                ProcessStartInfo runInfo = new ProcessStartInfo();
                runInfo.WorkingDirectory = EditorManager.projectLocation + "\\CSharp\\";
                runInfo.Arguments = "/c .\\run.bat";
                runInfo.FileName = "C:\\Windows\\System32\\cmd.exe";

                gameProcess = Process.Start(runInfo);
            }

            if (gameProcess != null)
            {
                if (ImGui.Button("Stop"))
                {
                    gameProcess.Kill();
                }
            }

            if (ImGui.Button("Reload Components"))
            {
                Output.SetOutput($"Compiling project...", 100);

                ProcessStartInfo buildInfo = new ProcessStartInfo();
                buildInfo.WorkingDirectory = EditorManager.projectLocation + "\\CSharp\\";
                buildInfo.Arguments = "/c .\\build.bat";
                buildInfo.FileName = "C:\\Windows\\System32\\cmd.exe";

                Process.Start(buildInfo);

                Inspector.componentAssemblies = new List<Assembly>();

                Output.SetOutput($"Loading assemblies" +
                    $"...", 100);
                Inspector.componentAssemblies.Add(Assembly.Load(File.ReadAllBytes(EditorManager.projectLocation + "\\CSharp\\bin\\Debug\\net6.0\\CSharp.dll")));
            }
        }
    }
}
