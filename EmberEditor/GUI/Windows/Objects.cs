using ImGuiNET;
using System.Numerics;
using EmberEngine.GUI;
using EmberEngine;
using AuroraEditor;

namespace AuroraEditor.GUI.Windows
{
    public class Objects : GUIWindow
    {
        public Objects()
        {
            windowName = "objects";
        }

        public override void RenderContent()
        {
            foreach (GameObject obj in SceneManager.currentScene.objects)
            {
                if (EditorManager.selectedObject != null)
                {
                    if (ImGui.Selectable(obj.name))
                    {
                        EditorManager.selectedObject = obj;
                    }
                }
                else
                {
                    if (EditorManager.selectedObject == obj)
                    {
                        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.01f, 1f, 0f, 1.0f));

                        if (ImGui.Selectable(obj.name))
                        {
                            EditorManager.selectedObject = obj;
                        }

                        ImGui.PopStyleColor();
                    }
                    else
                    {
                        if (ImGui.Selectable(obj.name))
                        {
                            EditorManager.selectedObject = obj;
                        }
                    }
                }
            }

            if (ImGui.Button("Create Object"))
            {
                GameObject newObject = new GameObject("New GameObject");

                SceneManager.currentScene.AddObject(newObject);
            }
        }
    }
}