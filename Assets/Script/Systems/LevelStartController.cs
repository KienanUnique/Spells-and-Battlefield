using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Factories;
using Common.Abstract_Bases.Factories.Object_Pool;
using Systems.In_Game_Systems.Factory;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class LevelStartController : MonoBehaviour
    {
        private IReadOnlyList<IObjectPoolingFactory> _objectPoolingFactories;
        private IInGameSystemsFactory _gameSystemsFactory;

        [Inject]
        private void Construct(IReadOnlyList<IObjectPoolingFactory> objectPoolingFactories,
            IInGameSystemsFactory gameSystemsFactory)
        {
            _objectPoolingFactories = objectPoolingFactories;
            _gameSystemsFactory = gameSystemsFactory;
        }

        private void Start()
        {
            _gameSystemsFactory.Create();
            foreach (var factory in _objectPoolingFactories)
            {
                factory.FillPool();
            }
        }

        private void OnEnable()
        {
            foreach (var factory in _objectPoolingFactories)
            {
                factory.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (var factory in _objectPoolingFactories)
            {
                factory.Disable();
            }
        }
    }
}