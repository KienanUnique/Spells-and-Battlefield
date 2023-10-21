using Common.Abstract_Bases;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Presenter;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Continuous Effect Indicator Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.UIPrefabProvidersDirectory +
                   "Continuous Effect Indicator Prefab Provider", order = 0)]
    public class ContinuousEffectIndicatorPrefabProvider : PrefabProviderScriptableObjectBase,
        IContinuousEffectIndicatorPrefabProvider
    {
        [SerializeField] private ContinuousEffectIndicatorPresenter _presenter;
        public override GameObject Prefab => _presenter.gameObject;
    }
}