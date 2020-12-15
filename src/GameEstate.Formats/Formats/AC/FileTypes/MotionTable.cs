using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using GameEstate.Formats.AC.Props;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.MotionTable)]
    public class MotionTable : AbstractFileType, IGetExplorerInfo
    {
        public static Dictionary<ushort, MotionCommand> RawToInterpreted = Enum.GetValues(typeof(MotionCommand)).Cast<object>().ToDictionary(x => (ushort)(uint)x, x => (MotionCommand)x);
        public readonly uint DefaultStyle;
        public readonly Dictionary<uint, uint> StyleDefaults;
        public readonly Dictionary<uint, MotionData> Cycles;
        public readonly Dictionary<uint, MotionData> Modifiers;
        public readonly Dictionary<uint, Dictionary<uint, MotionData>> Links;

        public MotionTable(BinaryReader r)
        {
            Id = r.ReadUInt32();
            DefaultStyle = r.ReadUInt32();
            StyleDefaults = r.ReadL32Many<uint, uint>(sizeof(uint), x => x.ReadUInt32());
            Cycles = r.ReadL32Many<uint, MotionData>(sizeof(uint), x => new MotionData(x));
            Modifiers = r.ReadL32Many<uint, MotionData>(sizeof(uint), x => new MotionData(x));
            Links = r.ReadL32Many<uint, Dictionary<uint, MotionData>>(sizeof(uint), x => x.ReadL32Many<uint, MotionData>(sizeof(uint), y => new MotionData(y)));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(MotionTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode($"Default style: {(MotionCommand)DefaultStyle}"),
                    new ExplorerInfoNode("Style defaults", items: StyleDefaults.Select(x => new ExplorerInfoNode($"{(MotionCommand)x.Key}: {(MotionCommand)x.Value}"))),
                    new ExplorerInfoNode("Cycles", items: Cycles.Select(x => new ExplorerInfoNode($"{x.Key:X8}", items: (x.Value as IGetExplorerInfo).GetInfoNodes()))),
                    new ExplorerInfoNode("Modifiers", items: Modifiers.Select(x => new ExplorerInfoNode($"{x.Key:X8}", items: (x.Value as IGetExplorerInfo).GetInfoNodes()))),
                    new ExplorerInfoNode("Links", items: Links.Select(x => new ExplorerInfoNode($"{x.Key:X8}", items: x.Value.Select(y => new ExplorerInfoNode($"{y.Key}", items: (y.Value as IGetExplorerInfo).GetInfoNodes()))))),
                })
            };
            return nodes;
        }

        /// <summary>
        /// Gets the default style for the requested MotionStance
        /// </summary>
        /// <returns>The default style or MotionCommand.Invalid if not found</returns>
        MotionCommand GetDefaultMotion(MotionStance style) => StyleDefaults.TryGetValue((uint)style, out var z) ? (MotionCommand)z : MotionCommand.Invalid;
        public float GetAnimationLength(MotionCommand motion) => GetAnimationLength((MotionStance)DefaultStyle, motion, GetDefaultMotion((MotionStance)DefaultStyle));
        public float GetAnimationLength(MotionStance stance, MotionCommand motion, MotionCommand? currentMotion = null) => GetAnimationLength(stance, motion, currentMotion ?? GetDefaultMotion(stance));

        public float GetCycleLength(MotionStance stance, MotionCommand motion)
        {
            var key = (uint)stance << 16 | (uint)motion & 0xFFFFF;
            if (!Cycles.TryGetValue(key, out var motionData) || motionData == null)
                return 0.0f;
            var length = 0.0f;
            foreach (var anim in motionData.Anims)
                length += GetAnimationLength(anim);
            return length;
        }

        /*
public List<float> GetAttackFrames(uint motionTableId, MotionStance stance, MotionCommand motion, MotionCommand? currentMotion = null)
{
    var motionTable = DatManager.PortalDat.ReadFromDat<MotionTable>(motionTableId);

    var animData = GetAnimData(stance, motion, currentMotion ?? GetDefaultMotion(stance));

    var frameNums = new List<int>();
    var totalFrames = 0;

    foreach (var anim in animData)
    {
        var animation = DatManager.PortalDat.ReadFromDat<Animation>(anim.AnimId);

        foreach (var frame in animation.PartFrames)
        {
            foreach (var hook in frame.Hooks)
            {
                if (hook.HookType == AnimationHookType.Attack)
                    frameNums.Add(totalFrames);
            }
            totalFrames++;
        }
    }
    var attackFrames = new List<float>();
    foreach (var frameNum in frameNums)
        attackFrames.Add((float)frameNum / totalFrames);    // div 0?

    // cache?
    return attackFrames;
}
*/

        public AnimData[] GetAnimData(MotionStance stance, MotionCommand motion, MotionCommand currentMotion)
        {
            var animData = new AnimData[0];
            var key = (uint)stance << 16 | (uint)currentMotion & 0xFFFFF;
            if (!Links.TryGetValue(key, out var link) || link == null)
                return animData;
            if (!link.TryGetValue((uint)motion, out var motionData) || motionData == null)
            {
                key = (uint)stance << 16;
                if (!Links.TryGetValue(key, out link) || link == null)
                    return animData;
                if (!link.TryGetValue((uint)motion, out motionData) || motionData == null)
                    return animData;
            }
            return motionData.Anims;
        }

        public float GetAnimationLength(MotionStance stance, MotionCommand motion, MotionCommand currentMotion)
        {
            var animData = GetAnimData(stance, motion, currentMotion);
            var length = 0.0f;
            foreach (var anim in animData)
                length += GetAnimationLength(anim);
            return length;
        }

        public float GetAnimationLength(AnimData anim)
        {
            throw new NotImplementedException();
            //var highFrame = anim.HighFrame;

            //// get the maximum # of animation frames
            //var animation = DatManager.PortalDat.ReadFromDat<Animation>(anim.AnimId);

            //if (anim.HighFrame == -1)
            //    highFrame = (int)animation.NumFrames;

            //if (highFrame > animation.NumFrames)
            //{
            //    // magic windup for level 6 spells appears to be the only animation w/ bugged data
            //    //Console.WriteLine($"MotionTable.GetAnimationLength({anim}): highFrame({highFrame}) > animation.NumFrames({animation.NumFrames})");
            //    highFrame = (int)animation.NumFrames;
            //}

            //var numFrames = highFrame - anim.LowFrame;

            //return numFrames / Math.Abs(anim.Framerate); // framerates can be negative, which tells the client to play in reverse
        }

        /*
public ACE.Entity.Position GetAnimationFinalPositionFromStart(ACE.Entity.Position position, float objScale, MotionCommand motion)
{
    MotionStance defaultStyle = (MotionStance)DefaultStyle;

    // get the default motion for the default
    MotionCommand defaultMotion = GetDefaultMotion(defaultStyle);
    return GetAnimationFinalPositionFromStart(position, objScale, defaultMotion, defaultStyle, motion);
}

public ACE.Entity.Position GetAnimationFinalPositionFromStart(ACE.Entity.Position position, float objScale, MotionCommand currentMotionState, MotionStance style, MotionCommand motion)
{
    float length = 0; // init our length var...will return as 0 if not found

    ACE.Entity.Position finalPosition = new ACE.Entity.Position();

    uint motionHash = ((uint)currentMotionState & 0xFFFFFF) | ((uint)style << 16);

    if (Links.ContainsKey(motionHash))
    {
        Dictionary<uint, MotionData> links = Links[motionHash];

        if (links.ContainsKey((uint)motion))
        {
            // loop through all that animations to get our total count
            for (int i = 0; i < links[(uint)motion].Anims.Count; i++)
            {
                AnimData anim = links[(uint)motion].Anims[i];

                uint numFrames;

                // check if the animation is set to play the whole thing, in which case we need to get the numbers of frames in the raw animation
                if ((anim.LowFrame == 0) && (anim.HighFrame == -1))
                {
                    var animation = DatManager.PortalDat.ReadFromDat<Animation>(anim.AnimId);
                    numFrames = animation.NumFrames;

                    if (animation.PosFrames.Count > 0)
                    {
                        finalPosition = position;
                        var origin = new Vector3(position.PositionX, position.PositionY, position.PositionZ);
                        var orientation = new Quaternion(position.RotationX, position.RotationY, position.RotationZ, position.RotationW);
                        foreach (var posFrame in animation.PosFrames)
                        {
                            origin += Vector3.Transform(posFrame.Origin, orientation) * objScale;

                            orientation *= posFrame.Orientation;
                            orientation = Quaternion.Normalize(orientation);
                        }

                        finalPosition.PositionX = origin.X;
                        finalPosition.PositionY = origin.Y;
                        finalPosition.PositionZ = origin.Z;

                        finalPosition.RotationW = orientation.W;
                        finalPosition.RotationX = orientation.X;
                        finalPosition.RotationY = orientation.Y;
                        finalPosition.RotationZ = orientation.Z;
                    }
                    else
                        return position;
                }
                else
                    numFrames = (uint)(anim.HighFrame - anim.LowFrame);

                length += numFrames / Math.Abs(anim.Framerate); // Framerates can be negative, which tells the client to play in reverse
            }
        }
    }

    return finalPosition;
}
        */


        public MotionStance[] GetStances()
        {
            var stances = new HashSet<MotionStance>();

            foreach (var cycle in Cycles.Keys)
            {
                var stance = (MotionStance)(0x80000000 | cycle >> 16);
                if (!stances.Contains(stance))
                    stances.Add(stance);
            }
            if (stances.Count > 0 && !stances.Contains(MotionStance.Invalid))
                stances.Add(MotionStance.Invalid);
            return stances.ToArray();
        }

        public MotionCommand[] GetMotionCommands(MotionStance stance = MotionStance.Invalid)
        {
            var commands = new HashSet<MotionCommand>();
            foreach (var cycle in Cycles.Keys)
            {
                if (stance != MotionStance.Invalid && (cycle >> 16) != ((uint)stance & 0xFFFF))
                    continue;
                var rawCommand = (ushort)(cycle & 0xFFFF);
                var motionCommand = RawToInterpreted[rawCommand];
                if (!commands.Contains(motionCommand))
                    commands.Add(motionCommand);
            }
            return commands.ToArray();
        }

    }
}
