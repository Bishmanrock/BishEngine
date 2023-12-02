// GameObject is a base class for all entities within scenes

namespace Engine
{
    public class GameObject : Object
    {
        public Transform transform;

        private bool isActive; // Whether the GameObject is active or not

        public GameObject()
        {
            transform = new Transform();
            SetActive(true);
        }

        // Sets the active bool and adds/removes object to the rendering manager based off if it's active or not
        public void SetActive(bool active)
        {
            isActive = active;

            if (isActive == true)
            {
                RenderingManager.Add(this);
            }
            else
            {
                RenderingManager.Remove(this);
            }
        }

        /// <summary>
        /// Creates the matrix which transforms vertex positions into world positions.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns>The transformation matrix.</returns>
        public Matrix4x4 TransformToModel(Transform transform)
        {
            return Matrix4x4.CreateRotationX(transform.rotation.x) *
                Matrix4x4.CreateRotationY(transform.rotation.y) *
                 Matrix4x4.CreateRotationZ(transform.rotation.z) *
                Matrix4x4.CreateScale(transform.scale) *
                 Matrix4x4.CreateTranslation(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}