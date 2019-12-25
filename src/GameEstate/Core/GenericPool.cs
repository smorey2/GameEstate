using System;
using System.Collections.Concurrent;

namespace GameEstate.Core
{
    public class GenericPool<T> : IDisposable
        where T : IDisposable
    {
        const int MAX = 10;
        readonly ConcurrentBag<T> _items = new ConcurrentBag<T>();
        public readonly Func<T> Factory;

        public GenericPool(Func<T> factory) => Factory = factory;

        public void Dispose()
        {
            foreach (var item in _items)
                item.Dispose();
        }

        public void Release(T item)
        {
            if (_items.Count < MAX) _items.Add(item);
            else item.Dispose();
        }

        public T Get() => _items.TryTake(out var item) ? item : Factory();
    }
}