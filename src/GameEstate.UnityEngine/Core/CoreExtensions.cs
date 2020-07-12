using System;
using UnityEngine;

namespace GameEstate.Core
{
    public static class CoreExtensions
    {
        #region Convert Color

        public static Color B565ToColor(this ushort B565)
        {
            var R5 = (B565 >> 11) & 31;
            var G6 = (B565 >> 5) & 63;
            var B5 = B565 & 31;
            return new Color((float)R5 / 31, (float)G6 / 63, (float)B5 / 31, 1);
        }

        public static Color32 B565ToColor32(this ushort B565) => B565ToColor(B565);

        #endregion
    }
}