using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine.Components
{
    public enum LightType
    {
        Point,
        Directional,
        Spot
    }

    public class Light : Component
    {
        public LightType lightType { get; set; }

        public float intensity { get; set; }

        public Vector3 color { get; set; }

        public Light()
        {
            lightType = LightType.Point;
            intensity = 1f;
        }
    }
}
