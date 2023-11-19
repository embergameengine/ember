using Jitter.Collision.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine.Components
{
    public class BoxCollider : Component
    {
        public Shape shape;

        public override void Load()
        {
            shape = new BoxShape(transform.scale.X, transform.scale.Y, transform.scale.Z);
        }

        public override void Update(double dt)
        {
            shape = new BoxShape(transform.scale.X, transform.scale.Y, transform.scale.Z);
        }
    }
}
