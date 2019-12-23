using System;
using System.Collections.Concurrent;

namespace GameEstate.Core
{
    public class GenericPool<T> : IDisposable
        where T : IDisposable
    {
        const int MAX = 5;
        readonly ConcurrentBag<T> _items = new ConcurrentBag<T>();
        int _counter = 0;
        public readonly Func<T> Factory;

        public GenericPool(Func<T> factory) => Factory = factory;

        public void Dispose()
        {
            foreach (var item in _items)
                item.Dispose();
        }

        public void Release(T item)
        {
            if (_counter < MAX)
            {
                _items.Add(item);
                _counter++;
            }
            else
                item.Dispose();
        }

        public T Get()
        {
            if (_items.TryTake(out var item))
            {
                _counter--;
                return item;
            }
            else
            {
                var obj = Factory();
                _items.Add(obj);
                _counter++;
                return obj;
            }
        }
    }
}