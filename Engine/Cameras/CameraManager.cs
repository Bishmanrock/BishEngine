// The CameraManager class is used for managing cameras. It serves as a way to set which is the currently active camera, and retrieve information about the currently active camera.

namespace Engine
{
    public static class CameraManager
    {
        public static Camera activeCamera { get; set; }

        // Sets the provided camera as the active camera
        public static void SetActiveCamera(Camera camera)
        {
            activeCamera = camera;
        }

        // Returns the currently active camera
        public static Camera GetActiveCamera()
        {
            return activeCamera;
        }
    }
}