using System;

namespace Common.Abstract_Bases.Factories
{
    public interface IObjectPoolItem
    {
        public event Action<IObjectPoolItem> NeedRelease;
        public bool IsUsed { get; }
    }
}