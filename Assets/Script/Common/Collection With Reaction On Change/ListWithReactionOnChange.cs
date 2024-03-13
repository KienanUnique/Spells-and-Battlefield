using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.Collection_With_Reaction_On_Change
{
    public class ListWithReactionOnChange<T> : IList<T>, IReadonlyListWithReactionOnChange<T>
    {
        private readonly List<T> _list;

        public ListWithReactionOnChange()
        {
            _list = new List<T>();
        }

        public ListWithReactionOnChange(IEnumerable<T> enumerableFromCopy)
        {
            _list = new List<T>(enumerableFromCopy);
        }

        public event Action<ItemWithIndexEventArgs<T>> ItemAdded;
        public event Action<ItemWithIndexEventArgs<T>> ItemRemoved;
        public event Action<ItemReplacedEventArgs<T>> ItemReplaced;
        public event Action<ItemWithIndexEventArgs<T>> ItemInserted;
        public event Action<EventArgs> ItemsCleared;

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => _list[index];
            set
            {
                var oldValue = _list[index];
                _list[index] = value;
                ItemReplaced?.Invoke(new ItemReplacedEventArgs<T>(value, oldValue, index));
            }
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public void Add(T item)
        {
            _list.Add(item);
            ItemAdded?.Invoke(new ItemWithIndexEventArgs<T>(item, _list.Count - 1));
        }

        public void Clear()
        {
            _list.Clear();
            ItemsCleared?.Invoke(EventArgs.Empty);
        }

        public bool Remove(T item)
        {
            var itemToRemoveIndex = _list.IndexOf(item);
            var willBeRemoved = itemToRemoveIndex != -1;
            if (willBeRemoved)
            {
                _list.RemoveAt(itemToRemoveIndex);
                ItemRemoved?.Invoke(new ItemWithIndexEventArgs<T>(item, itemToRemoveIndex));
            }

            return willBeRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            ItemInserted?.Invoke(new ItemWithIndexEventArgs<T>(item, index));
        }

        public void RemoveAt(int index)
        {
            var removedItem = _list[index];
            _list.RemoveAt(index);
            ItemRemoved?.Invoke(new ItemWithIndexEventArgs<T>(removedItem, index));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}