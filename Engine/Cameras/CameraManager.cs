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