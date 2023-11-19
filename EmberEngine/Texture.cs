using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Numerics;
using StbImageSharp;

namespace EmberEngine
{
    public class Texture
    {
        GL _gl;
        public uint id;
        GLEnum type;

        public int width, height;

        public unsafe Texture(string imagePath, GLEnum slot, PixelType pixelType)
        {
            this.type = GLEnum.Texture2D;
            GLEnum format = GLEnum.Rgba;
            InternalFormat internalFormat = InternalFormat.Rgba;
            PixelFormat pixelFormat = PixelFormat.Rgba;

            

            _gl = Globals.application._gl;

            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult result = ImageResult.FromMemory(File.ReadAllBytes(imagePath), ColorComponents.RedGreenBlueAlpha);

            width = result.Width;
            height = result.Height;

            switch (result.Comp)
            {
                case ColorComponents.RedGreenBlueAlpha:
                    format = GLEnum.Rgba;
                    internalFormat = InternalFormat.Rgba;
                    pixelFormat = PixelFormat.Rgba;
                    break;

                case ColorComponents.RedGreenBlue:
                    format = GLEnum.Rgb;
                    internalFormat = InternalFormat.Rgb;
                    pixelFormat = PixelFormat.Rgb;
                    break;
            }
            
            id = _gl.GenTextures(1);

            _gl.ActiveTexture(slot);
            _gl.BindTexture(type, id);
            _gl.TexParameter(type, GLEnum.TextureMinFilter, (float)GLEnum.Nearest);
            _gl.TexParameter(type, GLEnum.TextureMagFilter, (float)GLEnum.Nearest);

            _gl.TexParameter(type, GLEnum.TextureWrapS, (float)GLEnum.Repeat);
            _gl.TexParameter(type, GLEnum.TextureWrapT, (float)GLEnum.Repeat);
            fixed (byte* ptr = result.Data)
            {
                _gl.TexImage2D(type, 0, internalFormat, (uint)result.Width,
                    (uint)result.Height, 0, pixelFormat, pixelType, ptr);
            }
            _gl.GenerateMipmap(type);

            _gl.BindTexture(type, 0);
        }

        public unsafe Texture(ImageResult image, GLEnum type, GLEnum slot, InternalFormat internalFormat, PixelType pixelType, PixelFormat pixelFormat)
        {
            this.type = type;
            _gl = Globals.application._gl;

            id = _gl.GenTextures(1);

            _gl.ActiveTexture(slot);
            _gl.BindTexture(type, id);
            _gl.TexParameter(type, GLEnum.TextureMinFilter, (float)GLEnum.Nearest);
            _gl.TexParameter(type, GLEnum.TextureMagFilter, (float)GLEnum.Nearest);

            _gl.TexParameter(type, GLEnum.TextureWrapS, (float)GLEnum.Repeat);
            _gl.TexParameter(type, GLEnum.TextureWrapT, (float)GLEnum.Repeat);

            fixed (byte* ptr = image.Data)
            {
                _gl.TexImage2D(type, 0, internalFormat, (uint)Math.Sqrt(image.Data.Length / 4), (uint)Math.Sqrt(image.Data.Length / 4), 0, pixelFormat, pixelType, ptr);
            }
            _gl.GenerateMipmap(type);

            _gl.BindTexture(type, 0);
        }

        public unsafe Texture(byte[] image, GLEnum type, GLEnum slot, InternalFormat internalFormat, PixelType pixelType, PixelFormat pixelFormat)
        {
            this.type = type;
            _gl = Globals.application._gl;

            id = _gl.GenTextures(1);

            _gl.ActiveTexture(slot);
            _gl.BindTexture(type, id);
            _gl.TexParameter(type, GLEnum.TextureMinFilter, (float)GLEnum.Nearest);
            _gl.TexParameter(type, GLEnum.TextureMagFilter, (float)GLEnum.Nearest);

            _gl.TexParameter(type, GLEnum.TextureWrapS, (float)GLEnum.Repeat);
            _gl.TexParameter(type, GLEnum.TextureWrapT, (float)GLEnum.Repeat);

            fixed (byte* ptr = image)
            {
                _gl.TexImage2D(type, 0, internalFormat, (uint)Math.Sqrt(image.Length / 4), (uint)Math.Sqrt(image.Length / 4), 0, pixelFormat, pixelType, ptr);
            }
            _gl.GenerateMipmap(type);

            _gl.BindTexture(type, 0);
        }

        public void TexUnit(Shader shader, string uniform, uint unit)
        {
            int texUni = _gl.GetUniformLocation(shader.id, uniform);

            shader.Activate();
            
            _gl.Uniform1(texUni, unit);
        }

        public void Bind()
        {
            _gl.BindTexture(type, id);
        }

        public void Unbind()
        {
            _gl.BindTexture(type, 0);
        }

        public void Delete()
        {
            _gl.DeleteTextures(1, id);
        }
    }
}