using System.Numerics;
using SharpGLTF.Schema2;
using SharpGLTF.Memory;
using Silk.NET.OpenGL;
using StbImageSharp;
using System.Text.Json;

namespace EmberEngine
{
    [Serializable]
    public class Mesh
    {
        public List<float> vertices { get; set; }
        public List<uint> indices { get; set; }
        public List<Texture> textures;
        public List<List<byte>> cacheTextures { get; set; }

        GL _gl;

        public static async Task<Mesh> Load(string path)
        {
            /*
            Mesh glmesh = new Mesh();

            ModelRoot model = ModelRoot.Load(path);

            List<float> vertices = new List<float>();
            List<uint> indices = new List<uint>();

            foreach (SharpGLTF.Schema2.Mesh mesh in model.LogicalMeshes)
            {
                foreach (MeshPrimitive primitive in mesh.Primitives)
                {
                    IList<Vector2> texCoords = null;
                    if (primitive.VertexAccessors.ContainsKey("TEXCOORD_0"))
                    {
                        var accessor = primitive.VertexAccessors["TEXCOORD_0"];
                        texCoords = accessor.AsVector2Array();
                    }

                    if (primitive.VertexAccessors.ContainsKey("POSITION"))
                    {
                        var accessor = primitive.VertexAccessors["POSITION"];
                        var data = accessor.AsVector3Array();

                        foreach (var vertex in data)
                        {
                            vertices.Add(vertex.X);
                            vertices.Add(vertex.Y);
                            vertices.Add(vertex.Z);

                            vertices.Add(1f);
                            vertices.Add(1f);
                            vertices.Add(1f);
                            vertices.Add(1f);

                            if (texCoords != null)
                            {
                                ////Console.WriteLine("Tex coords found");
                                var texCoord = texCoords[data.IndexOf(vertex)];

                                ////Console.WriteLine(texCoord);
                                
                                vertices.Add(texCoord.X);
                                vertices.Add(texCoord.Y);

                            }
                            else
                            {
                                vertices.Add(1f);
                                vertices.Add(1f);
                            }
                            
                        }
                    }

                    if (primitive.IndexAccessor != null)
                    {
                        Accessor accessor = primitive.IndexAccessor;
                        IntegerArray data = accessor.AsIndicesArray();

                        foreach (uint index in data)
                        {
                            indices.Add(index);
                        }
                    }
                }
                
            }

            List<Texture> textures = new List<Texture>();
            List<List<byte>> cacheTextures = new List<List<byte>>();

            foreach (Image imageData in model.LogicalImages)
            {
                //Console.WriteLine("Texture" + imageData.Name);
                // Create a texture from the image data
                ImageResult image = ImageResult.FromMemory(imageData.Content.Content.ToArray());

                Texture texture = new Texture(image, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Rgba);
                textures.Add(texture);

                cacheTextures.Add(image.Data.ToList());
            }

            glmesh.vertices = vertices;
            glmesh.indices = indices;
            glmesh.textures = textures;
            glmesh.cacheTextures = cacheTextures;
            glmesh._gl = Globals.application._gl;

            ////Console.WriteLine(vertices.Count / 9);

            return glmesh;

            */
            Mesh glmesh = new Mesh();

            ModelRoot model = await Task.Run(() => ModelRoot.Load(path));

            List<float> vertices = new List<float>();
            List<uint> indices = new List<uint>();

            foreach (SharpGLTF.Schema2.Mesh mesh in model.LogicalMeshes)
            {
                foreach (MeshPrimitive primitive in mesh.Primitives)
                {
                    IList<Vector2> texCoords = null;
                    if (primitive.VertexAccessors.ContainsKey("TEXCOORD_0"))
                    {
                        var accessor = primitive.VertexAccessors["TEXCOORD_0"];
                        texCoords = accessor.AsVector2Array();
                    }

                    if (primitive.VertexAccessors.ContainsKey("POSITION"))
                    {
                        var accessor = primitive.VertexAccessors["POSITION"];
                        var data = accessor.AsVector3Array();

                        foreach (var vertex in data)
                        {
                            vertices.Add(vertex.X);
                            vertices.Add(vertex.Y);
                            vertices.Add(vertex.Z);

                            vertices.Add(1f);
                            vertices.Add(1f);
                            vertices.Add(1f);
                            vertices.Add(1f);

                            if (texCoords != null)
                            {
                                var texCoord = texCoords[data.IndexOf(vertex)];
                                vertices.Add(texCoord.X);
                                vertices.Add(texCoord.Y);
                            }
                            else
                            {
                                vertices.Add(1f);
                                vertices.Add(1f);
                            }
                        }
                    }

                    if (primitive.IndexAccessor != null)
                    {
                        Accessor accessor = primitive.IndexAccessor;
                        IntegerArray data = accessor.AsIndicesArray();

                        foreach (uint index in data)
                        {
                            indices.Add(index);
                        }
                    }
                }
            }

            List<Texture> textures = new List<Texture>();
            List<List<byte>> cacheTextures = new List<List<byte>>();

            foreach (Image imageData in model.LogicalImages)
            {
                ImageResult image = ImageResult.FromMemory(imageData.Content.Content.ToArray());

                Texture texture = new Texture(image, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Rgba);
                textures.Add(texture);

                cacheTextures.Add(image.Data.ToList());
            }

            glmesh.vertices = vertices;
            glmesh.indices = indices;
            glmesh.textures = textures;
            glmesh.cacheTextures = cacheTextures;
            glmesh._gl = Globals.application._gl;

            return glmesh;
        }
        public static Mesh Load(string path, float scaleFactor)
        {
#if !DEBUG
            if (File.Exists(Path.Combine("Models", path) + ".cache"))
            {
                GLMesh mesh = JsonSerializer.Deserialize<GLMesh>(File.ReadAllText(Path.Combine("Models", path) + ".cache"));

                mesh.LoadFromCache();

                return mesh;
            }
#endif

            Mesh glmesh = new Mesh();

            ModelRoot model = ModelRoot.Load(Path.Combine("Models", path));

            List<float> vertices = new List<float>();
            List<uint> indices = new List<uint>();

            List<Texture> textures = new List<Texture>();
            List<List<byte>> cacheTextures = new List<List<byte>>();

            foreach (Image imageData in model.LogicalImages)
            {
                //Console.WriteLine("Texture" + imageData.Name);
                // Create a texture from the image data

                ImageResult image = ImageResult.FromMemory(imageData.Content.Content.ToArray());
                Texture texture;

                switch (image.Comp)
                {
                    case ColorComponents.RedGreenBlueAlpha:
                        texture = new Texture(image, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Rgba);
                        textures.Add(texture);
                        break;

                    case ColorComponents.RedGreenBlue:
                        texture = new Texture(image, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Rgb);
                        textures.Add(texture);
                        break;

                    case ColorComponents.Grey:
                        texture = new Texture(image, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Red);
                        textures.Add(texture);
                        break;
                }

                cacheTextures.Add(image.Data.ToList());
                cacheTextures.Add(new List<byte>() { (byte)image.Comp });
            }

            foreach (SharpGLTF.Schema2.Mesh mesh in model.LogicalMeshes)
            {
                foreach (MeshPrimitive primitive in mesh.Primitives)
                {
                    IList<Vector2> texCoords = null;
                    if (primitive.VertexAccessors.ContainsKey("TEXCOORD_0"))
                    {
                        var accessor = primitive.VertexAccessors["TEXCOORD_0"];
                        texCoords = accessor.AsVector2Array();
                    }

                    if (primitive.VertexAccessors.ContainsKey("POSITION"))
                    {
                        var accessor = primitive.VertexAccessors["POSITION"];
                        var data = accessor.AsVector3Array();

                        foreach (var vertex in data)
                        {
                            vertices.Add(vertex.X / scaleFactor);
                            vertices.Add(vertex.Y / scaleFactor);
                            vertices.Add(vertex.Z / scaleFactor);

                            vertices.Add(1f);
                            vertices.Add(1f);
                            vertices.Add(1f);
                            vertices.Add(1f);

                            if (texCoords != null)
                            {
                                ////Console.WriteLine("Tex coords found");
                                var texCoord = texCoords[data.IndexOf(vertex)];

                                ////Console.WriteLine(texCoord);

                                vertices.Add(texCoord.X);
                                vertices.Add(1f - texCoord.Y);

                            }
                            else
                            {
                                vertices.Add(1f);
                                vertices.Add(1f);
                            }

                        }
                    }

                    if (primitive.IndexAccessor != null)
                    {
                        Accessor accessor = primitive.IndexAccessor;
                        IntegerArray data = accessor.AsIndicesArray();

                        foreach (uint index in data)
                        {
                            indices.Add(index);
                        }
                    }
                }
            }

            

            glmesh.vertices = vertices;
            glmesh.indices = indices;
            glmesh.textures = textures;
            glmesh.cacheTextures = cacheTextures;
            glmesh._gl = Globals.application._gl;

            //Console.WriteLine("Caching model " + path);
#if !DEBUG
            File.WriteAllText(Path.Combine("Models", path) + ".cache", JsonSerializer.Serialize<GLMesh>(glmesh));
#endif

            return glmesh;
        }

        public void LoadFromCache()
        {
            for (int i = 0; i < cacheTextures.Count; i++)
            {
                List<byte> imageDataList = cacheTextures[i];
                i++;
                ColorComponents comp = (ColorComponents)cacheTextures[i][0];
                byte[] imageData = imageDataList.ToArray();
                Texture texture;

                switch (comp)
                {
                    case ColorComponents.RedGreenBlueAlpha:
                        texture = new Texture(imageData, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Rgba);
                        textures.Add(texture);
                        break;

                    case ColorComponents.RedGreenBlue:
                        texture = new Texture(imageData, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Rgb);
                        textures.Add(texture);
                        break;

                    case ColorComponents.Grey:
                        texture = new Texture(imageData, GLEnum.Texture2D, GLEnum.Texture0, InternalFormat.Rgba, PixelType.UnsignedByte, PixelFormat.Red);
                        textures.Add(texture);
                        break;
                }
            }
            
        }
    }
}
