using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Numerics;

namespace EmberEngine
{
    public class Shader
    {
        public uint id;
        GL _gl;

        string vertexShader, fragmentShader;

        public unsafe void SetUniform(string name, Matrix4x4 value)
        {
            float* buf = (float*)&value;
            _gl.UniformMatrix4(_gl.GetUniformLocation(id, name), 1, false, buf);
        }

        public unsafe void SetUniform(string name, Vector3 value)
        {
            Vector3 asVec3 = value;
            _gl.Uniform3(_gl.GetUniformLocation(id, name), ref asVec3);
        }

        public unsafe void SetUniform(string name, Vector3[] values)
        {
            int location = _gl.GetUniformLocation(id, name);

            if (location == -1)
            {
                throw new Exception("Invalid uniform");
            }

            double[] doubleValues = new double[3 * values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                doubleValues[3 * i] = values[i].X;
                doubleValues[3 * i + 1] = values[i].Y;
                doubleValues[3 * i + 2] = values[i].Z;
            }

            fixed (double* pDoubleValues = doubleValues)
            {
                _gl.Uniform3(location, (uint)values.Length, pDoubleValues);
            }

        }

        public unsafe void GetUniformVector3Array(string name, int count, double[] values)
        {
            int location = _gl.GetUniformLocation(id, name);

            fixed (double* pValues = values)
            {
                _gl.GetnUniform(id, location, (uint)count, pValues);
            }
        }

        public Shader(string vertFile, string fragFile)
        {
            _gl = Globals.application._gl;

            this.vertexShader = vertFile;
            this.fragmentShader = fragFile;


            Console.WriteLine(this.vertexShader);

            string vertexShaderSource = File.ReadAllText(vertFile);
            string fragmentShaderSource = File.ReadAllText(fragFile);

            // compile vertex shader
            uint vertexShader = _gl.CreateShader(GLEnum.VertexShader);
            _gl.ShaderSource(vertexShader, vertexShaderSource);
            _gl.CompileShader(vertexShader);
            CompileErrors(vertexShader, "VERTEX");

            // compile fragment shader
            uint fragmentShader = _gl.CreateShader(GLEnum.FragmentShader);
            _gl.ShaderSource(fragmentShader, fragmentShaderSource);
            _gl.CompileShader(fragmentShader);
            CompileErrors(fragmentShader, "FRAGMENT");

            // create shader program
            id = _gl.CreateProgram();
            _gl.AttachShader(id, vertexShader);
            _gl.AttachShader(id, fragmentShader);
            _gl.LinkProgram(id);
            CompileErrors(id, "PROGRAM");


            _gl.DeleteShader(vertexShader);
            _gl.DeleteShader(fragmentShader);
        }

        public void Activate()
        {
            _gl.UseProgram(id);
        }

        public void Deactivate()
        {
            _gl.UseProgram(0);
        }

        public void Delete()
        {
            _gl.DeleteProgram(id);
        }

        void CompileErrors(uint shader, string type)
        {
            int hasCompiled;

            if (type != "PROGRAM")
            {
                hasCompiled = _gl.GetShader(shader, GLEnum.CompileStatus);

                if (hasCompiled == 0)
                {
                    string shaderLog = _gl.GetShaderInfoLog(shader);

                    //Console.WriteLine(type + " SHADER COMPILATION ERROR\nInfo Log: " + shaderLog);
                }
            }
        }
    }
}
