using Newtonsoft.Json;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine
{
    public class Component
    {
        [JsonIgnore]
        public GameObject gameObject;
        public Transform transform;
        [JsonIgnore]
        public GL _gl;

        public virtual void Load() { }

        public virtual void Update(double dt) { }

        public virtual void Render(ProjectionMatrices matrices, TransformationMatrices transformMatrices, GL _gl) { }

        public virtual void LoadFromScene() { }
    }
}
