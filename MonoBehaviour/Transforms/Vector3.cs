#region Assembly OpenGL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// F:\GameDev\opengl4tutorials-master\OpenGLTutorial13\OpenGLTutorial13\libs\OpenGL.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Engine
{
    [Serializable]
    public struct Vector3 : IEquatable<Vector3>
    {
        public float X;

        public float Y;

        public float Z;

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

        [Obsolete("Use Z instead, which is compatible with System.Numerics.")]
        public float z
        {
            get
            {
                return Z;
            }
            set
            {
                Z = value;
            }
        }

        [Obsolete("Vector3.Identity is not compatible with System.Numerics.  Use Vector3.One instead.")]
        public static Vector3 Identity => new Vector3(1f, 1f, 1f);

        public static Vector3 Zero => new Vector3(0f, 0f, 0f);

        [Obsolete("Vector3.Up is not compatible with System.Numerics.  Use Vector3.UnitY instead.")]
        public static Vector3 Up => new Vector3(0f, 1f, 0f);

        [Obsolete("Vector3.Down is not compatible with System.Numerics.  Use new Vector3(0, -1, 0) instead.")]
        public static Vector3 Down => new Vector3(0f, -1f, 0f);

        [Obsolete("Vector3.Forward is not compatible with System.Numerics.  Use new Vector3(0, 0, -1) instead.")]
        public static Vector3 Forward => new Vector3(0f, 0f, -1f);

        [Obsolete("Vector3.Backward is not compatible with System.Numerics.  Use Vector3.UnitZ instead.")]
        public static Vector3 Backward => new Vector3(0f, 0f, 1f);

        [Obsolete("Vector3.Left is not compatible with System.Numerics.  Use new Vector3(-1, 0, 0) instead.")]
        public static Vector3 Left => new Vector3(-1f, 0f, 0f);

        [Obsolete("Vector3.Right is not compatible with System.Numerics.  Use Vector3.UnitX instead.")]
        public static Vector3 Right => new Vector3(1f, 0f, 0f);

        public static Vector3 UnitX => new Vector3(1f, 0f, 0f);

        public static Vector3 UnitY => new Vector3(0f, 1f, 0f);

        public static Vector3 UnitZ => new Vector3(0f, 0f, 1f);

        [Obsolete("Vector3.UnitScale is not compatible with System.Numerics.  Use Vector3.One instead.")]
        public static Vector3 UnitScale => new Vector3(1f, 1f, 1f);

        public static Vector3 One => new Vector3(1f, 1f, 1f);

        public float this[int a]
        {
            get
            {
                return a switch
                {
                    2 => Z,
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
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>Returns the length of this vector object.</summary>
        /// <returns>The vector's length.</returns>
        /// <altmember cref="System.Numerics.Vector3.LengthSquared"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float Length()
        {
            float lengthSquared = LengthSquared();
            return (float)Math.Sqrt(lengthSquared);
        }

        [Obsolete("Use LengthSquared() instead, which is compatible with System.Numerics.")]
        public float SquaredLength => X * X + Y * Y + Z * Z;

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator +(Vector3 v, float s)
        {
            return new Vector3(v.X + s, v.Y + s, v.Z + s);
        }

        public static Vector3 operator +(float s, Vector3 v)
        {
            return new Vector3(v.X + s, v.Y + s, v.Z + s);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator -(Vector3 v, float s)
        {
            return new Vector3(v.X - s, v.Y - s, v.Z - s);
        }

        public static Vector3 operator -(float s, Vector3 v)
        {
            return new Vector3(s - v.X, s - v.Y, s - v.Z);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(0f - v.X, 0f - v.Y, 0f - v.Z);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public static Vector3 operator *(float s, Vector3 v)
        {
            return new Vector3(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vector3 operator *(Vector3 v, float s)
        {
            return new Vector3(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
        }

        public static Vector3 operator /(float s, Vector3 v)
        {
            return new Vector3(s / v.X, s / v.Y, s / v.Z);
        }

        public static Vector3 operator /(Vector3 v, float s)
        {
            return new Vector3(v.X / s, v.Y / s, v.Z / s);
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y)
            {
                return v1.Z == v2.Z;
            }

            return false;
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y)
            {
                return v1.Z != v2.Z;
            }

            return true;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [Obsolete("Use floats instead, which is compatible with System.Numerics.")]
        public Vector3(double x, double y, double z)
        {
            X = (float)x;
            Y = (float)y;
            Z = (float)z;
        }

        [Obsolete("Use Vector3(float, float, float) instead, which is compatible with System.Numerics.")]
        public Vector3(float[] vector)
        {
            if (vector.Length != 3)
            {
                throw new Exception($"float[] vector was of length {vector.Length}.  Was expecting a length of 3.");
            }

            X = vector[0];
            Y = vector[1];
            Z = vector[2];
        }

        public Vector3(float value)
        {
            X = (Y = (Z = value));
        }

        public Vector3(Vector2 v, float z)
        {
            X = v.X;
            Y = v.Y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector3))
            {
                return false;
            }

            return Equals((Vector3)obj);
        }

        public bool Equals(Vector3 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"<{X}, {Y}, {Z}>";
        }

        public static Vector3 Parse(string text)
        {
            string[] array = text.Trim('{', '}').Split(',');
            if (array.Length != 3)
            {
                return Zero;
            }

            return new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
        }

        public float[] ToFloat()
        {
            return new float[3] { X, Y, Z };
        }

        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public float Dot(Vector3 v)
        {
            return Dot(this, v);
        }

        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        public Vector3 Cross(Vector3 v)
        {
            return Cross(this, v);
        }

        public Vector3 Truncate()
        {
            float num = (((double)Math.Abs(X) - 0.0001 < 0.0) ? 0f : X);
            float num2 = (((double)Math.Abs(Y) - 0.0001 < 0.0) ? 0f : Y);
            float num3 = (((double)Math.Abs(Z) - 0.0001 < 0.0) ? 0f : Z);
            return new Vector3(num, num2, num3);
        }

        public void TakeMin(Vector3 v)
        {
            if (v.X < X)
            {
                X = v.X;
            }

            if (v.Y < Y)
            {
                Y = v.Y;
            }

            if (v.Z < Z)
            {
                Z = v.Z;
            }
        }

        public void TakeMax(Vector3 v)
        {
            if (v.X > X)
            {
                X = v.X;
            }

            if (v.Y > Y)
            {
                Y = v.Y;
            }

            if (v.Z > Z)
            {
                Z = v.Z;
            }
        }

        public float Max()
        {
            if (!(X >= Y) || !(X >= Z))
            {
                if (!(Y >= Z))
                {
                    return Z;
                }

                return Y;
            }

            return X;
        }

        public float Min()
        {
            if (!(X <= Y) || !(X <= Z))
            {
                if (!(Y <= Z))
                {
                    return Z;
                }

                return Y;
            }

            return X;
        }

        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float amount)
        {
            return v1 + (v2 - v1) * amount;
        }

        public static float CalculateAngle(Vector3 first, Vector3 second)
        {
            return (float)Math.Acos(Dot(first, second) / (first.Length() * second.Length()));
        }

        public float CalculateAngle(Vector3 v)
        {
            return CalculateAngle(this, v);
        }

        public static void Swap(ref Vector3 v1, ref Vector3 v2)
        {
            Vector3 vector = v1;
            v1 = v2;
            v2 = vector;
        }

        public Quaternion GetRotationTo(Vector3 destination)
        {
            Vector3 vector = Normalize();
            Vector3 v = destination.Normalize();
            float num = vector.Dot(v);
            if (num >= 1f)
            {
                return Quaternion.Identity;
            }

            if (num < -0.999999f)
            {
                Vector3 axis = UnitX.Cross(this);
                if ((double)axis.LengthSquared() < 1E-12)
                {
                    axis = UnitY.Cross(this);
                }

                axis.Normalize();
                return Quaternion.FromAngleAxis((float)Math.Pi, axis);
            }

            float num2 = (float)Math.Sqrt((1f + num) * 2f);
            float num3 = 1f / num2;
            Vector3 vector2 = vector.Cross(v);
            return new Quaternion(vector2.X * num3, vector2.Y * num3, vector2.Z * num3, num2 * 0.5f).Normalize();
        }

        public static Vector3 Abs(Vector3 value)
        {
            return new Vector3(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));
        }

        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            float num = Math.Max(min.X, max.X);
            num = ((value.X < min.X) ? min.X : ((value.X > num) ? num : value.X));
            float num2 = Math.Max(min.Y, max.Y);
            num2 = ((value.Y < min.Y) ? min.Y : ((value.Y > num2) ? num2 : value.Y));
            float num3 = Math.Max(min.Z, max.Z);
            num3 = ((value.Z < min.Z) ? min.Z : ((value.Z > num3) ? num3 : value.Z));
            return new Vector3(num, num2, num3);
        }

        public static Vector3 Add(Vector3 v1, Vector3 v2)
        {
            return v1 + v2;
        }

        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return (v1 - v2).Length();
        }

        public static float DistanceSquared(Vector3 v1, Vector3 v2)
        {
            return (v1 - v2).LengthSquared();
        }

        public static Vector3 Divide(Vector3 v, float f)
        {
            return v / f;
        }

        public static Vector3 Divide(Vector3 v1, Vector3 v2)
        {
            return v1 / v2;
        }

        public static Vector3 Max(Vector3 v1, Vector3 v2)
        {
            return new Vector3(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y), Math.Max(v1.Z, v2.Z));
        }

        public static Vector3 Min(Vector3 v1, Vector3 v2)
        {
            return new Vector3(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y), Math.Min(v1.Z, v2.Z));
        }

        public static Vector3 Multiply(Vector3 v, float f)
        {
            return v * f;
        }

        public static Vector3 Multiply(float f, Vector3 v)
        {
            return f * v;
        }

        public static Vector3 Multiply(Vector3 v1, Vector3 v2)
        {
            return v1 * v2;
        }

        public static Vector3 Negate(Vector3 v)
        {
            return new Vector3(0f - v.X, 0f - v.Y, 0f - v.Z);
        }

        public Vector3 Normalize()
        {
            if (Length() == 0f)
            {
                return Zero;
            }

            return new Vector3(X, Y, Z) / Length();
        }

        public static Vector3 Normalize(Vector3 v)
        {
            if (v.Length() == 0f)
            {
                return Zero;
            }

            return new Vector3(v.X, v.Y, v.Z) / v.Length();
        }

        public static Vector3 Reflect(Vector3 v, Vector3 normal)
        {
            return v - Dot(v, normal) * normal * 2f;
        }

        public static Vector3 SquareRoot(Vector3 v)
        {
            return new Vector3((float)Math.Sqrt(v.X), (float)Math.Sqrt(v.Y), (float)Math.Sqrt(v.Z));
        }

        public static Vector3 Subtract(Vector3 v1, Vector3 v2)
        {
            return v1 - v2;
        }

        public static Vector3 Transform(Vector3 v, Quaternion q)
        {
            return q * v;
        }

        public float Get(int index)
        {
            return index switch
            {
                1 => Y,
                0 => X,
                _ => Z,
            };
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
