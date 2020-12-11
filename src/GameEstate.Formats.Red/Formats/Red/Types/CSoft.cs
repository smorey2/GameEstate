﻿using GameEstate.Formats.Red.CR2W;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GameEstate.Formats.Red.Types
{
    /// <summary>
    /// CSofts are Uint16 references to the imports table of a cr2w file
    /// Imports are paths to a file in the tw3 filesystem
    /// and can be set manually by DepotPath and Classname
    /// Imports have flags which are set on write
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [REDMeta()]
    public class CSoft<T> : CVariable, ISoftAccessor where T : CVariable
    {
        public CSoft(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }

        [DataMember(EmitDefaultValue = false)] public string DepotPath { get; set; }
        [DataMember(EmitDefaultValue = false)] public string ClassName { get; set; }
        [DataMember(EmitDefaultValue = false)] public ushort Flags { get; set; }

        public override void Read(BinaryReader r, uint size) => SetValueInternal(r.ReadUInt16());

        void SetValueInternal(ushort value)
        {
            if (value > 0)
            {
                DepotPath = cr2w.Imports[value - 1].DepotPathStr;
                var filetype = cr2w.Imports[value - 1].Import.className;
                ClassName = cr2w.Names[filetype].Str;
                Flags = cr2w.Imports[value - 1].Import.flags;
            }
            else
            {
                DepotPath = "";
                ClassName = "";
                Flags = 4;
            }
        }

        /// <summary>
        /// Call after the stringtable was generated!
        /// </summary>
        /// <param name="w"></param>
        public override void Write(BinaryWriter w)
        {
            ushort val = 0;
            var import = cr2w.Imports.FirstOrDefault(_ => _.DepotPathStr == DepotPath && _.ClassNameStr == ClassName && _.Flags == Flags);
            val = (ushort)(cr2w.Imports.IndexOf(import) + 1);
            w.Write(val);
        }

        public override CVariable SetValue(object val)
        {
            switch (val)
            {
                case ushort o: SetValueInternal(o); break;
                case ISoftAccessor cvar:
                    DepotPath = cvar.DepotPath;
                    ClassName = cvar.ClassName;
                    Flags = cvar.Flags;
                    break;
            }
            return this;
        }

        public override CVariable Copy(CR2WCopyAction context)
        {
            var copy = (CSoft<T>)base.Copy(context);
            copy.ClassName = ClassName;
            copy.Flags = Flags;
            copy.DepotPath = DepotPath;
            return copy;
        }

        public override string ToString() => $"{ClassName}: {DepotPath}";
    }
}