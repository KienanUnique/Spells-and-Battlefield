using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Interfaces;
using TMPro;
using UI.Popup_Text.Settings;
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
        private IInitializablePopupTextController _controller;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPopupTextSettings _settings;

        [Inject]
        private void GetDependencies(IPopupTextSettings settings, IPlayerInformationProvider playerInformationProvider)
        {
            _settings = settings;
            _playerInformationProvider = playerInformationProvider;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            _controller.Initialize(_text, _mainTransform, _settings, _playerInformationProvider.CameraTransform);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePopupTextController>();
        }
    }
}