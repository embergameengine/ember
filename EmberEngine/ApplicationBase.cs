using Silk.NET.Core;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System.Numerics;

namespace EmberEngine
{
    public class ApplicationBase
    {
        internal IWindow _window;
        internal GL _gl;
        internal IInputContext _input;
        internal IKeyboard _keyboard;
        internal ImGuiController _controller;

        public Vector2 size { get { return new Vector2(_window.Size.X, _window.Size.Y); } }
        public Camera camera;
        public bool useFreecam = true;

        public bool useFont;
        private int imguiFontSize;

        public event Action OnLoad;
        public event Action OnUpdate;

        internal bool freeze = false;

        public ApplicationBase(Vector2 size, string title, bool freeze)
        {
            DisableConsoleQuickEdit.Go();

            WindowOptions options = WindowOptions.Default;

            options.Size = new Vector2D<int>((int)size.X, (int)size.Y);
            options.Title = title;

            _window = Window.Create(options);

            _window.Render += Render;
            _window.Update += UpdateInternal;
            _window.Load += Load;
            _window.Resize += Resize;

            SceneManager.Init();

            Globals.application = this;

            AudioManager.Init();

            this.freeze = freeze;
            this.useFont = useFont;
        }

        public ApplicationBase(Vector2 size, string title)
        {
            DisableConsoleQuickEdit.Go();

            WindowOptions options = WindowOptions.Default;

            options.Size = new Vector2D<int>((int)size.X, (int)size.Y);
            options.Title = title;

            _window = Window.Create(options);

            _window.Render += Render;
            _window.Update += UpdateInternal;
            _window.Load += Load;
            _window.Resize += Resize;

            //RawImage icon = new RawImage(512, 512, new Memory<byte>(File.ReadAllBytes("icon.png")));

            //_window.SetWindowIcon(ref icon);

            SceneManager.Init();

            Globals.application = this;

            AudioManager.Init();

            freeze = false;
        }

        private void Resize(Vector2D<int> obj)
        {
            _gl.Viewport(0, 0, (uint)_window.Size.X, (uint)_window.Size.Y);
            camera.settings.aspectRatio = _window.Size.X / _window.Size.Y;
        }

        private void Load()
        {
            camera = new Camera(new Vector3(0, 0, 3), _window);

            _input = _window.CreateInput();

            foreach (IKeyboard keyboard in _input.Keyboards)
            {
                _keyboard = keyboard;
            }

            Input.keyboard = _keyboard;

            _gl = GL.GetApi(_window);

            _gl.Viewport(0, 0, (uint)_window.Size.X, (uint)_window.Size.Y);
            _gl.Enable(EnableCap.DepthTest);
            _gl.DepthFunc(DepthFunction.Lequal);

            _gl.Enable(EnableCap.Texture2D);

            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Fonts", "uiFont.ttf")) != null)
            {
                ImGuiFontConfig config = new ImGuiFontConfig(Path.Combine(Environment.CurrentDirectory, "Fonts", "uiFont.ttf"), 15);

                _controller = new ImGuiController(_gl, _window, _input, config);
            } else
            {
                _controller = new ImGuiController(_gl, _window, _input);
            }

            SceneManager.AddScene(new Scene("UnloadedScene", new Vector4(1f, 0f, 1f, 1f)));
            SceneManager.LoadScene("UnloadedScene");

            OnLoad?.Invoke();
        }

        private void UpdateInternal(double dt)
        {
            if (!freeze)
            {
                SceneManager.Update(dt);
            } else
            {
                if (SceneManager.currentScene.GetCamera() != null)
                {
                    SceneManager.currentScene.GetCamera().Update(dt);
                }
            }

            _controller.Update((float)dt);

            Update(dt);
            OnUpdate?.Invoke();
        }

        public virtual void Update(double dt) { }

        public virtual void RenderUI()
        {

        }

        private void Render(double dt)
        {
            SceneManager.Render();

            RenderUI();

            _controller.Render();
        }

        public void Run()
        {
            _window.Run();
        }

        public void Exit()
        {
            _window.Close();
        }
    }
}
