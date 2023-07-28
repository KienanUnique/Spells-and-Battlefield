using Enemies.Spawn.Factory;
using Pickable_Items.Factory;
using Spells.Factory;
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

        public override void InstallBindings()
        {
            InstallPickableItemsFactory();
            InstallSpellFactory();
            InstallEnemyFactory();
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