using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GameEstate.Toy
{
    public static class KVExtensions
    {
        public static bool TryGet<T>(this Dictionary<string, object> collection, string name, out T value, T defaultValue = default)
        {
            if (collection.TryGetValue(name, out var z) && z is T v) { value = v; return true; }
            else { value = defaultValue; return false; }
        }

        public static T Get<T>(this Dictionary<string, object> collection, string name, T defaultValue = default)
            => collection.TryGetValue(name, out var z) && z is T v ? v : defaultValue;

        public static Dictionary<string, object> GetSub(this Dictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is Dictionary<string, object> v ? v : default;

        public static T[] GetArray<T>(this Dictionary<string, object> collection, string name, Func<Dictionary<string, object>, T> mapper)
            => collection.TryGetValue(name, out var z) && z is Dictionary<string, object>[] v ? v.Select(mapper).ToArray() : default;

        public static long GetInt(this Dictionary<string, object> collection, string name, long defaultValue = default)
            => collection.TryGetValue(name, out var z) ? Convert.ToInt64(z) : defaultValue;

        public static ulong GetUInt(this Dictionary<string, object> collection, string name, ulong defaultValue = default)
        {
            unchecked
            {
                return collection.TryGetValue(name, out var value) ? value is int i ? (ulong)i : Convert.ToUInt64(value) : defaultValue;
            }
        }

        public static double GetDouble(this Dictionary<string, object> collection, string name, double defaultValue = default)
            => collection.TryGetValue(name, out var z) ? Convert.ToDouble(z) : defaultValue;

        public static float GetFloat(this Dictionary<string, object> collection, string name, float defaultValue = default)
            => (float)GetDouble(collection, name, defaultValue);

        public static long[] GetIntArray(this Dictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is Array v ? v.Cast<object>().Select(Convert.ToInt64).ToArray() : default;

        public static ulong[] GetUIntArray(this Dictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is Array v ? v.Cast<object>().Select(Convert.ToUInt64).ToArray() : default;

        public static Dictionary<string, object>[] GetArray(this Dictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is Dictionary<string, object>[] v ? v : default;

        public static Vector3 ToVector3(this Dictionary<string, object> collection) => new Vector3(
            collection.GetFloat("0"),
            collection.GetFloat("1"),
            collection.GetFloat("2"));

        public static Vector4 ToVector4(this Dictionary<string, object> collection) => new Vector4(
            collection.GetFloat("0"),
            collection.GetFloat("1"),
            collection.GetFloat("2"),
            collection.GetFloat("3"));

        public static Quaternion ToQuaternion(this Dictionary<string, object> collection) => new Quaternion(
            collection.GetFloat("0"),
            collection.GetFloat("1"),
            collection.GetFloat("2"),
            collection.GetFloat("3"));

        public static Matrix4x4 ToMatrix4x4(this Dictionary<string, object>[] array)
        {
            var column1 = array[0].ToVector4();
            var column2 = array[1].ToVector4();
            var column3 = array[2].ToVector4();
            var column4 = array.Length > 3 ? array[3].ToVector4() : new Vector4(0, 0, 0, 1);
            return new Matrix4x4(column1.X, column2.X, column3.X, column4.X, column1.Y, column2.Y, column3.Y, column4.Y, column1.Z, column2.Z, column3.Z, column4.Z, column1.W, column2.W, column3.W, column4.W);
        }

        public static string Print(Dictionary<string, object> collection, int indent = 0)
        {
            var b = new StringBuilder();
            var space = new string(' ', indent * 4);
            foreach (var kvp in collection)
            {
                if (kvp.Value is Dictionary<string, object> nestedCollection)
                {
                    b.AppendLine($"{space}{kvp.Key} = {{");
                    b.Append(Print(nestedCollection, indent + 1));
                    b.AppendLine($"{space}}}");
                }
                else
                    b.AppendLine($"{space}{kvp.Key} = {kvp.Value}");
            }
            return b.ToString();
        }
    }
}
