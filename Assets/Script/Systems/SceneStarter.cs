using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Factories;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class SceneStarter : MonoBehaviour
    {
        private IReadOnlyList<IObjectPoolingFactory> _objectPoolingFactories;

        [Inject]
        private void Construct(IReadOnlyList<IObjectPoolingFactory> objectPoolingFactories)
        {
            _objectPoolingFactories = objectPoolingFactories;
        }

        private void Start()
        {
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