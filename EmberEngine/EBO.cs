using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Numerics;

namespace EmberEngine
{
    public class EBO
    {
        public uint id;
        GL _gl;

        public unsafe EBO(uint[] indices)
        {
            _gl = Globals.application._gl;

            id = _gl.GenBuffer();

            // bind EBO
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, id);

            // set indices data in ebo
            fixed (uint* buf = indices)
                _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);
        }

        public void Bind()
        {
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, id);
        }

        public void Unbind()
        {
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }

        public void Delete()
        {
            _gl.DeleteBuffers(1, id);
        }
    }
}
