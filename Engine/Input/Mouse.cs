using System.Collections;
using System.Text;
using GLFW;

namespace Engine
{
    /// <summary>
    /// Encapsulates the state of a mouse device.
    /// </summary>
    public class Mouse
    {
        /// <summary>
        /// The maximum number of buttons a <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseState" /> can represent.
        /// </summary>
        internal const int MaxButtons = 16;

        private readonly BitArray _buttons = new BitArray(16);

        private readonly BitArray _buttonsPrevious = new BitArray(16);

        /// <summary>
        /// Gets a <see cref="T:OpenTK.Mathematics.Vector2" /> representing the absolute position of the pointer
        /// in the current frame, relative to the top-left corner of the contents of the window.
        /// </summary>
        public Vector2 Position { get; internal set; }

        /// <summary>
        /// Gets a <see cref="T:OpenTK.Mathematics.Vector2" /> representing the absolute position of the pointer
        /// in the previous frame, relative to the top-left corner of the contents of the window.
        /// </summary>
        public Vector2 PreviousPosition { get; internal set; }

        /// <summary>
        /// Gets a <see cref="T:OpenTK.Mathematics.Vector2" /> representing the amount that the mouse moved since the last frame.
        /// This does not necessarily correspond to pixels, for example in the case of raw input.
        /// </summary>
        public Vector2 Delta => Position - PreviousPosition;

        /// <summary>
        /// Get a Vector2 representing the position of the mouse wheel.
        /// </summary>
        public Vector2 Scroll { get; internal set; }

        /// <summary>
        /// Get a Vector2 representing the position of the mouse wheel.
        /// </summary>
        public Vector2 PreviousScroll { get; internal set; }

        /// <summary>
        /// Get a Vector2 representing the amount that the mouse wheel moved since the last frame.
        /// </summary>
        public Vector2 ScrollDelta => Scroll - PreviousScroll;

        /// <summary>
        /// Gets a <see cref="T:System.Boolean" /> indicating whether the specified
        ///  <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseButton" /> is pressed.
        /// </summary>
        /// <param name="button">The <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseButton" /> to check.</param>
        /// <returns><c>true</c> if key is pressed; <c>false</c> otherwise.</returns>
        public bool this[MouseButton button]
        {
            get
            {
                return _buttons[(int)button];
            }
            internal set
            {
                _buttons[(int)button] = value;
            }
        }

        /// <summary>
        /// Gets an integer representing the absolute x position of the pointer, in window pixel coordinates.
        /// </summary>
        public float X => Position.x;

        /// <summary>
        /// Gets an integer representing the absolute y position of the pointer, in window pixel coordinates.
        /// </summary>
        public float Y => Position.y;

        /// <summary>
        /// Gets an integer representing the absolute x position of the pointer, in window pixel coordinates.
        /// </summary>
        public float PreviousX => PreviousPosition.x;

        /// <summary>
        /// Gets an integer representing the absolute y position of the pointer, in window pixel coordinates.
        /// </summary>
        public float PreviousY => PreviousPosition.y;

        /// <summary>
        /// Gets a value indicating whether any button is down.
        /// </summary>
        /// <value><c>true</c> if any button is down; otherwise, <c>false</c>.</value>
        public bool IsAnyButtonDown
        {
            get
            {
                for (int i = 0; i < 16; i++)
                {
                    if (_buttons[i])
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public Mouse()
        {
        }

        private Mouse(Mouse source)
        {
            Position = source.Position;
            PreviousPosition = source.PreviousPosition;
            Scroll = source.Scroll;
            PreviousScroll = source.PreviousScroll;
            _buttons = (BitArray)source._buttons.Clone();
            _buttonsPrevious = (BitArray)source._buttonsPrevious.Clone();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseState" />.
        /// </summary>
        /// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseState" />.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                stringBuilder.Append(_buttons[i] ? "1" : "0");
            }

            return $"[X={X}, Y={Y}, Buttons={stringBuilder}]";
        }

        internal void Update()
        {
            _buttonsPrevious.SetAll(value: false);
            _buttonsPrevious.Or(_buttons);
            PreviousPosition = Position;
            PreviousScroll = Scroll;
        }

        /// <summary>
        /// Gets a <see cref="T:System.Boolean" /> indicating whether this button is down.
        /// </summary>
        /// <param name="button">The <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseButton" /> to check.</param>
        /// <returns><c>true</c> if the <paramref name="button" /> is down, otherwise <c>false</c>.</returns>
        public bool IsButtonDown(MouseButton button)
        {
            return _buttons[(int)button];
        }

        /// <summary>
        /// Gets a <see cref="T:System.Boolean" /> indicating whether this button was down in the previous frame.
        /// </summary>
        /// <param name="button">The <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseButton" /> to check.</param>
        /// <returns><c>true</c> if the <paramref name="button" /> is down, otherwise <c>false</c>.</returns>
        public bool WasButtonDown(MouseButton button)
        {
            return _buttonsPrevious[(int)button];
        }

        /// <summary>
        /// Gets whether the specified mouse button is pressed in the current frame but released in the previous frame.
        /// </summary>
        /// <remarks>
        ///     "Frame" refers to invocations of <see cref="!:NativeWindow.ProcessEvents()" /> (<see cref="!:NativeWindow.ProcessInputEvents()" /> more precisely) here.
        /// </remarks>
        /// <param name="button">The <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseButton">mouse button</see> to check.</param>
        /// <returns>True if the mouse button is pressed in this frame, but not the last frame.</returns>
        public bool IsButtonPressed(MouseButton button)
        {
            if (_buttons[(int)button])
            {
                return !_buttonsPrevious[(int)button];
            }

            return false;
        }

        /// <summary>
        /// Gets whether the specified mouse button is released in the current frame but pressed in the previous frame.
        /// </summary>
        /// <remarks>
        /// "Frame" refers to invocations of <see cref="!:NativeWindow.ProcessEvents()" /> (<see cref="!:NativeWindow.ProcessInputEvents()" /> more precisely) here.
        /// </remarks>
        /// <param name="button">The <see cref="T:OpenTK.Windowing.GraphicsLibraryFramework.MouseButton">mouse button</see> to check.</param>
        /// <returns>True if the mouse button is released in this frame, but pressed the last frame.</returns>
        public bool IsButtonReleased(MouseButton button)
        {
            if (!_buttons[(int)button])
            {
                return _buttonsPrevious[(int)button];
            }

            return false;
        }

        /// <summary>
        /// Gets an immutable snapshot of this MouseState.
        /// This can be used to save the current mouse state for comparison later on.
        /// </summary>
        /// <returns>Returns an immutable snapshot of this MouseState.</returns>
        public Mouse GetSnapshot()
        {
            return new Mouse(this);
        }
    }
}
#if false // Decompilation log
'161' items in cache
------------------
Resolve: 'System.Runtime, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\microsoft.netcore.app.ref\3.1.0\ref\netcoreapp3.1\System.Runtime.dll'
------------------
Resolve: 'System.Runtime.Extensions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Extensions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\microsoft.netcore.app.ref\3.1.0\ref\netcoreapp3.1\System.Runtime.Extensions.dll'
------------------
Resolve: 'OpenTK.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'OpenTK.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\opentk.core\4.7.4\lib\netstandard2.1\OpenTK.Core.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\microsoft.netcore.app.ref\3.1.0\ref\netcoreapp3.1\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Diagnostics.Debug, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Debug, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\microsoft.netcore.app.ref\3.1.0\ref\netcoreapp3.1\System.Diagnostics.Debug.dll'
------------------
Resolve: 'OpenTK.Windowing.Common, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'OpenTK.Windowing.Common, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\opentk.windowing.common\4.7.4\lib\netcoreapp3.1\OpenTK.Windowing.Common.dll'
------------------
Resolve: 'System.Collections, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\microsoft.netcore.app.ref\3.1.0\ref\netcoreapp3.1\System.Collections.dll'
------------------
Resolve: 'OpenTK.Mathematics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'OpenTK.Mathematics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\opentk.mathematics\4.7.4\lib\netcoreapp3.1\OpenTK.Mathematics.dll'
------------------
Resolve: 'System.Runtime.InteropServices.RuntimeInformation, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices.RuntimeInformation, Version=4.0.4.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Users\Bishmanrock\.nuget\packages\microsoft.netcore.app.ref\3.1.0\ref\netcoreapp3.1\System.Runtime.InteropServices.RuntimeInformation.dll'
#endif
