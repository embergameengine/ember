using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Drawing;
using Silk.NET.Input;
using System.Numerics;
using Newtonsoft.Json;
using System.Linq;

namespace EmberEngine
{
    public class TransformationMatrices
    {
        public Matrix4x4 rotationMatrix;
        public Vector3 position;
        public Vector3 scale;

        public TransformationMatrices(Transform transform)
        {
            rotationMatrix = Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(transform.rotation.X)) * Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(transform.rotation.Y)) * Matrix4x4.CreateRotationZ(MathHelper.DegreesToRadians(transform.rotation.Z));
            position = transform.position;
            scale = transform.scale;
        }

        public void ApplyToShader(Shader shader)
        {
            shader.SetUniform("rotationMat", rotationMatrix);

            shader.SetUniform("objPosition", position);
            shader.SetUniform("objScale", scale);
        }
    }

    public class GameObject
    {
        [JsonIgnore]
        internal GL _gl;
        public List<Component> components;

        public string name;
        public Transform transform { get; set; }

        public void AddComponent(Component component)
        {
            components.Add(component);

            component._gl = _gl;
            component.transform = transform;
            component.gameObject = this;
            component.Load();
        }

        public void AddComponent(Component component, bool a)
        {
            components.Add(component);

            component._gl = _gl;
            component.transform = transform;

            //component.Load();
        }

        public GameObject(string name)
        {
            components = new List<Component>();

            _gl = Globals.application._gl;
            this.name = name;
            transform = new Transform();
        }

        public GameObject()
        {

        }

        public bool HasComponent<T>() where T : Component
        {
            bool containsSubclass = components.Any(c => c is T);

            return containsSubclass;
        }

        public T GetComponent<T>() where T : Component
        {
            List<Type> componentTypes = components.Select((c) =>
            {
                return c.GetType();
            }).ToList();

            if (componentTypes.Contains(typeof(T)))
            {
                return (T)components[componentTypes.IndexOf(typeof(T))];
            }
            else
            {
                return null;
            }
        }

        public void Update(double dt)
        {
            foreach (Component component in components)
            {
                component.Update(dt);
            }
        }

        public unsafe void Render(ProjectionMatrices matrices)
        {
            TransformationMatrices transformationMatrices = new TransformationMatrices(transform);
            foreach (Component component in components)
            {
                component.Render(matrices, transformationMatrices, _gl);
            }

            
        }
    }
}
