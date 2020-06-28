using System.Globalization;
using System.Numerics;

namespace GameEstate.Toy
{
    public static class VectorExtensions
    {
        public static OpenTK.Vector3 ToOpenTK(this Vector3 vec) => new OpenTK.Vector3(vec.X, vec.Y, vec.Z);

        public static OpenTK.Vector4 ToOpenTK(this Vector4 vec) => new OpenTK.Vector4(vec.X, vec.Y, vec.Z, vec.W);

        public static OpenTK.Matrix4 ToOpenTK(this Matrix4x4 m) => new OpenTK.Matrix4(m.M11, m.M12, m.M13, m.M14, m.M21, m.M22, m.M23, m.M24, m.M31, m.M32, m.M33, m.M34, m.M41, m.M42, m.M43, m.M44);

        public static Vector3 ParseVector(string input)
        {
            var split = input.Split(' ');
            return split.Length == 3
                ? new Vector3(float.Parse(split[0], CultureInfo.InvariantCulture), float.Parse(split[1], CultureInfo.InvariantCulture), float.Parse(split[2], CultureInfo.InvariantCulture))
                : default;
        }

        /// <summary>
        /// Flatten an array of matrices to an array of floats.
        /// </summary>
        public static float[] Flatten(this Matrix4x4[] matrices)
        {
            var r = new float[matrices.Length * 16];

            for (var i = 0; i < matrices.Length; i++)
            {
                var mat = matrices[i];
                r[i * 16] = mat.M11;
                r[(i * 16) + 1] = mat.M12;
                r[(i * 16) + 2] = mat.M13;
                r[(i * 16) + 3] = mat.M14;

                r[(i * 16) + 4] = mat.M21;
                r[(i * 16) + 5] = mat.M22;
                r[(i * 16) + 6] = mat.M23;
                r[(i * 16) + 7] = mat.M24;

                r[(i * 16) + 8] = mat.M31;
                r[(i * 16) + 9] = mat.M32;
                r[(i * 16) + 10] = mat.M33;
                r[(i * 16) + 11] = mat.M34;

                r[(i * 16) + 12] = mat.M41;
                r[(i * 16) + 13] = mat.M42;
                r[(i * 16) + 14] = mat.M43;
                r[(i * 16) + 15] = mat.M44;
            }

            return r;
        }
    }
}
