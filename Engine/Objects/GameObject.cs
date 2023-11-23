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

        // Sets the active bool
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
    }
}