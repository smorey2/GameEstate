﻿using GameEstate.Formats.Red.CR2W;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace GameEstate.Formats.Red.Types
{
    public interface IEnumAccessor
    {
        List<string> Value { get; set; }
        bool IsFlag { get; }
        string EnumToString();
        CVariable SetValue(object val);
        Type GetEnumType();
    }

    [DataContract(Namespace = ""), REDMeta()]
    public class CEnum<T> : CVariable, IEnumAccessor where T : Enum
    {
        public CEnum(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }

        T _wrappedEnum;
        public T WrappedEnum
        {
            get => _wrappedEnum;
            set
            {
                _wrappedEnum = value;
                UpdateStringList();
            }
        }

        [DataMember]
        public List<string> Value { get; set; } = new List<string>();

        public bool IsFlag => WrappedEnum.GetType().IsDefined(typeof(FlagsAttribute), false);

        void UpdateStringList()
        {
            var strings = new List<string>();
            if (IsFlag) { } // TODO: not implemented
            else strings.Add(WrappedEnum.ToString());
            Value = strings;
        }

        public Type GetEnumType() => WrappedEnum.GetType();
        public string EnumToString() => WrappedEnum.ToString();

        public override string REDType => WrappedEnum.GetType().Name;

        static void SetFlag<T1>(ref T1 value, T1 flag) where T1 : Enum
        {
            var numericValue = Convert.ToUInt64(value);
            numericValue |= Convert.ToUInt64(flag);
            value = (T1)Enum.ToObject(typeof(T1), numericValue);
        }

        static void ClearFlag<T1>(ref T1 value, T1 flag) where T1 : Enum
        {
            var numericValue = Convert.ToUInt64(value);
            numericValue &= ~Convert.ToUInt64(flag);
            value = (T1)Enum.ToObject(typeof(T1), numericValue);
        }

        public override void Read(BinaryReader r, uint size)
        {
            var strings = new List<string>();
            if (IsFlag)
            {
                while (true)
                {
                    var idx = r.ReadUInt16();
                    if (idx == 0)
                        break;
                    var s = cr2w.Names[idx].Str;
                    strings.Add(s);
                }
            }
            else
            {
                var idx = r.ReadUInt16();
                var s = cr2w.Names[idx].Str;
                strings.Add(s);
            }
            SetValue(strings);
        }

        /// <summary>
        /// Call after the stringtable was generated!
        /// </summary>
        /// <param name="w"></param>
        public override void Write(BinaryWriter w)
        {
            ushort val = 0;
            foreach (var item in Value)
            {
                var nw = cr2w.Names.First(_ => _.Str == item);
                val = (ushort)cr2w.Names.IndexOf(nw);
                w.Write(val);
            }
            if (IsFlag)
                w.Write((ushort)0x00);
        }

        public override CVariable Copy(CR2WCopyAction context)
        {
            var var = (CEnum<T>)base.Copy(context);
            var.Value = Value;
            var.WrappedEnum = WrappedEnum;
            return var;
        }

        public override CVariable SetValue(object val)
        {
            if (!(val is List<string> l))
                return this;
            Value = l;
            if (IsFlag)
                foreach (var item in WrappedEnum.GetType().GetEnumNames())
                {
                    // handle EnumValues with Spaces in them. facepalm.
                    var finalvalue = item.Replace(" ", string.Empty);
                    finalvalue = finalvalue.Replace("'", string.Empty);
                    finalvalue = finalvalue.Replace("/", string.Empty);
                    finalvalue = finalvalue.Replace(".", string.Empty);
                    var en = (T)Enum.Parse(WrappedEnum.GetType(), finalvalue);
                    // flag is set
                    if (Value.Contains(item)) SetFlag(ref _wrappedEnum, en);
                    // flag is not set
                    else ClearFlag(ref _wrappedEnum, en);
                }
            else
            {
                var s = Value.Last();
                // handle EnumValues with Spaces in them. facepalm.
                string finalvalue = s.Replace(" ", string.Empty);
                finalvalue = finalvalue.Replace("'", string.Empty);
                finalvalue = finalvalue.Replace("/", string.Empty);
                finalvalue = finalvalue.Replace(".", string.Empty);
                // set enum
                T en = (T)Enum.Parse(WrappedEnum.GetType(), finalvalue);
                WrappedEnum = en;
            }
            return this;
        }

        public override string ToString() => string.Join(",", this.Value);
    }
}