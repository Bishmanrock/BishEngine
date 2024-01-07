// This is the master class for other cameras to inherit from

namespace Engine
{
    public class Camera
    {
        private CameraMode cameraMode = CameraMode.PERSPECTIVE; // The camera mode. Either perspective or orthographic. Defaults to perspective.

        // Then, we create two matrices to hold our view and projection. They're initialized at the bottom of OnLoad.
        // The view matrix is what you might consider the "camera". It represents the current viewport in the window.
        private Matrix4x4 _view;

        // This represents how the vertices will be projected. It's hard to explain through comments,
        // so check out the web version for a good demonstration of what this does.
        // Note: This shouldn't be in the individual camera? It ties into aspect ratio so surely would need to be across all cameras
        private Matrix4x4 _projection;

        public Camera()
        {
            CameraManager.activeCamera = this; // Sets this as the primary camera. This may need to be split out in future if creating multiple cameras at once.

            // For the view, we don't do too much here. Next tutorial will be all about a Camera class that will make it much easier to manipulate the view.
            // For now, we move it backwards three units on the Z axis.
            _view = Matrix4x4.CreateTranslation(0.0f, 0.0f, -3.0f);

            // For the matrix, we use a few parameters.
            //   Field of view. This determines how much the viewport can see at once. 45 is considered the most "realistic" setting, but most video games nowadays use 90
            //   Aspect ratio. This should be set to Width / Height.
            //   Near-clipping. Any vertices closer to the camera than this value will be clipped.
            //   Far-clipping. Any vertices farther away from the camera than this value will be clipped.
            //_projection = Matrix4x4.CreatePerspectiveFieldOfView(Engine.Math.DegreesToRadians(45), DisplayManager.windowSize.X / DisplayManager.windowSize.Y, 0.1f, 100.0f);

            _projection = Matrix4x4.CreatePerspectiveFieldOfView(Maths.DegreesToRadians(45f), Window.windowSize.x / Window.windowSize.y, 0.1f, 100.0f);

            // Now, head over to OnRenderFrame to see how we setup the model matrix.
        }

        public Matrix4x4 GetView()
        {
            return _view;
        }

        public Matrix4x4 GetProjection()
        {
            return _projection;
        }
    }
}