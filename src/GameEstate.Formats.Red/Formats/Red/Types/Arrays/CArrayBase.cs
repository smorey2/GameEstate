﻿using GameEstate.Formats.Red.CR2W;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Formats.Red.Types.Arrays
{
    [REDMeta()]
    public abstract class CArrayBase<T> : CVariable, IArrayAccessor<T>, IList<T> where T : IEditableVariable
    {
        public CArrayBase(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }

        public List<T> Elements { get; set; } = new List<T>();

        [Browsable(false)] public List<int> Flags { get; set; }
        public string Elementtype { get; set; }
        public Type InnerType => GetType().GetGenericArguments().Single();

        [Browsable(false)]
        public override string REDType => Flags != null
            ? BuildTypeName(Elementtype, Flags.ToArray())
            : BuildTypeName(Elementtype);

        string BuildTypeName(string type, params int[] flags) => BuildTypeName(type, flags.AsEnumerable().GetEnumerator());

        string BuildTypeName(string elementtype, IEnumerator<int> flags)
        {
            var v1 = flags.MoveNext() ? flags.Current : 0;
            var v2 = flags.MoveNext() ? flags.Current : 0;
            return $"array:{v1},{v2},{elementtype}";
        }

        public override List<IEditableVariable> GetEditableVariables() => Elements.Cast<IEditableVariable>().ToList();

        public override void Read(BinaryReader file, uint size) => throw new NotImplementedException();

        protected void Read(BinaryReader file, uint size, int elementcount)
        {
            for (var i = 0; i < elementcount; i++)
            {
                var element = CR2WTypeManager.Create(Elementtype, i.ToString(), cr2w, this);

                // no actual way to find out the elementsize of an array element
                // bacause cdpr serialized classes have no fixed size
                // solution? not sure: pass 0 and disable checks?
                element.Read(file, (uint)0);
                if (element is T te)
                {
                    te.IsSerialized = true;
                    Elements.Add(te);
                }
            }
        }

        public override void Write(BinaryWriter file)
        {
            foreach (var element in Elements)
                element.Write(file);
        }

        public override CVariable SetValue(object val)
        {
            if (val is CArrayBase<T> cvar)
                Elements = cvar.Elements;
            return this;
        }

        public override bool CanAddVariable(IEditableVariable newvar) => newvar == null || newvar is T;

        public override void AddVariable(CVariable variable)
        {
            if (variable is T tvar)
            {
                variable.SetREDName(Elements.Count.ToString());
                tvar.IsSerialized = true;
                Elements.Add(tvar);
            }
        }
        public override bool CanRemoveVariable(IEditableVariable child) => child is T && Elements.Count > 0;

        public override bool RemoveVariable(IEditableVariable child)
        {
            if (child is T tvar)
            {
                Elements.Remove(tvar);
                UpdateNames();
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            var b = new StringBuilder().Append(Elements.Count);
            if (Elements.Count > 0)
            {
                b.Append(":");
                foreach (var element in Elements)
                {
                    b.Append(" <").Append(element.ToString()).Append(">");
                    if (b.Length > 100)
                    {
                        b.Remove(100, b.Length - 100);
                        break;
                    }
                }
            }
            return b.ToString();
        }

        public override string REDLeanValue() => "";

        /// <summary>
        /// Usually CArrayBase elements's names are indices, so reindexes
        /// </summary>
        void UpdateNames()
        {
            for (var i = 0; i < Elements.Count; i++)
                if (!(Elements[i] is CVariantSizeNameType))
                    Elements[i].SetREDName(i.ToString());
        }

        public override CVariable Copy(CR2WCopyAction context)
        {
            var copy = base.Copy(context) as CArrayBase<T>;
            context.Parent = copy;
            foreach (var element in Elements)
            {
                var ccopy = element.Copy(context);
                if (ccopy is T copye)
                    copy.Elements.Add(copye);
            }
            return copy;
        }

        [Browsable(false)]
        public T this[int index]
        {
            get => ((IList<T>)Elements)[index];
            set => ((IList<T>)Elements)[index] = value;
        }

        public int Count => ((IList<T>)Elements).Count;

        public bool IsReadOnly => ((IList<T>)Elements).IsReadOnly;

        public bool IsFixedSize => ((IList)Elements).IsFixedSize;

        public object SyncRoot => ((IList)Elements).SyncRoot;

        public bool IsSynchronized => ((IList)Elements).IsSynchronized;

        object IList.this[int index]
        {
            get => ((IList)Elements)[index];
            set => ((IList)Elements)[index] = value;
        }

        public IEnumerator<T> GetEnumerator() => Elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(T item) => ((IList<T>)Elements).IndexOf(item);

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
            //((IList<T>)elements).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
            //((IList<T>)elements).RemoveAt(index);
        }

        public void Add(T item) => AddVariable(item as CVariable);

        public void Clear() => ((IList<T>)Elements).Clear();

        public bool Contains(T item) => ((IList<T>)Elements).Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => ((IList<T>)Elements).CopyTo(array, arrayIndex);

        public bool Remove(T item) => RemoveVariable(item);

        public int Add(object value)
        {
            if (value is T tvar)
            {
                AddVariable(tvar as CVariable);
                return Elements.Count;
            }
            return -1;
        }

        public bool Contains(object value) => ((IList)Elements).Contains(value);

        public int IndexOf(object value) => ((IList)Elements).IndexOf(value);

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
            //((IList)elements).Insert(index, value);
        }

        public void Remove(object value)
        {
            if (value is T cvar)
                RemoveVariable(cvar);
        }

        public void CopyTo(Array array, int index) => ((IList)Elements).CopyTo(array, index);
    }
}