using System;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Presenter;
using UnityEngine;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Provider
{
    [Serializable]
    public class ComicsScreenProvider : IComicsScreenProvider
    {
        [SerializeField] private ComicsScreenPresenter _prefab;
        public GameObject Prefab => _prefab.gameObject;
    }
}