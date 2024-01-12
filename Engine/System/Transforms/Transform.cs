// The Transform component determines the Position, Rotation, and Scale of every object. Every GameObject has a Transform.

namespace Engine
{
    public struct Transform
    {
        Matrix4x4 transformPosition; // What is this?! Why not just use the below Vector3 positon?

        public Vector3 position;
        public Vector3 rotation;
        Vector3 eulerRot;
        public Vector3 scale;

        public Transform()
        {
            Vector4 vec = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
            transformPosition = Matrix4x4.CreateTranslation(1f, 1f, 0.0f);

            position = new Vector3(0.0f, 0.0f, 0.0f);
            rotation = new Vector3(0, 0, 0);
            eulerRot = new Vector3(0.0f, 0.0f, 0.0f);
            scale = new Vector3(1, 1, 1);
        }

        /// <summary>
        /// Sets the transform position
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetPosition(Vector3 newPosition)
        {
            position = newPosition;
        }

        /// <summary>
        /// Sets the rotation
        /// </summary>
        /// <param name="newRotation"></param>
        public void SetRotation(Vector3 newRotation)
        {
            rotation = newRotation;
        }

        /// <summary>
        /// Sets the scale
        /// </summary>
        /// <param name="newScale"></param>
        public void SetScale(Vector3 newScale)
        {
            scale = newScale;
        }

        /// <summary>
        /// Creates the matrix which transforms vertex positions into world positions.
        /// </summary>
        public Matrix4x4 TransformToModel()
        {
            return Matrix4x4.CreateRotationX(rotation.x) *
                Matrix4x4.CreateRotationY(rotation.y) *
                 Matrix4x4.CreateRotationZ(rotation.z) *
                Matrix4x4.CreateScale(scale) *
                 Matrix4x4.CreateTranslation(position.x, position.y, position.z);
        }
    }
}