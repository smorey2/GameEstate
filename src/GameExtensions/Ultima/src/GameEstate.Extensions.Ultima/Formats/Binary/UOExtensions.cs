using System.IO;
using UnityEngine;

namespace GameEstate.Formats.Binary
{
    public static class UOExtensions
    {
        public static void Write(this BinaryWriter source, Vector2Int value)
        {
            source.Write(value.x);
            source.Write(value.y);
        }

        public static Vector2Int ReadVector2Int(this BinaryReader source) => new Vector2Int(source.ReadInt32(), source.ReadInt32());
    }
}