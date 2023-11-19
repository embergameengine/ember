using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Numerics;

namespace EmberEngine
{
    public class VAO
    {
        public uint id;
        GL _gl;

        public VAO()
        {
            _gl = Globals.application._gl;

            id = _gl.GenVertexArrays(1);
        }

        public unsafe void LinkAttrib(VBO vbo, uint layout, int numComponents, GLEnum type, uint stride, void* offset)
        {
            vbo.Bind();

            _gl.VertexAttribPointer(layout, numComponents, type, false, stride, offset);
            _gl.EnableVertexAttribArray(layout);

            vbo.Unbind();
        }

        public void Bind()
        {
            _gl.BindVertexArray(id);
        }

        public void Unbind()
        {
            _gl.BindVertexArray(0);
        }

        public void Delete()
        {

        }
    }
}
