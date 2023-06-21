using System;

namespace Common.Collection_With_Reaction_On_Change
{
    public class ItemWithIndexEventArgs<T> : EventArgs
    {
        public ItemWithIndexEventArgs(T item, int index)
        {
            Item = item;
            Index = index;
        }

        public T Item { get; }
        public int Index { get; }
    }
}