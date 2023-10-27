using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Provider;
using UnityEngine;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Factory
{
    public interface IComicsScreenFactory
    {
        public IInitializableComicsScreen Create(IComicsScreenProvider provider, Transform parent);
    }
}