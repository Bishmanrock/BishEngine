using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class CameraManager
    {
        private static Camera activeCamera;

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
