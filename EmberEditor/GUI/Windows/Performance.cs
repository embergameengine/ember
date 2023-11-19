using ImGuiNET;
using EmberEngine.GUI;
using EmberEngine;
using EmberEngine.Components;

namespace AuroraEditor.GUI.Windows
{
    public class Performance : GUIWindow
    {

        double fps;

        public Performance()
        {
            windowName = "performance";
            fps = 0;
        }

        public override void RenderContent()
        {
            ImGui.Text("Performance");
            ImGui.Text("FPS: " + fps.ToString());
        }

        public override void Update(double dt)
        {
            fps = 1 / dt;
        }
    }
}
