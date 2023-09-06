using System.Collections.Generic;
using Common.Abstract_Bases.Factories.Object_Pool;
using Systems.In_Game_Systems.Factory;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class LevelStartController : MonoBehaviour
    {
        private IInGameSystemsFactory _gameSystemsFactory;
        private IReadOnlyList<IObjectPoolingFactory> _objectPoolingFactories;

        [Inject]
        private void GetDependencies(IReadOnlyList<IObjectPoolingFactory> objectPoolingFactories,
            IInGameSystemsFactory gameSystemsFactory)
        {
            _objectPoolingFactories = objectPoolingFactories;
            _gameSystemsFactory = gameSystemsFactory;
        }

        private void Start()
        {
            _gameSystemsFactory.Create();
            foreach (IObjectPoolingFactory factory in _objectPoolingFactories)
            {
                factory.FillPool();
            }
        }

        private void OnEnable()
        {
            foreach (IObjectPoolingFactory factory in _objectPoolingFactories)
            {
                factory.Enable();
            }
        }

        private void OnDisable()
        {
            foreach (IObjectPoolingFactory factory in _objectPoolingFactories)
            {
                factory.Disable();
            }
        }
    }
}