#region Assembly OpenGL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// F:\GameDev\opengl4tutorials-master\OpenGLTutorial13\OpenGLTutorial13\libs\OpenGL.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;

namespace Engine
{
    [Serializable]
    public struct Vector2 : IEquatable<Vector2>
    {
        public float X;

        public float Y;

        [Obsolete("Use X instead, which is compatible with System.Numerics.")]
        public float x
        {
            get
            {
                return X;
            }
            set
            {
                X = value;
            }
        }

        [Obsolete("Use Y instead, which is compatible with System.Numerics.")]
        public float y
        {
            get
            {
                return Y;
            }
            set
            {
                Y = value;
            }
        }

        [Obsolete("Vector2.Identity is not compatible with System.Numerics.  Use Vector2.One instead.")]
        public static Vector2 Identity => new Vector2(1f, 1f);

        public static Vector2 Zero => new Vector2(0f, 0f);

        public static Vector2 One => new Vector2(1f, 1f);

        public static Vector2 UnitX => new Vector2(1f, 0f);

        public static Vector2 UnitY => new Vector2(0f, 1f);

        public float this[int a]
        {
            get
            {
                return a switch
                {
                    1 => Y,
                    0 => X,
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
            set
            {
                switch (a)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        [Obsolete("Use LengthSquared() instead, which is compatible with System.Numerics.")]
        public float SquaredLength => X * X + Y * Y;

        public Vector2 PerpendicularRight => new Vector2(Y, 0f - X);

        public Vector2 PerpendicularLeft => new Vector2(0f - Y, X);

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator +(Vector2 v, float s)
        {
            return new Vector2(v.X + s, v.Y + s);
        }

        public static Vector2 operator +(float s, Vector2 v)
        {
            return new Vector2(v.X + s, v.Y + s);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator -(Vector2 v, float s)
        {
            return new Vector2(v.X - s, v.Y - s);
        }

        public static Vector2 operator -(float s, Vector2 v)
        {
            return new Vector2(s - v.X, s - v.Y);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(0f - v.X, 0f - v.Y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 operator *(float s, Vector2 v)
        {
            return new Vector2(v.X * s, v.Y * s);
        }

        public static Vector2 operator *(Vector2 v, float s)
        {
            return new Vector2(v.X * s, v.Y * s);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector2 operator /(float s, Vector2 v)
        {
            return new Vector2(s / v.X, s / v.Y);
        }

        public static Vector2 operator /(Vector2 v, float s)
        {
            return new Vector2(v.X / s, v.Y / s);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            if (v1.X == v2.X)
            {
                return v1.Y == v2.Y;
            }

            return false;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            if (v1.X == v2.X)
            {
                return v1.Y != v2.Y;
            }

            return true;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        [Obsolete("Use floats instead, which is compatible with System.Numerics.")]
        public Vector2(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public Vector2(float value)
        {
            X = (Y = value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
            {
                return false;
            }

            return Equals((Vector2)obj);
        }

        public bool Equals(Vector2 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"<{X}, {Y}>";
        }

        public static Vector2 Parse(string text)
        {
            string[] array = text.Trim('{', '}').Split(',');
            if (array.Length != 2)
            {
                return Zero;
            }

            return new Vector2(float.Parse(array[0]), float.Parse(array[1]));
        }

        public float LengthSquared()
        {
            return X * X + Y * Y;
        }

        public float[] ToFloat()
        {
            return new float[2] { X, Y };
        }

        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public float Dot(Vector2 v)
        {
            return Dot(v);
        }

        public Vector2 Normalize()
        {
            if (Length == 0f)
            {
                return Zero;
            }

            return new Vector2(X, Y) / Length;
        }

        public Vector2 Truncate()
        {
            float num = (((double)Math.Abs(X) - 0.0001 < 0.0) ? 0f : X);
            float num2 = (((double)Math.Abs(Y) - 0.0001 < 0.0) ? 0f : Y);
            return new Vector2(num, num2);
        }

        public void TakeMin(Vector2 v)
        {
            if (v.X < X)
            {
                X = v.X;
            }

            if (v.Y < Y)
            {
                Y = v.Y;
            }
        }

        public void TakeMax(Vector2 v)
        {
            if (v.X > X)
            {
                X = v.X;
            }

            if (v.Y > Y)
            {
                Y = v.Y;
            }
        }

        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float amount)
        {
            return v1 + (v2 - v1) * amount;
        }

        public static void Swap(ref Vector2 v1, ref Vector2 v2)
        {
            Vector2 vector = v1;
            v1 = v2;
            v2 = vector;
        }
    }
}
#if false // Decompilation log
'6' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll'
------------------
Resolve: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.dll'
------------------
Resolve: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Core.dll'
------------------
Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.dll'
#endif
