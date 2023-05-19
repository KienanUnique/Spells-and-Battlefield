using Enemies.Factory;
using Pickable_Items;
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

        [SerializeField] private PickableSpellController _pickableSpellPrefab;

        public override void InstallBindings()
        {
            InstallPickableSpellFactory();
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

        private void InstallPickableSpellFactory()
        {
            IPickableSpellsFactory pickableSpellsFactory =
                new PickableSpellsFactory(Container, _pickableItemsParent, _pickableSpellPrefab);
            Container
                .Bind<IPickableSpellsFactory>()
                .FromInstance(pickableSpellsFactory)
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