﻿using GameEstate.Core;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace GameEstate.Graphics
{
    /// <summary>
    /// Color
    /// </summary>
    /// <seealso cref="System.IEquatable{GameEstate.Graphics.Color}" />
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        /// <summary>
        /// The r
        /// </summary>
        public float R;
        /// <summary>
        /// The g
        /// </summary>
        public float G;
        /// <summary>
        /// The b
        /// </summary>
        public float B;
        /// <summary>
        /// a
        /// </summary>
        public float A;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a">a.</param>
        public Color(float r, float g, float b, float a = 1f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static implicit operator Vector4(Color c) => new Vector4(c.R, c.G, c.B, c.A);
        public static implicit operator Color(Vector4 v) => new Color(v.X, v.Y, v.Z, v.W);
        public static Color operator +(Color a, Color b) => new Color(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
        public static Color operator -(Color a, Color b) => new Color(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
        public static Color operator *(Color a, Color b) => new Color(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
        public static Color operator *(Color a, float b) => new Color(a.R * b, a.G * b, a.B * b, a.A * b);
        public static Color operator *(float b, Color a) => new Color(a.R * b, a.G * b, a.B * b, a.A * b);
        public static Color operator /(Color a, float b) => new Color(a.R / b, a.G / b, a.B / b, a.A / b);
        public static bool operator ==(Color lhs, Color rhs) => (lhs == rhs);
        public static bool operator !=(Color lhs, Color rhs) => !(lhs == rhs);

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return R;
                    case 1: return G;
                    case 2: return B;
                    case 3: return A;
                    default: throw new IndexOutOfRangeException("Invalid Vector3 index");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: R = value; break;
                    case 1: G = value; break;
                    case 2: B = value; break;
                    case 3: A = value; break;
                    default: throw new IndexOutOfRangeException("Invalid Vector3 index");
                }
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"RGBA({R:F3}, {G:F3}, {B:F3}, {A:F3})";
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(string format) => $"RGBA({R.ToString(format)}, {G.ToString(format)}, {B.ToString(format)}, {A.ToString(format)})";

        public override bool Equals(object other) => other is Color color && Equals(color);
        public bool Equals(Color other) => R.Equals(other.R) && Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);

        public static Color Lerp(Color a, Color b, float t)
        {
            t = Mathx.Clamp(t);
            return new Color(a.R + ((b.R - a.R) * t), a.G + ((b.G - a.G) * t), a.B + ((b.B - a.B) * t), a.A + ((b.A - a.A) * t));
        }

        public static Color LerpUnclamped(Color a, Color b, float t) => new Color(a.R + ((b.R - a.R) * t), a.G + ((b.G - a.G) * t), a.B + ((b.B - a.B) * t), a.A + ((b.A - a.A) * t));

        internal Color RGBMultiplied(float multiplier) => new Color(R * multiplier, G * multiplier, B * multiplier, A);

        internal Color AlphaMultiplied(float multiplier) => new Color(R, G, B, A * multiplier);

        internal Color RGBMultiplied(Color multiplier) => new Color(R * multiplier.R, G * multiplier.G, B * multiplier.B, A);

        public static Color Red => new Color(1f, 0f, 0f, 1f);
        public static Color Green => new Color(0f, 1f, 0f, 1f);
        public static Color Blue => new Color(0f, 0f, 1f, 1f);
        public static Color White => new Color(1f, 1f, 1f, 1f);
        public static Color Black => new Color(0f, 0f, 0f, 1f);
        public static Color Yellow => new Color(1f, 0.9215686f, 0.01568628f, 1f);
        public static Color Cyan => new Color(0f, 1f, 1f, 1f);
        public static Color Magenta => new Color(1f, 0f, 1f, 1f);
        public static Color Gray => new Color(0.5f, 0.5f, 0.5f, 1f);
        public static Color Clear => new Color(0f, 0f, 0f, 0f);

        public float Grayscale => (((0.299f * R) + (0.587f * G)) + (0.114f * B));

        //public Color Linear => new Color(Mathx.GammaToLinearSpace(R), Mathx.GammaToLinearSpace(G), Mathx.GammaToLinearSpace(B), A);
        //public Color Gamma => new Color(Mathx.LinearToGammaSpace(R), Mathx.LinearToGammaSpace(G), Mathx.LinearToGammaSpace(B), A);

        public float MaxColorComponent => Math.Max(Math.Max(R, G), B);

        #region HSV

        public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V)
        {
            if (rgbColor.B > rgbColor.G && rgbColor.B > rgbColor.R) RGBToHSVHelper(4f, rgbColor.B, rgbColor.R, rgbColor.G, out H, out S, out V);
            else if (rgbColor.G > rgbColor.R) RGBToHSVHelper(2f, rgbColor.G, rgbColor.B, rgbColor.R, out H, out S, out V);
            else RGBToHSVHelper(0f, rgbColor.R, rgbColor.G, rgbColor.B, out H, out S, out V);
        }

        static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V)
        {
            V = dominantcolor;
            if (V == 0f)
            {
                S = 0f;
                H = 0f;
            }
            else
            {
                var color = colorone <= colortwo ? colorone : colortwo;
                var color2 = V - color;
                if (color2 != 0f) { S = color2 / V; H = offset + ((colorone - colortwo) / color2); }
                else { S = 0f; H = offset + (colorone - colortwo); }
                H /= 6f;
                if (H < 0f)
                    H++;
            }
        }

        public static Color HSVToRGB(float h, float s, float v) => HSVToRGB(h, s, v, true);

        public static Color HSVToRGB(float h, float s, float v, bool hdr)
        {
            var white = White;
            if (s == 0f) { white.R = v; white.G = v; white.B = v; }
            else if (v == 0f) { white.R = 0f; white.G = 0f; white.B = 0f; }
            else
            {
                white.R = 0f; white.G = 0f; white.B = 0f;
                var r0 = v;
                var f = h * 6f;
                var whole = (int)Math.Floor(f);
                var remain = f - whole;
                var r1 = r0 * (1f - s);
                var r2 = r0 * (1f - (s * remain));
                var r3 = r0 * (1f - (s * (1f - remain)));
                switch (whole)
                {
                    case -1: white.R = r0; white.G = r1; white.B = r2; break;
                    case 0: white.R = r0; white.G = r3; white.B = r1; break;
                    case 1: white.R = r2; white.G = r0; white.B = r1; break;
                    case 2: white.R = r1; white.G = r0; white.B = r3; break;
                    case 3: white.R = r1; white.G = r2; white.B = r0; break;
                    case 4: white.R = r3; white.G = r1; white.B = r0; break;
                    case 5: white.R = r0; white.G = r1; white.B = r2; break;
                    case 6: white.R = r0; white.G = r3; white.B = r1; break;
                    default: break;
                }
                if (!hdr) { white.R = Mathx.Clamp(white.R, 0f, 1f); white.G = Mathx.Clamp(white.G, 0f, 1f); white.B = Mathx.Clamp(white.B, 0f, 1f); }
            }
            return white;
        }

        #endregion
    }
}