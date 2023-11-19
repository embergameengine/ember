using ImGuiNET;
using EmberEngine.GUI;
using EmberEngine;
using EmberEngine.Components;

namespace AuroraEditor.GUI.Windows
{
    public class SceneConfig : GUIWindow
    {
        public SceneConfig()
        {
            windowName = "scene configuration";
        }

        public override void RenderContent()
        {
            ImGui.Text("Scene Config");
            ImGui.InputText("Name", ref SceneManager.currentScene.name, 128);
        }
    }
}
