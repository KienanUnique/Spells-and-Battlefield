using System;
using System.Collections.Generic;

namespace Common.Collection_With_Reaction_On_Change
{
    public interface IReadonlyListWithReactionOnChange<T> : IReadOnlyList<T>
    {
        public event Action<ItemWithIndexEventArgs<T>> ItemAdded;
        public event Action<ItemWithIndexEventArgs<T>> ItemRemoved;
        public event Action<ItemReplacedEventArgs<T>> ItemReplaced;
        public event Action<ItemWithIndexEventArgs<T>> ItemInserted;
        public event Action<EventArgs> ItemsCleared;
    }
}