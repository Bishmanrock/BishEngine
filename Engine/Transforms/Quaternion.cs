namespace Engine
{
    [Serializable] public struct Quaternion : IEquatable<Quaternion>
    {
        public float X;

        public float Y;

        public float Z;

        public float W;

        private static readonly int[] rotationLookup = new int[3] { 1, 2, 0 };

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

        public static Quaternion Zero => new Quaternion(0f, 0f, 0f, 0f);

        public static Quaternion Identity => new Quaternion(0f, 0f, 0f, 1f);

        public Matrix4x4 Matrix4 => new Matrix4x4(
            1f - 2f * (Y * Y + Z * Z),
            2f * (X * Y - W * Z),
            2f * (X * Z + W * Y),
            0f,
            2f * (X * Y + W * Z),
            1f - 2f * (X * X + Z * Z),
            2f * (Y * Z - W * X),
            0f,
            2f * (X * Z - W * Y),
            2f * (Y * Z + W * X),
            1f - 2f * (X * X + Y * Y),
            0f,
            0f, 0f, 0f, 1f);

        public float this[int a]
        {
            get
            {
                return a switch
                {
                    3 => Z,
                    1 => Y,
                    0 => X,
                    _ => W,
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
                        W = value;
                        break;
                }
            }
        }

        public float Length => (float)Maths.Sqrt(X * X + Y * Y + Z * Z + W * W);

        public float SquaredLength => X * X + Y * Y + Z * Z + W * W;

        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Quaternion(Vector4 vec)
        {
            X = vec.X;
            Y = vec.Y;
            Z = vec.Z;
            W = vec.W;
        }

        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.X + q2.X, q1.Y + q2.Y, q1.Z + q2.Z, q1.W + q2.W);
        }

        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.X - q2.X, q1.Y - q2.Y, q1.Z - q2.Z, q1.W - q2.W);
        }

        public static Quaternion operator -(Quaternion q)
        {
            return new Quaternion(0f - q.X, 0f - q.Y, 0f - q.Z, 0f - q.W);
        }

        public static Quaternion operator *(Quaternion q, float s)
        {
            return new Quaternion(s * q.X, s * q.Y, s * q.Z, s * q.W);
        }

        public static Quaternion operator *(float s, Quaternion q)
        {
            return new Quaternion(s * q.X, s * q.Y, s * q.Z, s * q.W);
        }

        public static Vector3 operator *(Quaternion q, Vector3 v)
        {
            Vector3 v2 = new Vector3(q.X, q.Y, q.Z);
            Vector3 vector = Vector3.Cross(v2, v);
            Vector3 vector2 = Vector3.Cross(v2, vector);
            vector *= 2f * q.W;
            vector2 *= 2f;
            return v + vector + vector2;
        }

        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y + q1.W * q2.X, (0f - q1.X) * q2.Z + q1.Y * q2.W + q1.Z * q2.X + q1.W * q2.Y, q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W + q1.W * q2.Z, (0f - q1.X) * q2.X - q1.Y * q2.Y - q1.Z * q2.Z + q1.W * q2.W);
        }

        public static Quaternion operator /(Quaternion q, float scalar)
        {
            float num = 1f / scalar;
            return new Quaternion(q.X * num, q.Y * num, q.Z * num, q.W * num);
        }

        public static Quaternion operator /(Quaternion q1, Quaternion q2)
        {
            return q1 * q2.Inverse();
        }

        public static bool operator ==(Quaternion q1, Quaternion q2)
        {
            if (q1.W == q2.W && q1.X == q2.X && q1.Y == q2.Y)
            {
                return q1.Z == q2.Z;
            }

            return false;
        }

        public static bool operator !=(Quaternion q1, Quaternion q2)
        {
            if (q1.W == q2.W && q1.X == q2.X && q1.Y == q2.Y)
            {
                return q1.Z != q2.Z;
            }

            return true;
        }

        public override string ToString()
        {
            return $"<{X}, {Y}, {Z}, {W}>";
        }

        public static Quaternion Parse(string text)
        {
            string[] array = text.Trim('{', '}').Split(',');
            if (array.Length != 4)
            {
                return Identity;
            }

            return new Quaternion(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Quaternion))
            {
                return false;
            }

            return Equals((Quaternion)obj);
        }

        public bool Equals(Quaternion other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public float Dot(Quaternion q)
        {
            return X * q.X + Y * q.Y + Z * q.Z + W * q.W;
        }

        public Quaternion Conjugate()
        {
            return new Quaternion(0f - X, 0f - Y, 0f - Z, W);
        }

        public float Norm()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        public Quaternion Inverse()
        {
            return Conjugate() / Norm();
        }

        private static Quaternion Normalize(Quaternion q)
        {
            return q / q.Length;
        }

        public Quaternion Normalize()
        {
            return this / Length;
        }

        public static Quaternion Log(Quaternion q)
        {
            float num = (float)Maths.Acos(q.W);
            float num2 = (float)Maths.Sin(num);
            if (num2 > 0f)
            {
                return new Quaternion(num * q.X / num2, num * q.Y / num2, num * q.Z / num2, 0f);
            }

            return new Quaternion(q.X, q.Y, q.Z, 0f);
        }

        public static Quaternion Exp(Quaternion q)
        {
            float num = (float)Maths.Sqrt(q.X * q.X + q.Y * q.Y + q.Z * q.Z);
            float num2 = (float)Maths.Sin(num);
            float num3 = (float)Maths.Cos(num);
            if (num > 0f)
            {
                return new Quaternion(num2 * q.X / num, num2 * q.Y / num, num2 * q.Z / num, num3);
            }

            return new Quaternion(q.X, q.Y, q.Z, num3);
        }

        public static Quaternion Lerp(Quaternion q1, Quaternion q2, float t)
        {
            return Normalize(q1 + t * (q1 - q2));
        }

        public static Quaternion Slerp(Quaternion q1, Quaternion q2, float t)
        {
            float num = q1.Dot(q2);
            float num2 = 1f;
            if (num < 0f)
            {
                num = 0f - num;
                num2 = -1f;
            }

            float num5;
            float num6;
            if (num < 0.999f)
            {
                float num3 = (float)Maths.Acos(num);
                float num4 = 1f / (float)Maths.Sqrt(1f - num * num);
                num5 = (float)Maths.Sin((1f - t) * num3) * num4;
                num6 = (float)Maths.Sin(t * num3) * num4;
            }
            else
            {
                num5 = 1f - t;
                num6 = t;
            }

            return num5 * q1 + num2 * num6 * q2;
        }

        public static Quaternion Squad(Quaternion q1, Quaternion q2, Quaternion ta, Quaternion tb, float t)
        {
            float t2 = 2f * t * (1f - t);
            Quaternion q3 = Slerp(q1, q2, t);
            Quaternion q4 = Slerp(ta, tb, t);
            return Slerp(q3, q4, t2);
        }

        public static Quaternion SimpleSquad(Quaternion prev, Quaternion q1, Quaternion q2, Quaternion post, float t)
        {
            if (prev.Dot(q1) < 0f)
            {
                q1 = -q1;
            }

            if (q1.Dot(q2) < 0f)
            {
                q2 = -q2;
            }

            if (q2.Dot(post) < 0f)
            {
                post = -post;
            }

            Quaternion ta = Spline(prev, q1, q2);
            Quaternion tb = Spline(q1, q2, post);
            return Squad(q1, q2, ta, tb, t);
        }

        public static Quaternion Spline(Quaternion pre, Quaternion q, Quaternion post)
        {
            Quaternion quaternion = q.Conjugate();
            return q * Exp((Log(quaternion * pre) + Log(quaternion * post)) * -0.25f);
        }

        public static Quaternion FromAngleAxis(float Angle, Vector3 Axis)
        {
            if (Axis.LengthSquared() == 0f)
            {
                return Identity;
            }

            return new Quaternion(new Vector4(Axis.Normalize() * (float)Maths.Sin(Angle * 0.5f), (float)Maths.Cos(Angle * 0.5f)));
        }

        public Vector4 ToAxisAngle()
        {
            Quaternion quaternion = this;
            if (quaternion.W > 1f)
            {
                quaternion.Normalize();
            }

            float num = 2f * (float)Maths.Acos(quaternion.W);
            float num2 = (float)Maths.Sqrt(1.0 - (double)(quaternion.W * quaternion.W));
            if (num2 > 0.0001f)
            {
                return new Vector4(new Vector3(quaternion.X, quaternion.Y, quaternion.Z) / num2, num);
            }

            return new Vector4(Vector3.UnitX, num);
        }

        /// <summary>Creates a quaternion from the specified rotation matrix.</summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The newly created quaternion.</returns>
        public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
        {
            float trace = matrix.M11 + matrix.M22 + matrix.M33;

            Quaternion q = default;

            if (trace > 0.0f)
            {
                float s = MathF.Sqrt(trace + 1.0f);
                q.W = s * 0.5f;
                s = 0.5f / s;
                q.X = (matrix.M23 - matrix.M32) * s;
                q.Y = (matrix.M31 - matrix.M13) * s;
                q.Z = (matrix.M12 - matrix.M21) * s;
            }
            else
            {
                if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
                {
                    float s = MathF.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
                    float invS = 0.5f / s;
                    q.X = 0.5f * s;
                    q.Y = (matrix.M12 + matrix.M21) * invS;
                    q.Z = (matrix.M13 + matrix.M31) * invS;
                    q.W = (matrix.M23 - matrix.M32) * invS;
                }
                else if (matrix.M22 > matrix.M33)
                {
                    float s = MathF.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
                    float invS = 0.5f / s;
                    q.X = (matrix.M21 + matrix.M12) * invS;
                    q.Y = 0.5f * s;
                    q.Z = (matrix.M32 + matrix.M23) * invS;
                    q.W = (matrix.M31 - matrix.M13) * invS;
                }
                else
                {
                    float s = MathF.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
                    float invS = 0.5f / s;
                    q.X = (matrix.M31 + matrix.M13) * invS;
                    q.Y = (matrix.M32 + matrix.M23) * invS;
                    q.Z = 0.5f * s;
                    q.W = (matrix.M12 - matrix.M21) * invS;
                }
            }

            return q;
        }

        /// <summary>Creates a new quaternion from the given yaw, pitch, and roll.</summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X axis.</param>
        /// <param name="roll">The roll angle, in radians, around the Z axis.</param>
        /// <returns>The resulting quaternion.</returns>
        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            //  Roll first, about axis the object is facing, then
            //  pitch upward, then yaw to face into the new heading
            float sr, cr, sp, cp, sy, cy;

            float halfRoll = roll * 0.5f;
            sr = MathF.Sin(halfRoll);
            cr = MathF.Cos(halfRoll);

            float halfPitch = pitch * 0.5f;
            sp = MathF.Sin(halfPitch);
            cp = MathF.Cos(halfPitch);

            float halfYaw = yaw * 0.5f;
            sy = MathF.Sin(halfYaw);
            cy = MathF.Cos(halfYaw);

            Quaternion result;

            result.X = cy * sp * cr + sy * cp * sr;
            result.Y = sy * cp * cr - cy * sp * sr;
            result.Z = cy * cp * sr - sy * sp * cr;
            result.W = cy * cp * cr + sy * sp * sr;

            return result;
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
