using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
            float left = focusPosition.X - Window.windowSize.X / 2f;
            float right = focusPosition.X + Window.windowSize.X / 2f;
            float top = focusPosition.Y - Window.windowSize.Y / 2f;
            float bottom = focusPosition.Y + Window.windowSize.Y / 2f;

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