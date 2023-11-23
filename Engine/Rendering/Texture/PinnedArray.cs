using System.Runtime.InteropServices;

namespace Engine
{
    public class PinnedArray : IDisposable
    {
        private readonly GCHandle _handle;

        public IntPtr Pointer => _handle.AddrOfPinnedObject();

        public PinnedArray(byte[] array)
        {
            _handle = GCHandle.Alloc(array, GCHandleType.Pinned);
        }

        public GCHandle GetHandle()
        {
            return _handle;
        }

        public void Dispose()
        {
            _handle.Free();
        }
    }
}