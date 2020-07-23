using System;

namespace GameEstate.Core
{
    public static class Mathx
    {
        #region Clamp

        public static int Clamp(int value, int min, int max)
            => value < min ? min
            : value > max ? max
            : value;

        public static float Clamp(float value, float min, float max)
            => value < min ? min
            : value > max ? max
            : value;

        public static float Clamp(float value)
            => value >= 0f ? value <= 1f
            ? value : 1f
            : 0f;

        #endregion

        #region Lerp

        public static float InverseLerp(float a, float b, float value) => a == b ? 0f : Clamp((value - a) / (b - a));

        public static float Lerp(float a, float b, float t) => a + ((b - a) * Clamp(t));

        public static float LerpAngle(float a, float b, float t)
        {
            var num = Repeat(b - a, 360f);
            if (num > 180f)
                num -= 360f;
            return (a + (num * Clamp(t)));
        }

        public static float LerpUnclamped(float a, float b, float t) => a + ((b - a) * t);

        #endregion

        public static float Repeat(float t, float length) => Clamp(t - ((float)Math.Floor(t / length) * length), 0f, length);
    }
}