using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Settings.UI;
using UI.Spells_Panel.Slot.Model;
using UI.Spells_Panel.Slot.View;
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
        [SerializeField] private Transform _transform;
        private IInitializableSpellSlotPresenter _controllerToSetup;
        private SpellPanelSettings _settings;
        private ISpellSlotModel _model;
        private ISpellSlotView _view;

        [Inject]
        public void Construct(SpellPanelSettings settings)
        {
            _settings = settings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _controllerToSetup = GetComponent<IInitializableSpellSlotPresenter>();
            _model = new SpellSlotModel();
            _view = new SpellSlotView(_image, _transform, _settings);
        }

        protected override void Initialize()
        {
            _controllerToSetup.Initialize(_model, _view, _settings);
        }
    }
}