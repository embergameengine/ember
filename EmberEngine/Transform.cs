using Silk.NET.Maths;
using System.Numerics;

namespace EmberEngine
{
    public class Transform
    {
        public Vector3 position;
        public Vector3 scale;
        public Vector3 rotation;

        public Transform()
        {
            scale = new Vector3(1, 1, 1);
            position = new Vector3(0, 0, 0);
            rotation = new Vector3(0, 0, 0);
        }
    }
}