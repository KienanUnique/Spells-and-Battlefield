using System.Collections.Generic;
using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Enemies.Spawn.Factory;
using Pickable_Items.Factory;
using Spells.Factory;
using Systems.In_Game_Systems.Factory;
using Systems.In_Game_Systems.Prefab_Provider;
using UI.Popup_Text.Factory;
using UI.Popup_Text.Factory.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    [RequireComponent(typeof(LevelStartController))]
    public class FactoriesInstaller : MonoInstaller
    {
        private const string EnemiesSectionName = "Enemies";
        private const string SpellsSectionName = "Spells";
        private const string PickableItemsSectionName = "Pickable Items";
        private const string PopupTextSectionName = "Popup Text";
        private const string SystemsSectionName = "Systems";

        [SerializeField] private PopupHitPointsChangeTextFactorySettings _popupHitPointsChangeTextFactorySettings;
        [SerializeField] private InGameSystemsPrefabProvider _inGameSystemsPrefabProvider;
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