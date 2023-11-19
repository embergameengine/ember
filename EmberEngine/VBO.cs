using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Numerics;

namespace EmberEngine
{
    public class VBO
    {
        public uint id;
        GL _gl;

        public unsafe VBO(float[] vertices)
        {
            _gl = Globals.application._gl;

            id = _gl.GenBuffers(1);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, id);

            // set data in vbo
            fixed (float* buf = vertices)
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
        }

        public void Bind()
        {
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, id);
        }

        public void Unbind()
        {
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        }

        public void Delete()
        {
            _gl.DeleteBuffers(1, id);
        }
    }
}
