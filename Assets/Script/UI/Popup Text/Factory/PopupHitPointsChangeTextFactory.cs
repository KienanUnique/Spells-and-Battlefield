using System;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using UI.Popup_Text.Factory.Settings;
using UnityEngine;
using Zenject;

namespace UI.Popup_Text.Factory
{
    public class PopupHitPointsChangeTextFactory : IObjectPoolingFactory, IPopupHitPointsChangeTextFactory
    {
        private readonly List<IObjectPoolingFactory> _allTextFactories;
        private readonly IPopupTextFactory _damageTextFactory;
        private readonly IPopupTextFactory _healTextFactory;

        public PopupHitPointsChangeTextFactory(IInstantiator instantiator, Transform parentTransform,
            IPopupHitPointsChangeTextFactorySettings settings,
            IPositionDataForInstantiation defaultPositionDataForInstantiation)
        {
            var healTextFactory = new PopupTextFactory(instantiator, parentTransform,
                settings.HealTextObjectPooledCount, settings.HealTextPrefabProvider,
                defaultPositionDataForInstantiation);
            _healTextFactory = healTextFactory;

            var damageTextFactory = new PopupTextFactory(instantiator, parentTransform,
                settings.DamageTextObjectPooledCount, settings.DamageTextPrefabProvider,
                defaultPositionDataForInstantiation);
            _damageTextFactory = damageTextFactory;

            _allTextFactories = new List<IObjectPoolingFactory> {damageTextFactory, healTextFactory};
        }

        public void Enable()
        {
            _allTextFactories.ForEach(factory => factory.Enable());
        }

        public void Disable()
        {
            _allTextFactories.ForEach(factory => factory.Disable());
        }

        public void FillPool()
        {
            _allTextFactories.ForEach(factory => factory.FillPool());
        }

        public void Create(TypeOfHitPointsChange typeOfHitPointsChange, int countOfHitPoints, Vector3 startPosition)
        {
            IPopupTextFactory selectedFactory = typeOfHitPointsChange switch
            {
                TypeOfHitPointsChange.Damage => _damageTextFactory,
                TypeOfHitPointsChange.Heal => _healTextFactory,
                _ => throw new ArgumentOutOfRangeException(nameof(typeOfHitPointsChange), typeOfHitPointsChange, null)
            };

            selectedFactory.Create(countOfHitPoints.ToString(), startPosition);
        }
    }
}