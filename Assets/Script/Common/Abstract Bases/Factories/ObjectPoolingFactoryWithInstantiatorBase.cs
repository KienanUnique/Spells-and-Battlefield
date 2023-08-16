using System;
using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using UnityEngine;
using Zenject;

namespace Common.Abstract_Bases.Factories
{
    public abstract class ObjectPoolingFactoryWithInstantiatorBase<T> : FactoryWithInstantiatorBase, IDisableable, IObjectPoolingFactory where T : IObjectPoolItem
    {
        private readonly List<T> _items;
        private readonly int _needItemsCount;
        private T CurrentItem => _items.Find(item => !item.IsUsed);
        private bool IsAllItemsUsed => _items.All(item => item.IsUsed);

        protected ObjectPoolingFactoryWithInstantiatorBase(IInstantiator instantiator, Transform parentTransform,
            int needItemsCount) :
            base(instantiator, parentTransform)
        {
            _items = new List<T>();
            _needItemsCount = needItemsCount;
        }

        public void Enable()
        {
            if (_items == null) return;
            foreach (var objectPoolItem in _items)
            {
                objectPoolItem.NeedRelease += ReleaseItem;
            }
        }

        public void Disable()
        {
            if (_items == null) return;
            foreach (var objectPoolItem in _items)
            {
                objectPoolItem.NeedRelease -= ReleaseItem;
            }
        }

        public void FillPool()
        {
            for (var i = 0; i < _needItemsCount; i++)
            {
                _items.Add(InstantiateItem());
            }
        }

        protected abstract T InstantiateItem();

        protected abstract void HandleReleaseItem(T item);

        protected T GetItem()
        {
            T itemToReturn;
            if (IsAllItemsUsed)
            {
                itemToReturn = InstantiateItem();
                _items.Add(itemToReturn);
            }
            else
            {
                itemToReturn = CurrentItem;
            }

            return itemToReturn;
        }

        private void ReleaseItem(IObjectPoolItem itemNeedRelease)
        {
            if (itemNeedRelease is T itemAsT)
            {
                HandleReleaseItem(itemAsT);
            }
            else
            {
                throw new InvalidTypeException();
            }
        }
    }

    public class InvalidTypeException : Exception
    {
        public InvalidTypeException() : base("Invalid Type")
        {
        }
    }
}