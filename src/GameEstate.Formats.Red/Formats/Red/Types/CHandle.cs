﻿using GameEstate.Formats.Red.CR2W;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GameEstate.Formats.Red.Types
{
    /// <summary>
    /// Handles are Int32 that store 
    /// if > 0: a reference to a chunk inside the cr2w file (aka Soft)
    /// if < 0: a reference to a string in the imports table (aka Pointer)
    /// Exposed are 
    /// if ChunkHandle: 
    /// if ImportHandle: A string Handle, string Filetype and ushort Flags
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [REDMeta()]
    public class CHandle<T> : CVariable, IHandleAccessor where T : CVariable
    {
        public CHandle(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }

        [DataMember(EmitDefaultValue = false)] public bool ChunkHandle { get; set; }

        // Soft
        [DataMember(EmitDefaultValue = false)] public string DepotPath { get; set; }
        [DataMember(EmitDefaultValue = false)] public string ClassName { get; set; }

        [DataMember(EmitDefaultValue = false)] public ushort Flags { get; set; }

        // Pointer
        [DataMember(EmitDefaultValue = false)] public CR2WExportWrapper Reference { get; set; }

        public string ReferenceType => REDType.Split(':').Last();

        public void ChangeHandleType()
        {
            ChunkHandle = !ChunkHandle;
            // change to external path
            if (ChunkHandle) { }
            // change to chunk handle
            else { }
        }

        public override void Read(BinaryReader r, uint size) => SetValueInternal(r.ReadInt32());

        void SetValueInternal(int val)
        {
            if (val >= 0)
                ChunkHandle = true;
            if (ChunkHandle)
            {
                if (val == 0) Reference = null;
                else
                {
                    Reference = cr2w.Chunks[val - 1];
                    //Populate the reverse-lookups
                    Reference.AdReferences.Add(this);
                    cr2w.Chunks[LookUpChunkIndex()].AbReferences.Add(this);
                    //Soft mount the chunk except root chunk
                    if (Reference.ChunkIndex != 0)
                        Reference.MountChunkVirtually(LookUpChunkIndex());
                }
            }
            else
            {
                DepotPath = cr2w.Imports[-val - 1].DepotPathStr;
                var filetype = cr2w.Imports[-val - 1].Import.className;
                ClassName = cr2w.Names[filetype].Str;
                Flags = cr2w.Imports[-val - 1].Import.flags;
            }
        }

        /// <summary>
        /// Call after the stringtable was generated!
        /// </summary>
        /// <param name="w"></param>
        public override void Write(BinaryWriter w)
        {
            var val = 0;
            if (ChunkHandle)
            {
                if (Reference != null)
                    val = Reference.ChunkIndex + 1;
            }
            else
            {
                var import = cr2w.Imports.FirstOrDefault(_ => _.DepotPathStr == DepotPath && _.ClassNameStr == ClassName);
                val = -cr2w.Imports.IndexOf(import) - 1;
            }
            w.Write(val);
        }

        public override CVariable SetValue(object val)
        {
            switch (val)
            {
                case int o: SetValueInternal(o); break;
                case IHandleAccessor cvar:
                    ChunkHandle = cvar.ChunkHandle;
                    DepotPath = cvar.DepotPath;
                    ClassName = cvar.ClassName;
                    Flags = cvar.Flags;
                    Reference = cvar.Reference;
                    break;
            }
            return this;
        }

        public override CVariable Copy(CR2WCopyAction context)
        {
            var copy = (CHandle<T>)base.Copy(context);
            copy.ChunkHandle = ChunkHandle;

            // Soft
            copy.DepotPath = DepotPath;
            copy.ClassName = ClassName;
            copy.Flags = Flags;

            // Ptr
            if (ChunkHandle && Reference != null)
            {
                CR2WExportWrapper newref = context.TryLookupReference(Reference, copy);
                if (newref != null)
                    copy.Reference = newref;
            }

            return copy;
        }

        public override string ToString()
        {
            if (ChunkHandle)
            {
                if (Reference == null) return "NULL";
                else return $"{Reference.REDType} #{Reference.ChunkIndex}";
            }
            return $"{ClassName}: {DepotPath}";
        }
        public override string REDLeanValue() => Reference == null ? "" : $"{Reference.ChunkIndex}";
    }
}
