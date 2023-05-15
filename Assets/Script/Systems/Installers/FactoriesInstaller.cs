using Pickable_Items;
using Spells.Factory;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private PickableSpellsFactory _pickableSpellsFactory;

        public override void InstallBindings()
        {
            InstallPickableSpellFactory();
            InstallSpellFactory();
        }

        private void InstallPickableSpellFactory()
        {
            _pickableSpellsFactory.SetInstantiator(Container);
            Container
                .Bind<IPickableSpellsFactory>()
                .FromInstance(_pickableSpellsFactory)
                .AsSingle();
        }

        private void InstallSpellFactory()
        {
            ISpellObjectsFactory spellObjectsFactory = new SpellObjectsFactory(Container);
            Container
                .Bind<ISpellObjectsFactory>()
                .FromInstance(spellObjectsFactory)
                .AsSingle();
        }
    }
}