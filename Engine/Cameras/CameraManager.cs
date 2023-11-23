// The CameraManager class is used for managing cameras. It serves as a way to set which is the currently active camera, and retrieve information about the currently active camera.

namespace Engine
{
    public static class CameraManager
    {
        public static Camera activeCamera { get; set; }

        public static void SetActiveCamera(Camera camera)
        {
            activeCamera = camera;
        }

        public static Camera GetActiveCamera()
        {
            return activeCamera;
        }
    }
}