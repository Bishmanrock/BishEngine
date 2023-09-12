using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This is the master class for other cameras to inherit from

namespace Engine
{
    public class Camera
    {
        private CameraMode cameraMode = CameraMode.PERSPECTIVE;

        public Camera()
        {

        }
    }
}