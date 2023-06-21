using System;

namespace Common.Collection_With_Reaction_On_Change
{
    public class ItemReplacedEventArgs<T> : EventArgs
    {
        public ItemReplacedEventArgs(T newItem, T oldItem, int index)
        {
            NewItem = newItem;
            OldItem = oldItem;
            Index = index;
        }

        public T NewItem { get; }
        public T OldItem { get; }
        public int Index { get; }
    }
}