using Common.Abstract_Bases.Factories;
using UI.Popup_Text.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace UI.Popup_Text.Factory
{
    public class PopupTextFactory : ObjectPoolingFactoryWithInstantiatorBase<IPopupTextController>,
        IPopupTextFactory
    {
        private readonly IPopupTextPrefabProvider _prefabProvider;
        private readonly Vector3 _defaultSpawnPosition;

        public PopupTextFactory(IInstantiator instantiator, Transform parentTransform, int needItemsCount,
            IPopupTextPrefabProvider prefabProvider, Vector3 defaultSpawnPosition) : base(instantiator, parentTransform,
            needItemsCount)
        {
            _prefabProvider = prefabProvider;
            _defaultSpawnPosition = defaultSpawnPosition;
        }

        public IPopupTextController Create(string textToShow, Vector3 startPosition)
        {
            var getItem = GetItem();
            getItem.Popup(textToShow, startPosition);
            return getItem;
        }

        protected override IPopupTextController InstantiateItem()
        {
            return InstantiatePrefabForComponent<IPopupTextController>(_prefabProvider, _defaultSpawnPosition,
                Quaternion.identity);
        }

        protected override void HandleReleaseItem(IPopupTextController item)
        {
        }
    }
}