using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using UI.Popup_Text.Data_For_Activation;
using UnityEngine;
using Zenject;
using IPrefabProvider = Common.IPrefabProvider;

namespace UI.Popup_Text.Factory
{
    public class PopupTextFactory : ObjectPoolingFactoryWithInstantiatorBase<IPopupTextController,
            IPopupTextControllerDataForActivation>,
        IPopupTextFactory
    {
        public PopupTextFactory(IInstantiator instantiator, Transform parentTransform, int needItemsCount,
            IPrefabProvider prefabProvider, IPositionDataForInstantiation defaultPositionDataForInstantiation) : base(
            instantiator, parentTransform, needItemsCount, prefabProvider, defaultPositionDataForInstantiation)
        {
        }

        public void Create(string textToShow, Vector3 startPosition)
        {
            Create(new PopupTextControllerDataForActivation(textToShow, startPosition, Quaternion.identity));
        }
    }
}