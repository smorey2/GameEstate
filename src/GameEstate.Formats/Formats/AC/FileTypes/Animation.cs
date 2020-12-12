using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x03. 
    /// Special thanks to Dan Skorupski for his work on Bael'Zharon's Respite, which helped fill in some of the gaps https://github.com/boardwalk/bzr
    /// </summary>
    [PakFileType(PakFileType.Animation)]
    public class Animation : FileType
    {
        public AnimationFlags Flags { get; private set; }
        public uint NumParts { get; private set; }
        public uint NumFrames { get; private set; }
        public List<Frame> PosFrames { get; } = new List<Frame>();
        public List<AnimationFrame> PartFrames { get; } = new List<AnimationFrame>();

        public override void Read(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Flags = (AnimationFlags)r.ReadUInt32();
            NumParts = r.ReadUInt32();
            NumFrames = r.ReadUInt32();
            if ((Flags & AnimationFlags.PosFrames) != 0)
                PosFrames.Unpack(r, NumFrames);
            for (var i = 0U; i < NumFrames; i++)
            {
                var animationFrame = new AnimationFrame();
                animationFrame.Unpack(r, NumParts);
                PartFrames.Add(animationFrame);
            }
        }
    }
}
