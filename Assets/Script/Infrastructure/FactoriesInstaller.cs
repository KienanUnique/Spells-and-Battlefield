using Pickable_Items;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private PickableSpellsFactory _pickableSpellsFactory;

        public override void InstallBindings()
        {
            InstallPickableSpellFactory();
        }

        private void InstallPickableSpellFactory()
        {
            _pickableSpellsFactory.SetInstantiator(Container);
            Container
                .Bind<IPickableSpellsFactory>()
                .FromInstance(_pickableSpellsFactory)
                .AsSingle();
        }
    }
}