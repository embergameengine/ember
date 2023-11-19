using ImGuiNET;
using System.Numerics;

namespace EmberEngine.GUI
{
    public class GUIWindow
    {
        private bool open = true;
        private string guid;

        public string windowName = "GUIWindow";
        public ImGuiWindowFlags flags = ImGuiWindowFlags.None;

        public bool fixedPosition = false;
        public bool fixedSize = false;

        public Vector2 windowPosition;
        public Vector2 windowSize;

        public virtual void Update(double dt) { }

        public virtual void RenderContent() { }

        public virtual void OnOpen() { }

        public virtual void OnClose() { }

        public void Open()
        {
            open = true;
            OnOpen();
        }

        public void Close()
        {
            OnClose();
            open = false;
        }

        public void Render()
        {
            if (open == true)
            {
                if (guid == null)
                {
                    guid = Guid.NewGuid().ToString();
                }

                if (fixedPosition)
                {
                    ImGui.SetNextWindowPos(windowPosition);
                }

                if (fixedSize)
                {
                    ImGui.SetNextWindowSize(windowSize);
                }

                ImGui.PushID(guid);

                ImGui.Begin(windowName, ref open, flags);

                ImGui.PopID();

                RenderContent();

                ImGui.PushID(guid);

                ImGui.End();

                ImGui.PopID();
            }
            
        }
    }
}
