using System.Linq;
using System.Numerics;
using System.Text;

namespace System.Collections.Generic
{
    public static class KVExtensions
    {
        public static bool TryGet<T>(this IDictionary<string, object> collection, string name, out T value, T defaultValue = default)
        {
            if (collection.TryGetValue(name, out var z) && z is T v) { value = v; return true; }
            else { value = defaultValue; return false; }
        }

        public static T Get<T>(this IDictionary<string, object> collection, string name, T defaultValue = default)
            => collection.TryGetValue(name, out var z) && z is T v ? v : defaultValue;

        public static IDictionary<string, object> GetSub(this IDictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is IDictionary<string, object> v ? v : default;

        public static T[] GetArray<T>(this IDictionary<string, object> collection, string name, Func<IDictionary<string, object>, T> mapper)
            => collection.TryGetValue(name, out var z) && z is Array v ? v.Cast<IDictionary<string, object>>().Select(mapper).ToArray() : default;

        public static int GetInt32(this IDictionary<string, object> collection, string name, int defaultValue = default)
            => collection.TryGetValue(name, out var z) ? z is int i ? i : Convert.ToInt32(z) : defaultValue;

        public static uint GetUInt32(this IDictionary<string, object> collection, string name, uint defaultValue = default)
            => collection.TryGetValue(name, out var z) ? z is uint i ? i : Convert.ToUInt32(z) : defaultValue;

        public static long GetInt64(this IDictionary<string, object> collection, string name, long defaultValue = default)
            => collection.TryGetValue(name, out var z) ? z is long i ? i : Convert.ToInt64(z) : defaultValue;

        public static ulong GetUInt64(this IDictionary<string, object> collection, string name, ulong defaultValue = default)
        {
            unchecked
            {
                return collection.TryGetValue(name, out var value) ? value is int i ? (ulong)i : Convert.ToUInt64(value) : defaultValue;
            }
        }

        public static double GetDouble(this IDictionary<string, object> collection, string name, double defaultValue = default)
            => collection.TryGetValue(name, out var z) ? Convert.ToDouble(z) : defaultValue;

        public static float GetFloat(this IDictionary<string, object> collection, string name, float defaultValue = default)
            => (float)GetDouble(collection, name, defaultValue);

        public static long[] GetIntArray(this IDictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is Array v ? v.Cast<object>().Select(Convert.ToInt64).ToArray() : default;

        public static ulong[] GetUIntArray(this IDictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is Array v ? v.Cast<object>().Select(Convert.ToUInt64).ToArray() : default;

        public static IDictionary<string, object>[] GetArray(this IDictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is Array v ? v.Cast<IDictionary<string, object>>().ToArray() : default;


        public static Vector3 ToVector3(this IDictionary<string, object> collection) => new Vector3(
            collection.GetFloat("0"),
            collection.GetFloat("1"),
            collection.GetFloat("2"));
        public static Vector3 GetVector3(this IDictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is object[] v && v.Length == 3 ? new Vector3(
            (float)Convert.ToDouble(v[0]),
            (float)Convert.ToDouble(v[1]),
            (float)Convert.ToDouble(v[2])) : default;

        public static Vector4 ToVector4(this IDictionary<string, object> collection) => new Vector4(
            collection.GetFloat("0"),
            collection.GetFloat("1"),
            collection.GetFloat("2"),
            collection.GetFloat("3"));
        public static Vector4 GetVector4(this IDictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is object[] v && v.Length == 4 ? new Vector4(
            (float)Convert.ToDouble(v[0]),
            (float)Convert.ToDouble(v[1]),
            (float)Convert.ToDouble(v[2]),
            (float)Convert.ToDouble(v[3])) : default;

        public static Quaternion ToQuaternion(this IDictionary<string, object> collection) => new Quaternion(
            collection.GetFloat("0"),
            collection.GetFloat("1"),
            collection.GetFloat("2"),
            collection.GetFloat("3"));
        public static Quaternion GetQuaternion(this IDictionary<string, object> collection, string name)
            => collection.TryGetValue(name, out var z) && z is object[] v && v.Length == 4 ? new Quaternion(
            (float)Convert.ToDouble(v[0]),
            (float)Convert.ToDouble(v[1]),
            (float)Convert.ToDouble(v[2]),
            (float)Convert.ToDouble(v[3])) : default;

        public static Matrix4x4 ToMatrix4x4(this IDictionary<string, object>[] array)
        {
            var column1 = array[0].ToVector4();
            var column2 = array[1].ToVector4();
            var column3 = array[2].ToVector4();
            var column4 = array.Length > 3 ? array[3].ToVector4() : new Vector4(0, 0, 0, 1);
            return new Matrix4x4(column1.X, column2.X, column3.X, column4.X, column1.Y, column2.Y, column3.Y, column4.Y, column1.Z, column2.Z, column3.Z, column4.Z, column1.W, column2.W, column3.W, column4.W);
        }

        public static string Print(IDictionary<string, object> collection, int indent = 0)
        {
            var b = new StringBuilder();
            var space = new string(' ', indent * 4);
            foreach (var kvp in collection)
            {
                if (kvp.Value is IDictionary<string, object> nestedCollection)
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
