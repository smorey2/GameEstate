﻿using GameEstate.Core;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    public class REDIExtraIntData : REDIAbstract
    {
        public class EditIntData
        {
            public string Name { get; set; }
            public int Value { get; set; }

            public void WriteText(IndentedTextWriter w)
            {
                w.WriteLine("ResourceEditIntData_t {"); w.Indent++;
                w.WriteLine($"CResourceString m_Name = \"{Name}\"");
                w.WriteLine($"int32 m_nInt = {Value}");
                w.Indent--; w.WriteLine("}");
            }
        }

        public List<EditIntData> List { get; } = new List<EditIntData>();

        public override void Read(BinaryReader r, BinaryPak resource)
        {
            r.BaseStream.Position = Offset;
            for (var i = 0; i < Size; i++)
                List.Add(new EditIntData
                {
                    Name = r.ReadO32UTF8(),
                    Value = r.ReadInt32()
                });
        }

        public override void WriteText(IndentedTextWriter w)
        {
            w.WriteLine($"Struct m_ExtraIntData[{List.Count}] = ["); w.Indent++;
            foreach (var dep in List)
                dep.WriteText(w);
            w.Indent--; w.WriteLine("]");
        }
    }
}
