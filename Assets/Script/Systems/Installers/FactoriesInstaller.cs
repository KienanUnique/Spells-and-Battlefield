using System.Collections.Generic;
using Common.Abstract_Bases.Factories;
using Common.Readonly_Transform;
using Enemies.Spawn.Factory;
using Pickable_Items.Factory;
using Spells.Factory;
using UI.Popup_Text.Factory;
using UI.Popup_Text.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [Header("Enemies")] [SerializeField] private Transform _enemiesParent;

        [Header("Spells")] [SerializeField] private Transform _spellsParent;

        [Header("Pickable Items")] [SerializeField]
        private Transform _pickableItemsParent;

        [Header("Popup Text")] [SerializeField]
        private Transform _popupTextParent;

        [Min(1)] [SerializeField] private int _needPopupTextObjectPooledObjectsCount;
        [SerializeField] private PopupTextPrefabProvider _damageTextPrefabProvider;
        [SerializeField] private PopupTextPrefabProvider _healTextPrefabProvider;

        private IReadOnlyList<IObjectPoolingFactory> ObjectPoolingFactories => _objectPoolingFactories;
        private List<IObjectPoolingFactory> _objectPoolingFactories;

        public override void InstallBindings()
        {
            _objectPoolingFactories = new List<IObjectPoolingFactory>();
            InstallPickableItemsFactory();
            InstallSpellFactory();
            InstallEnemyFactory();
            InstallPopupTextFactories();
            InstallAllObjectPoolingFactories();
        }

        private void InstallPopupTextFactories()
        {
            var textFactory = new PopupHitPointsChangeTextFactory(Container,
                _popupTextParent, _needPopupTextObjectPooledObjectsCount, _healTextPrefabProvider,
                _damageTextPrefabProvider, Vector3.zero);

            _objectPoolingFactories.Add(textFactory);

            Container
                .Bind<IPopupHitPointsChangeTextFactory>()
                .FromInstance(textFactory)
                .AsSingle();
        }

        private void InstallAllObjectPoolingFactories()
        {
            Container
                .Bind<IReadOnlyList<IObjectPoolingFactory>>()
                .FromInstance(ObjectPoolingFactories)
                .AsSingle();
        }

        private void InstallEnemyFactory()
        {
            IEnemyFactory enemyFactory = new EnemyFactory(Container, _enemiesParent);
            Container
                .Bind<IEnemyFactory>()
                .FromInstance(enemyFactory)
                .AsSingle();
        }

        private void InstallPickableItemsFactory()
        {
            IPickableItemsFactory pickableItemsFactory = new PickableItemsFactory(Container, _pickableItemsParent);
            Container
                .Bind<IPickableItemsFactory>()
                .FromInstance(pickableItemsFactory)
                .AsSingle();
        }

        private void InstallSpellFactory()
        {
            ISpellObjectsFactory spellObjectsFactory = new SpellObjectsFactory(Container, _spellsParent);
            Container
                .Bind<ISpellObjectsFactory>()
                .FromInstance(spellObjectsFactory)
                .AsSingle();
        }
    }
}