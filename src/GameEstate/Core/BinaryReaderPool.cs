using System;
using System.Collections.Concurrent;
using System.IO;

namespace GameEstate.Core
{
    public class BinaryReaderPool : IDisposable
    {
        const int MAX = 5;
        readonly ConcurrentBag<BinaryReader> _items = new ConcurrentBag<BinaryReader>();
        int _counter = 0;
        public readonly string FilePath;

        public BinaryReaderPool(string filePath) => FilePath = filePath;

        public void Dispose()
        {
            foreach (var item in _items)
                item.Dispose();
        }

        public void Release(BinaryReader item)
        {
            if (_counter < MAX)
            {
                _items.Add(item);
                _counter++;
            }
            else
                item.Dispose();
        }

        public BinaryReader Get()
        {
            if (_items.TryTake(out var item))
            {
                _counter--;
                return item;
            }
            else
            {
                var obj = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read));
                _items.Add(obj);
                _counter++;
                return obj;
            }
        }
    }
}