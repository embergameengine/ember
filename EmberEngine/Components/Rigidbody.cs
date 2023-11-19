using System;
using System.Numerics;
using Jitter;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;

namespace EmberEngine.Components
{
    public class Rigidbody : Component
    {
        private RigidBody _rigidBody;
        private BoxCollider collider;
        float scaleFactor = 1.524f;
        public bool isStatic { get; set; }

        public Rigidbody(float mass, JVector size)
        {
            collider = gameObject.GetComponent<BoxCollider>();
            // Create a dynamic rigid body
            _rigidBody = new RigidBody(collider.shape);
            _rigidBody.Mass = mass;

            // Add the rigid body to the world
            SceneManager.currentScene.physicsWorld.AddBody(_rigidBody);

            isStatic = true;
        }

        public Rigidbody()
        {
            // Create a dynamic rigid body
            _rigidBody = new RigidBody(new BoxShape(new JVector(1, 1, 1)));
            _rigidBody.Mass = 0.5f;
            _rigidBody.AffectedByGravity = true;

            // Add the rigid body to the world
            SceneManager.currentScene.physicsWorld.AddBody(_rigidBody);

            isStatic = true;
        }

        public override void Update(double dt)
        {
            if (isStatic)
            {
                _rigidBody.IsStatic = true;
            } else
            {
                _rigidBody.IsStatic = false;
            }

            // Update the transform of the game object based on the rigid body's transform
            transform.position = new Vector3(_rigidBody.Position.X, _rigidBody.Position.Y, -_rigidBody.Position.Z);
            Console.WriteLine(_rigidBody.Position);
            transform.rotation = new Vector3(_rigidBody.Orientation.M11, _rigidBody.Orientation.M12, _rigidBody.Orientation.M13);
        }

        public override void Load()
        {
            // Add the rigid body to the world
            //SceneManager.currentScene.physicsWorld.AddBody(_rigidBody);
            _rigidBody.Position = new JVector(transform.position.X, transform.position.Y, transform.position.Z);
            //_rigidBody.Orientation = JMatrix.CreateFromYawPitchRoll(transform.rotation.Y, transform.rotation.X, transform.rotation.Z);
        }
    }
}
