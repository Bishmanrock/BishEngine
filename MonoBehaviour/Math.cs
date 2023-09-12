using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Engine
{
    public class Math
    {
        /// <summary>
        /// Defines the value of Pi as a <see cref="T:System.Single" />.
        /// </summary>
        public const float Pi = MathF.PI;

        /// <summary>
        /// Defines the value of Pi divided by two as a <see cref="T:System.Single" />.
        /// </summary>
        public const float PiOver2 = MathF.PI / 2f;

        public double ConvertToRadians(double angle)
        {
            return (System.Math.PI / 180) * angle;
        }

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="degrees">An angle in degrees.</param>
        /// <returns>The angle expressed in radians.</returns>
        public static float DegreesToRadians(float degrees)
        {
            return degrees * (MathF.PI / 180f);
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
            return System.Math.Max(System.Math.Min(n, max), min);
        }

        // Returns the largest of two floats
        public static float Max(float a, float b)
        {
            return (a > b) ? a : b;
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
    }
}
