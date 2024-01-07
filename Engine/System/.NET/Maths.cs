using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

// The Maths class provides various mathematical functions.

namespace Engine
{
    public class Maths
    {
        /// <summary>
        /// Defines the value of Pi as a <see cref="T:System.Single" />.
        /// </summary>
        public const float Pi = 3.14159265f;

        /// <summary>
        /// Defines the value of Pi divided by two as a <see cref="T:System.Single" />.
        /// </summary>
        public const float PiOver2 = Pi / 2f;

        public const float PiOver4 = Pi / 4f;

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float Sin(float x);

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="degrees">An angle in degrees.</param>
        /// <returns>The angle expressed in radians.</returns>
        public static float DegreesToRadians(float degrees)
        {
            return degrees * (Pi / 180f);
        }

        /// <summary>
        /// Convert radians to degrees.
        /// </summary>
        /// <param name="radians">An angle in radians.</param>
        /// <returns>The angle expressed in degrees.</returns>
        public static float RadiansToDegrees(float radians)
        {
            return radians * (180f / MathF.PI);
        }

        /// <summary>
        /// Clamps a number between a minimum and a maximum.
        /// </summary>
        /// <param name="n">The number to clamp.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <returns>min, if n is lower than min; max, if n is higher than max; n otherwise.</returns>
        public static float Clamp(float n, float min, float max)
        {
            return Max(Min(n, max), min);
        }

        // Returns the largest of two floats
        public static float Max(float a, float b)
        {
            return (a > b) ? a : b;
        }

        //
        // Summary:
        //     Returns the smaller of two single-precision floating-point numbers.
        //
        // Parameters:
        //   val1:
        //     The first of two single-precision floating-point numbers to compare.
        //
        //   val2:
        //     The second of two single-precision floating-point numbers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller. If val1, val2, or both val1 and
        //     val2 are equal to System.Single.NaN, System.Single.NaN is returned.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static float Min(float val1, float val2)
        {
            if (val1 < val2)
            {
                return val1;
            }

            if (float.IsNaN(val1))
            {
                return val1;
            }

            return val2;
        }










        // Below here are the standard class functions which have not been confirmed to be used in the engine

        public double ConvertToRadians(double angle)
        {
            return (System.Math.PI / 180) * angle;
        }

        // Returns largest of two or more values.
        public static float Max(params float[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0f;
            }

            float num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }

            return num2;
        }

        // Returns the largest of two or more values.
        public static int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // Returns the largest of two or more values.
        public static int Max(params int[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0;
            }

            int num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }

            return num2;
        }

        //
        // Summary:
        //     Returns the square root of a specified number.
        //
        // Parameters:
        //   d:a
        //     The number whose square root is to be found.
        //
        // Returns:
        //     One of the values in the following table. d parameter Return value Zero or positive
        //     The positive square root of d. Negative System.Double.NaN Equals System.Double.NaNSystem.Double.NaN
        //     Equals System.Double.PositiveInfinitySystem.Double.PositiveInfinity
        [MethodImpl(MethodImplOptions.InternalCall)]
        //[SecuritySafeCritical]
        //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static extern double Sqrt(double d);

        //
        // Summary:
        //     Returns the absolute value of a single-precision floating-point number.
        //
        // Parameters:
        //   value:
        //     A number that is greater than or equal to System.Single.MinValue, but less than
        //     or equal to System.Single.MaxValue.
        //
        // Returns:
        //     A single-precision floating-point number, x, such that 0 ≤ x ≤System.Single.MaxValue.
        [MethodImpl(MethodImplOptions.InternalCall)]
        [SecuritySafeCritical]
        public static extern float Abs(float value);

        //
        // Summary:
        //     Returns the angle whose cosine is the specified number.
        //
        // Parameters:
        //   d:
        //     A number representing a cosine, where d must be greater than or equal to -1,
        //     but less than or equal to 1.
        //
        // Returns:
        //     An angle, θ, measured in radians, such that 0 ≤θ≤π -or- System.Double.NaN if
        //     d < -1 or d > 1 or d equals System.Double.NaN.
        [MethodImpl(MethodImplOptions.InternalCall)]
        [SecuritySafeCritical]
        public static extern double Acos(double d);

        //
        // Summary:
        //     Returns the cosine of the specified angle.
        //
        // Parameters:
        //   d:
        //     An angle, measured in radians.
        //
        // Returns:
        //     The cosine of d. If d is equal to System.Double.NaN, System.Double.NegativeInfinity,
        //     or System.Double.PositiveInfinity, this method returns System.Double.NaN.
        [MethodImpl(MethodImplOptions.InternalCall)]
        [SecuritySafeCritical]
        public static extern double Cos(double d);

        //
        // Summary:
        //     Returns the sine of the specified angle.
        //
        // Parameters:
        //   a:
        //     An angle, measured in radians.
        //
        // Returns:
        //     The sine of a. If a is equal to System.Double.NaN, System.Double.NegativeInfinity,
        //     or System.Double.PositiveInfinity, this method returns System.Double.NaN.
        [MethodImpl(MethodImplOptions.InternalCall)]
        [SecuritySafeCritical]
        public static extern double Sin(double a);

        //
        // Summary:
        //     Returns the tangent of the specified angle.
        //
        // Parameters:
        //   a:
        //     An angle, measured in radians.
        //
        // Returns:
        //     The tangent of a. If a is equal to System.Double.NaN, System.Double.NegativeInfinity,
        //     or System.Double.PositiveInfinity, this method returns System.Double.NaN.
        [MethodImpl(MethodImplOptions.InternalCall)]
        [SecuritySafeCritical]
        public static extern double Tan(double a);

        //
        // Summary:
        //     Returns the smaller of two 8-bit signed integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 8-bit signed integers to compare.
        //
        //   val2:
        //     The second of two 8-bit signed integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        [CLSCompliant(false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static sbyte Min(sbyte val1, sbyte val2)
        {
            if (val1 > val2)
            {
                return val2;
            }

            return val1;
        }

        //
        // Summary:
        //     Returns the smaller of two 8-bit unsigned integers.
        //
        // Parameters:
        //   val1:
        //     The first of two 8-bit unsigned integers to compare.
        //
        //   val2:
        //     The second of two 8-bit unsigned integers to compare.
        //
        // Returns:
        //     Parameter val1 or val2, whichever is smaller.
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static byte Min(byte val1, byte val2)
        {
            if (val1 > val2)
            {
                return val2;
            }

            return val1;
        }
    }
}