﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace GameEstate.Formats.Red.CR2W
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct CR2WName
    {
        [FieldOffset(0)] public uint value;
        [FieldOffset(4)] public uint hash;
    }

    [DataContract(Namespace = "")]
    public class CR2WNameWrapper
    {
        public CR2WName Name { get; set; }
        [XmlIgnore, NonSerialized()] readonly CR2WFile _cr2w;
        public string Str => _cr2w.StringDictionary[Name.value];

        public CR2WNameWrapper(CR2WName name, CR2WFile cr2w)
        {
            Name = name;
            _cr2w = cr2w;
        }

        public override string ToString() => Str;
    }
}