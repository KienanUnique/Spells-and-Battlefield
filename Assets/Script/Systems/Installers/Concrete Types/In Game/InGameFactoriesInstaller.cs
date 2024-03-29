﻿using System.Collections.Generic;
using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Enemies.Spawn.Factory;
using Pickable_Items.Factory;
using Spells.Factory;
using Systems.In_Game_Systems.Factory;
using Systems.In_Game_Systems.Prefab_Provider;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Factory;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings;
using UI.Concrete_Scenes.In_Game.Popup_Text.Factory;
using UI.Concrete_Scenes.In_Game.Popup_Text.Factory.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.In_Game
{
    [RequireComponent(typeof(LevelStartController))]
    public class InGameFactoriesInstaller : MonoInstaller
    {
        private const string EnemiesSectionName = "Enemies";
        private const string SpellsSectionName = "Spells";
        private const string PickableItemsSectionName = "Pickable Items";
        private const string PopupTextSectionName = "Popup Text";
        private const string SystemsSectionName = "Systems";
        private const string OtherSectionName = "Other";

        [SerializeField] private PopupHitPointsChangeTextFactorySettings _popupHitPointsChangeTextFactorySettings;
        [SerializeField] private InGameSystemsPrefabProvider _inGameSystemsPrefabProvider;
        [SerializeField] private ContinuousEffectIndicatorFactorySettings _continuousEffectIndicatorFactorySettings;
        private List<IObjectPoolingFactory> _objectPoolingFactories;
        private IReadOnlyList<IObjectPoolingFactory> ObjectPoolingFactories => _objectPoolingFactories;

        public override void InstallBindings()
        {
            _objectPoolingFactories = new List<IObjectPoolingFactory>();

            InstallPickableItemsFactory();
            InstallSpellFactory();
            InstallEnemyFactory();
            InstallPopupTextFactories();
            InstallAllObjectPoolingFactories();
            InstallGameControllerSystems();
            InstallIContinuousEffectIndicatorFactory();
        }

        private void InstallIContinuousEffectIndicatorFactory()
        {
            var factory = new ContinuousEffectIndicatorFactory(Container, CreateEmptyParentWithName(OtherSectionName),
                _continuousEffectIndicatorFactorySettings);
            _objectPoolingFactories.Add(factory);
            Container.Bind<IContinuousEffectIndicatorFactory>().FromInstance(factory).AsSingle();
        }

        private void InstallGameControllerSystems()
        {
            var factory = new InGameSystemsFactory(Container, CreateEmptyParentWithName(SystemsSectionName),
                _inGameSystemsPrefabProvider);
            Container.Bind<IInGameSystemsFactory>().FromInstance(factory).AsSingle();
        }

        private void InstallPopupTextFactories()
        {
            var textFactory = new PopupHitPointsChangeTextFactory(Container,
                CreateEmptyParentWithName(PopupTextSectionName), _popupHitPointsChangeTextFactorySettings,
                new PositionDataForInstantiation(Vector3.zero, Quaternion.identity));

            _objectPoolingFactories.Add(textFactory);

            Container.Bind<IPopupHitPointsChangeTextFactory>().FromInstance(textFactory).AsSingle();
        }

        private void InstallAllObjectPoolingFactories()
        {
            Container.Bind<IReadOnlyList<IObjectPoolingFactory>>().FromInstance(ObjectPoolingFactories).AsSingle();
        }

        private void InstallEnemyFactory()
        {
            IEnemyFactory enemyFactory = new EnemyFactory(Container, CreateEmptyParentWithName(EnemiesSectionName));
            Container.Bind<IEnemyFactory>().FromInstance(enemyFactory).AsSingle();
        }

        private void InstallPickableItemsFactory()
        {
            IPickableItemsFactory pickableItemsFactory =
                new PickableItemsFactory(Container, CreateEmptyParentWithName(PickableItemsSectionName));
            Container.Bind<IPickableItemsFactory>().FromInstance(pickableItemsFactory).AsSingle();
        }

        private void InstallSpellFactory()
        {
            ISpellObjectsFactory spellObjectsFactory =
                new SpellObjectsFactory(Container, CreateEmptyParentWithName(SpellsSectionName));
            Container.Bind<ISpellObjectsFactory>().FromInstance(spellObjectsFactory).AsSingle();
        }

        private Transform CreateEmptyParentWithName(string parentName)
        {
            return new GameObject(parentName).transform;
        }
    }
}