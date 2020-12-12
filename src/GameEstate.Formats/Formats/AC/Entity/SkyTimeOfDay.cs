using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SkyTimeOfDay
    {
        public readonly float Begin;

        public readonly float DirBright;
        public readonly float DirHeading;
        public readonly float DirPitch;
        public readonly uint DirColor;

        public readonly float AmbBright;
        public readonly uint AmbColor;

        public readonly float MinWorldFog;
        public readonly float MaxWorldFog;
        public readonly uint WorldFogColor;
        public readonly uint WorldFog;

        public readonly SkyObjectReplace[] SkyObjReplace;

        public SkyTimeOfDay(BinaryReader r)
        {
            Begin = r.ReadSingle();

            DirBright = r.ReadSingle();
            DirHeading = r.ReadSingle();
            DirPitch = r.ReadSingle();
            DirColor = r.ReadUInt32();

            AmbBright = r.ReadSingle();
            AmbColor = r.ReadUInt32();

            MinWorldFog = r.ReadSingle();
            MaxWorldFog = r.ReadSingle();
            WorldFogColor = r.ReadUInt32();
            WorldFog = r.ReadUInt32();

            r.AlignBoundary();
            SkyObjReplace = r.ReadL32Array(x => new SkyObjectReplace(x));
        }
    }
}
