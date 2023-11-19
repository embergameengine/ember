using EmberEngine;

namespace AuroraEditor.Components
{
    public class SpinningText : Component
    {
        public override void Update(double dt)
        {
            transform.rotation.Y += 0.2f;

            if (transform.rotation.Y > 360f)
            {
                transform.rotation.Y = 0f;
            }
        }
    }
}
