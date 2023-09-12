using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Time
    {
        public static float deltaTime { get; set; } // The interval in seconds from the last frame to the current one

        public static float totalElapsedSeconds { get; set; } // The total elapsed seconds since the application started running // REplaced by time

        public static float previousTime { get; set; } // The previously recorded totalElapsedSeconds

        public static float applicationStartTime { get; set; } // The time when the application started

        public static float time { get; set; } // The time at the beginning of this frame
    }
}