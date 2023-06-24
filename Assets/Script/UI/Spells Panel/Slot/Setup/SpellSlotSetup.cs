using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Settings.UI;
using UI.Spells_Panel.Slot.Model;
using UI.Spells_Panel.Slot.View;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Spells_Panel.Slot.Setup
{
    [RequireComponent(typeof(IInitializableSpellSlotPresenter))]
    public class SpellSlotSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private RawImage _image;
        [SerializeField] private RectTransform _rectTransform;
        private IInitializableSpellSlotPresenter _controllerToSetup;
        private SpellPanelSettings _settings;
        private ISpellSlotModel _model;
        private ISpellSlotView _view;

        [Inject]
        public void Construct(SpellPanelSettings settings)
        {
            _settings = settings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _controllerToSetup = GetComponent<IInitializableSpellSlotPresenter>();
            var currentSlotInformation = new SlotInformation(_rectTransform.localScale, _rectTransform.localPosition);
            _model = new SpellSlotModel(currentSlotInformation);
            _view = new SpellSlotView(_image, _rectTransform, _settings);
        }

        protected override void Initialize()
        {
            _controllerToSetup.Initialize(_model, _view, _settings);
        }
    }
}