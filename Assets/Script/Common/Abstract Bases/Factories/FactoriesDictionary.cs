using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Common.Abstract_Bases.Factories
{
    public abstract class FactoriesDictionary<TKey, TFactory>
    {
        private readonly Dictionary<TKey, TFactory> _factories = new Dictionary<TKey, TFactory>();
        private readonly IInstantiator _instantiator;
        private readonly Transform _parentTransform;

        protected FactoriesDictionary(IInstantiator instantiator, Transform parentTransform)
        {
            _instantiator = instantiator;
            _parentTransform = parentTransform;
        }

        protected abstract TFactory
            CreateFactoryForKey(TKey key, IInstantiator instantiator, Transform parentTransform);

        protected TFactory GetFactoryByKey(TKey key)
        {
            if (!_factories.ContainsKey(key))
            {
                _factories[key] = CreateFactoryForKey(key, _instantiator, _parentTransform);
            }

            return _factories[key];
        }
    }
}