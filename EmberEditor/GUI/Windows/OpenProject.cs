using ImGuiNET;
using EmberEngine.GUI;
using EmberEngine;
using EmberEngine.Components;

namespace AuroraEditor.GUI.Windows
{
    public class OpenProject : GUIWindow
    {
        string projectPath = "";
        public OpenProject()
        {
            windowName = "project loader";
        }

        public override void RenderContent()
        {
            ImGui.Text("Project Loader");
            ImGui.InputText("Path", ref projectPath, 128);

            if (ImGui.Button("Load"))
            {
                EditorManager.projectLocation = projectPath;
                SceneManager.LoadScene("UnloadedScene");
                Close();
                Output.SetOutput("Opened project at " + projectPath, 10000);
            }
        }
    }
}
