using GameEstate.Core;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class SpeechRecord
    {
        public class SpeechEntry : IComparer<SpeechEntry>
        {
            public readonly int Index;
            public readonly List<string> Strings = new List<string>();
            public SpeechEntry(int index) => Index = index;
            public int Compare(SpeechEntry x, SpeechEntry y) => x.Index.CompareTo(y.Index);
        }

        public readonly List<IDictionary<int, SpeechEntry>> Tables = new List<IDictionary<int, SpeechEntry>>();

        public Task ReadAsync(BinaryDatFile source)
        {
            int lastIndex = -1;
            IDictionary<int, SpeechEntry> table = null;
            using (var mul = source.GetReader("speech.mul"))
                if (mul != null)
                    while (mul.PeekChar() >= 0)
                    {
                        var index = (mul.ReadByte() << 8) | mul.ReadByte();
                        var length = (mul.ReadByte() << 8) | mul.ReadByte();
                        var text = Encoding.UTF8.GetString(mul.ReadBytes(length)).Trim();
                        if (text.Length == 0)
                            continue;
                        if (table == null || lastIndex > index)
                        {
                            if (index == 0 && text == "*withdraw*")
                                Tables.Insert(0, table = new Dictionary<int, SpeechEntry>());
                            else
                                Tables.Add(table = new Dictionary<int, SpeechEntry>());
                        }
                        lastIndex = index;

                        table.TryGetValue(index, out var entry);
                        if (entry == null)
                            table[index] = entry = new SpeechEntry(index);
                        entry.Strings.Add(text);
                    }
            return Task.CompletedTask;
        }
    }
}