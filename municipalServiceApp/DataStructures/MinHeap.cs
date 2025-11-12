//NEEDED FOR PART 3 OF POE
using System;
using System.Collections.Generic;

namespace municipalServiceApp.DataStructures
{
    public class MinHeap<T>
    {
        private readonly List<T> _data = new();
        private readonly Comparison<T> _cmp;

        public MinHeap(Comparison<T> comparison) { _cmp = comparison; }

        public int Count => _data.Count;

        public void Push(T item)
        {
            _data.Add(item);
            SiftUp(_data.Count - 1);
        }

        public T Pop()
        {
            if (_data.Count == 0) throw new InvalidOperationException("Heap empty");
            var top = _data[0];
            _data[0] = _data[^1];
            _data.RemoveAt(_data.Count - 1);
            if (_data.Count > 0) SiftDown(0);
            return top;
        }

        public T Peek()
        {
            if (_data.Count == 0) throw new InvalidOperationException("Heap empty");
            return _data[0];
        }

        private void SiftUp(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (_cmp(_data[i], _data[p]) >= 0) break;
                (_data[i], _data[p]) = (_data[p], _data[i]);
                i = p;
            }
        }

        private void SiftDown(int i)
        {
            int n = _data.Count;
            while (true)
            {
                int l = 2 * i + 1, r = l + 1, smallest = i;
                if (l < n && _cmp(_data[l], _data[smallest]) < 0) smallest = l;
                if (r < n && _cmp(_data[r], _data[smallest]) < 0) smallest = r;
                if (smallest == i) break;
                (_data[i], _data[smallest]) = (_data[smallest], _data[i]);
                i = smallest;
            }
        }
    }
}
