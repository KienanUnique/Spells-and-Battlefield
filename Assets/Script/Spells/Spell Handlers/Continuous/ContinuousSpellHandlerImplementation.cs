using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Factory;

namespace Spells.Spell_Handlers.Continuous
{
    public class ContinuousSpellHandlerImplementation : SpellsHandlerImplementationBase,
        IContinuousSpellHandlerImplementation
    {
        private IInformationAboutContinuousSpell _spellToCreate;
        private IContinuousSpellController _castedSpell;
        private ContinuousSpellPhases _currentPhase = ContinuousSpellPhases.Cast;

        public ContinuousSpellHandlerImplementation(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IReadonlyTransform spellSpawnObject) : base(caster, spellObjectsFactory, spellSpawnObject)
        {
        }

        private enum ContinuousSpellPhases
        {
            Idle, Cast, InAction
        }

        public float RatioOfCompletion => _castedSpell.RatioOfCompletion;

        public void HandleSpell(IInformationAboutContinuousSpell informationAboutContinuousSpell)
        {
            HandleStart();
            _spellToCreate = informationAboutContinuousSpell;
            _currentPhase = ContinuousSpellPhases.Cast;
            PlayContinuousActionAnimation(_spellToCreate.AnimationData);
        }

        public override bool TryInterrupt()
        {
            if (_currentPhase != ContinuousSpellPhases.Cast && _currentPhase != ContinuousSpellPhases.InAction)
            {
                return false;
            }

            Cancel();
            return true;
        }

        public override void OnSpellCastPartOfAnimationFinished()
        {
            _currentPhase = ContinuousSpellPhases.InAction;
            _castedSpell = _spellObjectsFactory.Create(_spellToCreate.GetActivationData(_caster, _spellSpawnObject),
                _spellToCreate.PrefabProvider,
                new PositionDataForInstantiation(_spellSpawnObject.Position, _spellSpawnObject.Rotation));
            _spellToCreate = null;
            if (IsEnabled)
            {
                SubscribeOnSpellEvents(_castedSpell);
            }

            HandleEndOfCast();
        }

        protected override void Cancel()
        {
            if (_castedSpell != null)
            {
                UnsubscribeFromSpellEvents(_castedSpell);
                _castedSpell.Interrupt();
                _castedSpell = null;
            }

            _spellToCreate = null;
            _currentPhase = ContinuousSpellPhases.Idle;
            base.Cancel();
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            if (_castedSpell == null)
            {
                return;
            }

            SubscribeOnSpellEvents(_castedSpell);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            if (_castedSpell == null)
            {
                return;
            }

            UnsubscribeFromSpellEvents(_castedSpell);
        }

        private void SubscribeOnSpellEvents(IContinuousSpellController spell)
        {
            spell.Finished += OnSpellFinished;
        }

        private void UnsubscribeFromSpellEvents(IContinuousSpellController spell)
        {
            spell.Finished -= OnSpellFinished;
        }

        private void OnSpellFinished()
        {
            UnsubscribeFromSpellEvents(_castedSpell);
            _castedSpell = null;
            _currentPhase = ContinuousSpellPhases.Idle;
            HandleEndOfSpell();
        }
    }
}