#region Assembly OpenGL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// F:\GameDev\opengl4tutorials-master\OpenGLTutorial13\OpenGLTutorial13\libs\OpenGL.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;

namespace Engine
{
    [Serializable]
    public struct Vector4 : IEquatable<Vector4>
    {
        public float X;

        public float Y;

        public float Z;

        public float W;

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

        [Obsolete("Use W instead, which is compatible with System.Numerics.")]
        public float w
        {
            get
            {
                return W;
            }
            set
            {
                W = value;
            }
        }

        [Obsolete("Use Vector4.One instead, which is compatible with System.Numerics.")]
        public static Vector4 Identity => new Vector4(1f, 1f, 1f, 1f);

        public static Vector4 Zero => new Vector4(0f, 0f, 0f, 0f);

        public static Vector4 UnitX => new Vector4(1f, 0f, 0f, 0f);

        public static Vector4 UnitY => new Vector4(0f, 1f, 0f, 0f);

        public static Vector4 UnitZ => new Vector4(0f, 0f, 1f, 0f);

        public static Vector4 UnitW => new Vector4(0f, 0f, 0f, 1f);

        public static Vector4 One => new Vector4(1f, 1f, 1f, 1f);

        [Obsolete("Use Get instead, which is compatible with System.Numerics.")]
        public float this[int a]
        {
            get
            {
                return a switch
                {
                    3 => W,
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
                    case 3:
                        W = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);

        public float SquaredLength => X * X + Y * Y + Z * Z + W * W;

        [Obsolete]
        public Vector2 Xy
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        [Obsolete]
        public Vector3 Xyz
        {
            get
            {
                return new Vector3(X, Y, Z);
            }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vector4 operator +(Vector4 v, float s)
        {
            return new Vector4(v.X + s, v.Y + s, v.Z + s, v.W + s);
        }

        public static Vector4 operator +(float s, Vector4 v)
        {
            return new Vector4(v.X + s, v.Y + s, v.Z + s, v.W + s);
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        public static Vector4 operator -(Vector4 v, float s)
        {
            return new Vector4(v.X - s, v.Y - s, v.Z - s, v.W - s);
        }

        public static Vector4 operator -(float s, Vector4 v)
        {
            return new Vector4(s - v.X, s - v.Y, s - v.Z, s - v.W);
        }

        public static Vector4 operator -(Vector4 v)
        {
            return new Vector4(0f - v.X, 0f - v.Y, 0f - v.Z, 0f - v.W);
        }

        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
        }

        public static Vector4 operator *(float s, Vector4 v)
        {
            return new Vector4(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static Vector4 operator *(Vector4 v, float s)
        {
            return new Vector4(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }

        public static Vector4 operator /(Vector4 v1, Vector4 v2)
        {
            return new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W / v2.W);
        }

        public static Vector4 operator /(float s, Vector4 v)
        {
            return new Vector4(s / v.X, s / v.Y, s / v.Z, s / v.W);
        }

        public static Vector4 operator /(Vector4 v, float s)
        {
            return new Vector4(v.X / s, v.Y / s, v.Z / s, v.W / s);
        }

        public static bool operator ==(Vector4 v1, Vector4 v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z)
            {
                return v1.W == v2.W;
            }

            return false;
        }

        public static bool operator !=(Vector4 v1, Vector4 v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z)
            {
                return v1.W != v2.W;
            }

            return true;
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [Obsolete("Use floats instead, which is compatible with System.Numerics.")]
        public Vector4(double x, double y, double z, double w)
        {
            X = (float)x;
            Y = (float)y;
            Z = (float)z;
            W = (float)w;
        }

        public Vector4(Vector3 v, float w)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = w;
        }

        [Obsolete("Construct manually, which is compatible with System.Numerics.")]
        public Vector4(byte[] RGBByte)
        {
            if (RGBByte.Length < 3)
            {
                throw new Exception("Color data was not 24bit as expected.");
            }

            X = (float)((double)(int)RGBByte[0] / 256.0);
            Y = (float)((double)(int)RGBByte[1] / 256.0);
            Z = (float)((double)(int)RGBByte[2] / 256.0);
            W = 1f;
        }

        [Obsolete("Use Vector4(float, float, float, float) instead, which is compatible with System.Numerics.")]
        public Vector4(float[] vector)
        {
            if (vector.Length != 4)
            {
                throw new Exception($"float[] vector was of length {vector.Length}.  Was expecting a length of 4.");
            }

            X = vector[0];
            Y = vector[1];
            Z = vector[2];
            W = vector[3];
        }

        public Vector4(float value)
        {
            X = (Y = (Z = (W = value)));
        }

        public Vector4(Vector2 v, float z, float w)
        {
            X = v.X;
            Y = v.Y;
            Z = z;
            W = w;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector4))
            {
                return false;
            }

            return Equals((Vector4)obj);
        }

        public bool Equals(Vector4 other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"<{X}, {Y}, {Z}, {W}>";
        }

        public static Vector4 Parse(string text)
        {
            string[] array = text.Trim('{', '}').Split(',');
            if (array.Length != 4)
            {
                return Zero;
            }

            return new Vector4(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
        }

        public float Get(int index)
        {
            return index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                3 => W,
                _ => 0f,
            };
        }

        public static float Dot(Vector4 v1, Vector4 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }

        public float Dot(Vector4 v)
        {
            return Dot(this, v);
        }

        public float[] ToFloat()
        {
            return new float[4] { X, Y, Z, W };
        }

        public Vector4 Normalize()
        {
            if (Length == 0f)
            {
                return Zero;
            }

            return new Vector4(X, Y, Z, W) / Length;
        }

        public Vector4 Truncate()
        {
            float num = (((double)Math.Abs(X) - 0.0001 < 0.0) ? 0f : X);
            float num2 = (((double)Math.Abs(Y) - 0.0001 < 0.0) ? 0f : Y);
            float num3 = (((double)Math.Abs(Z) - 0.0001 < 0.0) ? 0f : Z);
            float num4 = (((double)Math.Abs(W) - 0.0001 < 0.0) ? 0f : W);
            return new Vector4(num, num2, num3, num4);
        }

        public void TakeMin(Vector4 v)
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

            if (v.W < W)
            {
                W = v.W;
            }
        }

        public void TakeMax(Vector4 v)
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

            if (v.W > W)
            {
                W = v.W;
            }
        }

        public static Vector4 Lerp(Vector4 v1, Vector4 v2, float amount)
        {
            return v1 + (v2 - v1) * amount;
        }

        public static void Swap(ref Vector4 v1, ref Vector4 v2)
        {
            Vector4 vector = v1;
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
