// Camera2D is intended to be a camera specifically for 2D. This may now be redundant though.

namespace Engine
{
    public class Camera2D
    {
        public Vector2 focusPosition { get; set; }
        public float zoom { get; set; }

        public Camera2D(Vector2 focusPosition, float zoom)
        {
            this.focusPosition = focusPosition;
            this.zoom = zoom;
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            float left = focusPosition.x - Window.windowSize.x / 2f;
            float right = focusPosition.x + Window.windowSize.x / 2f;
            float top = focusPosition.y - Window.windowSize.y / 2f;
            float bottom = focusPosition.y + Window.windowSize.y / 2f;

            Matrix4x4 orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(
                left,
                right,
                bottom,
                top,
                0.01f, // Near plane
                100f); // Far plane

            Matrix4x4 zoomMatrix = Matrix4x4.CreateScale(zoom);

            return orthoMatrix * zoomMatrix;
        }
    }
}