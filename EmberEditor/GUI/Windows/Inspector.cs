using EmberEngine;
using EmberEngine.GUI;
using ImGuiNET;
using Silk.NET.Maths;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AuroraEditor.GUI.Windows
{
    public class StateDict<TValue>
    {
        string[] keys;
        TValue[] values;

        int currentIndex = 0;

        public StateDict()
        {
            values = new TValue[128];
            keys = new string[128];
        }

        public void Add(string name, TValue value)
        {
            keys[currentIndex] = name;
            values[currentIndex] = value;

            currentIndex++;
        }

        public TValue this[string name]
        {
            get => throw new NotImplementedException();
        }

        public TValue Get(string name)
        {
            return values[keys.ToList().IndexOf(name)];
        }
    }

    public class Inspector : GUIWindow
    {
        public StateDict<byte[]> stringValues;
        public StateDict<bool> boolValues;

        public static List<Assembly> componentAssemblies;

        bool showAddComponentModal;
        public Inspector()
        {
            componentAssemblies = new List<Assembly>();
            stringValues = new StateDict<byte[]>();
            boolValues = new StateDict<bool>();
            windowName = "inspector";
            showAddComponentModal = false;
        }

        public void InputVec3(string label, ref Vector3 output)
        {
            ImGui.Text(label);

            ImGui.Indent();
            ImGui.PushID("X" + label);
            ImGui.InputFloat("X", ref output.X);
            ImGui.PopID();

            ImGui.PushID("Y" + label);
            ImGui.InputFloat("Y", ref output.Y);
            ImGui.PopID();

            ImGui.PushID("Z" + label);
            ImGui.InputFloat("Z", ref output.Z);
            ImGui.PopID();
            ImGui.Unindent();

            ImGui.Text(" ");
        }

        public unsafe override void RenderContent()
        {
            if (EditorManager.selectedObject == null)
            {
                return;
            }
            // Display the name
            ImGui.InputText("Name", ref EditorManager.selectedObject.name, 128);

            // Display the transform properties
            ImGui.Text("Transform");
            ImGui.Indent();
            InputVec3("Position", ref EditorManager.selectedObject.transform.position);

            InputVec3("Rotation", ref EditorManager.selectedObject.transform.rotation);

            InputVec3("Scale", ref EditorManager.selectedObject.transform.scale);

            ImGui.Unindent();

            foreach (Component component in EditorManager.selectedObject.components)
            {
                ImGui.Text(component.GetType().Name);
                ImGui.Indent();

                foreach (PropertyInfo property in component.GetType().GetProperties())
                {
                    object val = property.GetValue(component);
                    switch (property.PropertyType.Name)
                    {
                        case nameof(String):
                            string stringVal = (string)val;

                            ImGui.InputText(property.Name, ref stringVal, 128);

                            property.SetValue(component, stringVal);

                            break;

                        case nameof(Boolean):
                            bool boolVal = (bool)val;

                            ImGui.Checkbox(property.Name, ref boolVal);

                            property.SetValue(component, boolVal);

                            break;

                        case nameof(Single):
                            float floatVal = (float)val;

                            ImGui.InputFloat(property.Name, ref floatVal);

                            property.SetValue(component, floatVal);

                            break;

                        case nameof(Vector3):
                            Vector3 vec3Val = (Vector3)val;

                            InputVec3(property.Name, ref vec3Val);

                            property.SetValue(component, vec3Val);
                            break;
                    }
                }

                ImGui.Unindent();
            }

            if (ImGui.Button("Add Component"))
            {
                showAddComponentModal = true;
            }

            if (ImGui.Button("Delete"))
            {
                SceneManager.currentScene.objects.Remove(EditorManager.selectedObject);

                EditorManager.selectedObject = null;
            }

            if (showAddComponentModal)
            {
                ImGui.End();
                ImGui.Begin("Add Component", ref showAddComponentModal);

                ImGui.Text("Select Component to add");

                AddComponentClassesFromAssembly(Assembly.GetAssembly(typeof(Component)));
                foreach (Assembly asm in componentAssemblies)
                {
                    AddComponentClassesFromAssembly(asm);
                }
            }
        }

        public void AddComponentClassesFromAssembly(Assembly asm)
        {
            foreach (TypeInfo type in asm.DefinedTypes)
            {
                if (type.BaseType == typeof(Component))
                {
                    if (ImGui.Selectable(type.Name))
                    {
                        Component component = Activator.CreateInstance(type) as Component;

                        component.transform = EditorManager.selectedObject.transform;
                        component.gameObject = EditorManager.selectedObject;

                        EditorManager.selectedObject.AddComponent(component);

                        showAddComponentModal = false;
                    }
                }
            }
        }
    }
}
