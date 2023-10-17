// Object is a base class for all objects in the engine

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine
{
    public class Object
    {
        //
        // Summary:
        //     Removes a GameObject, component or asset.
        //
        // Parameters:
        //   obj:
        //     The object to destroy.
        //
        //   t:
        //     The optional amount of time to delay before destroying the object.
        public static void Destroy(Object obj)
        {
            float t = 0f;
            Destroy(obj, t);
        }

        //
        // Summary:
        //     Removes a GameObject, component or asset.
        //
        // Parameters:
        //   obj:
        //     The object to destroy.
        //
        //   t:
        //     The optional amount of time to delay before destroying the object.
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);
    }
}
