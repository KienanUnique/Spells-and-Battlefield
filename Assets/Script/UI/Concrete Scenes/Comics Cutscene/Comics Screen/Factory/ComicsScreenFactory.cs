using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Provider;
using UnityEngine;
using Zenject;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Factory
{
    public class ComicsScreenFactory : IComicsScreenFactory
    {
        private readonly IInstantiator _instantiator;

        public ComicsScreenFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IInitializableComicsScreen Create(IComicsScreenProvider provider, Transform parent)
        {
            return _instantiator.InstantiatePrefabForComponent<IInitializableComicsScreen>(provider.Prefab.gameObject,
                parent);
        }
    }
}