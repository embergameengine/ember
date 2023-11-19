using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EmberEngine.Components
{
    public class MeshRenderer : Component
    {
        public string fragmentShader, vertexShader;
        [JsonIgnore]
        public Shader shaderProgram;
        private VBO vbo;
        private VAO vao;
        private EBO ebo;
        public bool useTextures;
        public bool useEmbededTextures;
        private Texture texture;
        public string texturePath { get; set; }
        internal Mesh mesh;
        public string meshPath { get; set; }

        private Task<Mesh> meshLoadTask; 

        public MeshRenderer(string meshPath, string vertexShader, string fragmentShader, bool useEmbededTextures) 
        {
            texturePath = "";
            this.vertexShader = vertexShader;
            this.fragmentShader = fragmentShader;
            meshLoadTask = Mesh.Load(meshPath);
            

            useTextures = false;
            this.useEmbededTextures = useEmbededTextures;

            this.meshPath = meshPath;
        }

        [JsonConstructor]
        public MeshRenderer()
        {
            
        }

        public override void LoadFromScene()
        {
            //Console.WriteLine("Hi");
            meshLoadTask = Mesh.Load(meshPath);
            //Console.WriteLine("Hi1");
            shaderProgram = new Shader(vertexShader, fragmentShader);

            texturePath = "";
        }

        public override unsafe void Load()
        {
            //Console.WriteLine(transform);
            mesh = meshLoadTask.GetAwaiter().GetResult();

            shaderProgram = new Shader(vertexShader, fragmentShader);

            // create and bind vao
            vao = new VAO();
            vao.Bind();

            // create vbo and ebo
            VBO vbo = new VBO(mesh.vertices.ToArray());
            EBO ebo = new EBO(mesh.indices.ToArray());

            // link vbo data to vao
            /*
            vao.LinkAttrib(vbo, 0, 3, GLEnum.Float, (uint)(8 * sizeof(float)), (void*)0);
            vao.LinkAttrib(vbo, 1, 3, GLEnum.Float, (uint)(8 * sizeof(float)), (void*)(3 * sizeof(float)));
            vao.LinkAttrib(vbo, 2, 2, GLEnum.Float, (uint)(8 * sizeof(float)), (void*)(6 * sizeof(float)));
            */
            // vertex coords vec3
            vao.LinkAttrib(vbo, 0, 3, GLEnum.Float, (uint)(9 * sizeof(float)), (void*)0);
            // color vec3
            vao.LinkAttrib(vbo, 1, 3, GLEnum.Float, (uint)(9 * sizeof(float)), (void*)(3 * sizeof(float)));
            // texture coords vec2
            vao.LinkAttrib(vbo, 2, 2, GLEnum.Float, (uint)(11 * sizeof(float)), (void*)(6 * sizeof(float)));

            // unbind vao, vbo and ebo
            vao.Unbind();
            vbo.Unbind();
            ebo.Unbind();

            if (useTextures)
            {
                // load texture
                // 24 bpp rgb image
                texture = new Texture("Textures/" + texturePath, GLEnum.Texture0, PixelType.UnsignedByte);
                texture.TexUnit(shaderProgram, "diffuse0", 0);
                //texture.Unbind();
            }
            if (useEmbededTextures)
            {
                int index = 0;
                foreach (Texture texture in mesh.textures)
                {
                    texture.TexUnit(shaderProgram, "diffuse" + index.ToString(), (uint)index);
                    index++;
                }
            }
        }

        public override unsafe void Render(ProjectionMatrices matrices, TransformationMatrices transformationMatrices, GL _gl)
        {
            shaderProgram.Activate();

            shaderProgram.SetUniform("model", matrices.model);
            shaderProgram.SetUniform("view", matrices.view);
            shaderProgram.SetUniform("proj", matrices.projection);

            transformationMatrices.ApplyToShader(shaderProgram);

            if (useTextures)
            {
                // bind texture
                texture.Bind();
            }

            if (useEmbededTextures)
            {
                mesh.textures[0].Bind();
            }

            // bind vao
            vao.Bind();
            // draw triangle
            //Console.WriteLine("hi");
            _gl.DrawElements(GLEnum.Triangles, (uint)mesh.indices.Count, DrawElementsType.UnsignedInt, null);

            if (useTextures)
            {
                // unbind texture
                texture.Unbind();
            }

            if (useEmbededTextures)
            {
                mesh.textures[0].Unbind();
            }
        }

        public override void Update(double dt)
        {
            //Console.WriteLine(test);
        }
    }
}
