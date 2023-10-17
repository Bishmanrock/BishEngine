// Class that controls where in the world space an object sits

namespace Engine
{
    public struct Transform
    {
        Matrix4x4 transformPosition;

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
            scale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}