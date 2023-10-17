namespace Engine
{
    [Serializable] public struct Vector2 : IEquatable<Vector2>
    {
        public float x;

        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        #region  Operators

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator -(Vector2 v, float s)
        {
            return new Vector2(v.x - s, v.y - s);
        }

        public static Vector2 operator -(float s, Vector2 v)
        {
            return new Vector2(s - v.x, s - v.y);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(0f - v.x, 0f - v.y);
        }

        #endregion  Operators









        // Below are the 'standard' definitions from the class which haven't been used yet and may be removed

        /*        [Obsolete("Use X instead, which is compatible with System.Numerics.")]
                public float x
                {
                    get
                    {
                        return x;
                    }
                    set
                    {
                        x = value;
                    }
                }*/

        /*        [Obsolete("Use Y instead, which is compatible with System.Numerics.")]
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
        */
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
                    1 => y,
                    0 => x,
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
            set
            {
                switch (a)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public float Length => (float)Maths.Sqrt(x * x + y * y);

        [Obsolete("Use LengthSquared() instead, which is compatible with System.Numerics.")]
        public float SquaredLength => x * x + y * y;

           public Vector2 PerpendicularRight => new Vector2(y, 0f - x);

           public Vector2 PerpendicularLeft => new Vector2(0f - y, x);

           public static Vector2 operator +(Vector2 v1, Vector2 v2)
           {
               return new Vector2(v1.x + v2.x, v1.y + v2.y);
           }

           public static Vector2 operator +(Vector2 v, float s)
           {
               return new Vector2(v.x + s, v.y + s);
           }

           public static Vector2 operator +(float s, Vector2 v)
           {
               return new Vector2(v.x + s, v.y + s);
           }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }

        public static Vector2 operator *(float s, Vector2 v)
        {
            return new Vector2(v.x * s, v.y * s);
        }

        public static Vector2 operator *(Vector2 v, float s)
        {
            return new Vector2(v.x * s, v.y * s);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x / v2.x, v1.y / v2.y);
        }

        public static Vector2 operator /(float s, Vector2 v)
        {
            return new Vector2(s / v.x, s / v.y);
        }

        public static Vector2 operator /(Vector2 v, float s)
        {
            return new Vector2(v.x / s, v.y / s);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            if (v1.x == v2.x)
            {
                return v1.y == v2.y;
            }

            return false;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            if (v1.x == v2.x)
            {
                return v1.y != v2.y;
            }

            return true;
        }

        [Obsolete("Use floats instead, which is compatible with System.Numerics.")]
        public Vector2(double x, double y)
        {
            this.x = (float)x;
            this.y = (float)y;
        }

        public Vector2(float value)
        {
            x = (y = value);
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
            return $"<{x}, {y}>";
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
            return x * x + y * y;
        }

        public float[] ToFloat()
        {
            return new float[2] { x, y };
        }

        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
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

            return new Vector2(x, y) / Length;
        }

        public Vector2 Truncate()
        {
            float num = (((double)Maths.Abs(x) - 0.0001 < 0.0) ? 0f : x);
            float num2 = (((double)Maths.Abs(y) - 0.0001 < 0.0) ? 0f : y);
            return new Vector2(num, num2);
        }

        public void TakeMin(Vector2 v)
        {
            if (v.x < x)
            {
                x = v.x;
            }

            if (v.y < y)
            {
                y = v.y;
            }
        }

        public void TakeMax(Vector2 v)
        {
            if (v.x > x)
            {
                x = v.x;
            }

            if (v.y > y)
            {
                y = v.y;
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