using Silk.NET.Windowing;
using Silk.NET.Input;
using System.Numerics;

namespace EmberEngine
{
    public class Camera : Component
    {
        public Vector3 target;
        public Vector3 direction;
        public Vector3 right;
        public Vector3 up;
        public CameraSettings settings;

        public float speed = 0.025f;
        public float rotSpeed = 0.75f;

        IWindow _window;

        public bool useFreecam { get; set; }
        

        public Camera(Vector3 position, IWindow window)
        {
            direction = new Vector3(0f, 0f, -1f);
            target = position + direction;
            right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, direction));
            up = Vector3.Cross(direction, right);
            _window = window;

            settings = new CameraSettings(_window);

            useFreecam = true;
        }

        public Camera()
        {
            useFreecam = true;
        }

        public override void Load()
        {
            direction = new Vector3(0f, 0f, -1f);
            target = transform.position + direction;
            right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, direction));
            up = Vector3.Cross(direction, right);
            _window = Globals.application._window;

            settings = new CameraSettings(_window);

            
        }

        public ProjectionMatrices CreateProjectionMatricies()
        {
            target = transform.position + direction;
            right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, direction));
            up = Vector3.Cross(direction, right);

            Matrix4x4 model = Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(0f)) * Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(0f));
            Matrix4x4 view = Matrix4x4.CreateLookAt(transform.position, target, up);

            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(settings.fov), (float)_window.Size.X / (float)_window.Size.Y, settings.nearPlane, settings.farPlane);

            return new ProjectionMatrices(model, view, projection);
        }

        public override void Update(double dt)
        {
            if (useFreecam)
            {
                Console.WriteLine("Test");
                if (Input.GetKey(Key.W))
                {
                    
                    transform.position += speed * direction;
                }
                if (Input.GetKey(Key.A))
                {
                    transform.position += speed * -Vector3.Normalize(Vector3.Cross(direction, up));
                }
                if (Input.GetKey(Key.S))
                {
                    transform.position += speed * -direction;
                }
                if (Input.GetKey(Key.D))
                {
                    transform.position += speed * Vector3.Normalize(Vector3.Cross(direction, up));
                }
                if (Input.GetKey(Key.Q))
                {
                    transform.position += speed * -up;
                }
                if (Input.GetKey(Key.E))
                {
                    transform.position += speed * up;
                }


                float angle = MathF.PI / 180 * rotSpeed; // Convert degrees to radians and scale by rotation speed

                if (Input.GetKey(Key.Down))
                {
                    Vector3 axis = right;
                    Matrix4x4 rotationMatrix = Matrix4x4.CreateFromAxisAngle(axis, angle);
                    direction = Vector3.Transform(direction, rotationMatrix);
                    right = Vector3.Normalize(Vector3.Cross(up, direction));
                }
                if (Input.GetKey(Key.Up))
                {
                    Vector3 axis = -right;
                    Matrix4x4 rotationMatrix = Matrix4x4.CreateFromAxisAngle(axis, angle);
                    direction = Vector3.Transform(direction, rotationMatrix);
                    right = Vector3.Normalize(Vector3.Cross(up, direction));
                }
                if (Input.GetKey(Key.Left))
                {
                    Vector3 axis = up;
                    Matrix4x4 rotationMatrix = Matrix4x4.CreateFromAxisAngle(axis, angle);
                    direction = Vector3.Transform(direction, rotationMatrix);
                    up = Vector3.Cross(direction, right);
                }
                if (Input.GetKey(Key.Right))
                {
                    Vector3 axis = -up;
                    Matrix4x4 rotationMatrix = Matrix4x4.CreateFromAxisAngle(axis, angle);
                    direction = Vector3.Transform(direction, rotationMatrix);
                    up = Vector3.Cross(direction, right);
                }
            }
            
        }
    }

    public struct CameraSettings
    {
        public float fov;
        public float farPlane, nearPlane;
        public float aspectRatio;

        public CameraSettings(IWindow window)
        {
            fov = 60f;
            nearPlane = 0.1f;
            farPlane = 100f;
            aspectRatio = window.Size.X / window.Size.Y;
        }
    }

    public class ProjectionMatrices
    {
        public Matrix4x4 model, view, projection;

        public ProjectionMatrices(Matrix4x4 model, Matrix4x4 view, Matrix4x4 projection)
        {
            this.model = model;
            this.view = view;
            this.projection = projection;
        }
    }
}
