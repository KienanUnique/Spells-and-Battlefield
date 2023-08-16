using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Readonly_Transform;
using Interfaces;
using Settings.UI;
using TMPro;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Popup_Text
{
    [RequireComponent(typeof(PopupTextController))]
    public class PopupTextControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _mainTransform;
        private PopupTextSettings _settings;
        private IPlayerInformationProvider _playerInformationProvider;
        private IInitializablePopupTextController _controller;

        [Inject]
        private void Construct(PopupTextSettings settings, IPlayerInformationProvider playerInformationProvider)
        {
            _settings = settings;
            _playerInformationProvider = playerInformationProvider;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePopupTextController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_text, _mainTransform, _settings, _playerInformationProvider.CameraTransform);
        }
    }
}