using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine.Components
{
    public class LightReciever : Component
    {
        Light[] lights;

        public LightReciever()
        {
            lights = new Light[10];
        }

        public override void Update(double dt)
        {
            lights = SceneManager.currentScene.FindObjectsOfType<Light>();
            MeshRenderer[] meshRenderers = SceneManager.currentScene.FindObjectsOfType<MeshRenderer>();

            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.shaderProgram.Activate();
                int i = 0;
                foreach (Light light in lights)
                {
                    renderer.shaderProgram.SetUniform("lights[" + i.ToString() + "].position", light.transform.position);
                    renderer.shaderProgram.SetUniform("lights[" + i.ToString() + "].color", light.color);
                    i++;
                }
            }
        }
    }
}
