using ImGuiNET;
using EmberEngine;

namespace AuroraEditor
{
    public enum EditorType
    {
        VSCode,
        VisualStudio
    }

    public static class EditorManager
    {
        public static GameObject selectedObject;
        public static ImFontPtr font;

        public static string projectLocation;

        public static EditorType editorType;
    }
}
