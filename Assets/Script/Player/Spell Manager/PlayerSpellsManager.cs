using System;
using System.Collections.ObjectModel;
using Common.Abstract_Bases.Spells_Manager;
using Common.Animator_Status_Controller;
using Common.Collection_With_Reaction_On_Change;
using Player.Spell_Manager.Spells_Selector;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Spells.Spell_Handlers.Continuous;
using Spells.Spell_Handlers.Instant;

namespace Player.Spell_Manager
{
    public class PlayerSpellsManager : SpellsManagerBase, IPlayerSpellsManager
    {
        private readonly IPlayerSpellsSelectorForSpellManager _playerSpellsSelector;

        public PlayerSpellsManager(IContinuousSpellHandlerImplementation continuousSpellHandler,
            IInstantSpellHandlerImplementation instantSpellHandler, IPlayerSpellsSelectorForSpellManager spellsSelector,
            IReadonlyAnimatorStatusChecker animatorStatusChecker) : base(continuousSpellHandler, instantSpellHandler,
            spellsSelector, animatorStatusChecker)
        {
            _playerSpellsSelector = spellsSelector;
        }

        public event Action ContinuousSpellStarted;
        public event Action ContinuousSpellFinished;

        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;
        public float ContinuousSpellRatioOfCompletion => ContinuousSpellHandler.RatioOfCompletion;

        public ISpellType SelectedSpellType => _playerSpellsSelector.SelectedSpellType;

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells =>
            _playerSpellsSelector.Spells;

        public void AddSpell(ISpellType spellType, ISpell newSpell)
        {
            _playerSpellsSelector.AddSpell(spellType, newSpell);
        }

        public void AddSpell(ISpell newSpell)
        {
            _playerSpellsSelector.AddSpell(newSpell);
        }

        public void SelectNextSpellType()
        {
            _playerSpellsSelector.SelectNextSpellType();
        }

        public void SelectPreviousSpellType()
        {
            _playerSpellsSelector.SelectPreviousSpellType();
        }

        public void SelectSpellTypeWithIndex(int indexToSelect)
        {
            _playerSpellsSelector.SelectSpellTypeWithIndex(indexToSelect);
        }

        protected sealed override void SubscribeOnEvents()
        {
            _playerSpellsSelector.SelectedSpellTypeChanged += OnSelectedSpellTypeChanged;
            _playerSpellsSelector.TryingToUseEmptySpellTypeGroup += OnTryingToUseEmptySpellTypeGroup;

            base.SubscribeOnEvents();
        }

        protected override void OnSpellCastPartOfAnimationFinished()
        {
            base.OnSpellCastPartOfAnimationFinished();
            if (IsCurrentSpellContinuous && CurrentSpellsHandler != null)
            {
                ContinuousSpellStarted?.Invoke();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            _playerSpellsSelector.SelectedSpellTypeChanged -= OnSelectedSpellTypeChanged;
            _playerSpellsSelector.TryingToUseEmptySpellTypeGroup -= OnTryingToUseEmptySpellTypeGroup;

            base.UnsubscribeFromEvents();
        }

        protected override void OnSpellHandled()
        {
            if (IsCurrentSpellContinuous)
            {
                ContinuousSpellFinished?.Invoke();
            }

            base.OnSpellHandled();
        }

        private void OnTryingToUseEmptySpellTypeGroup(ISpellType obj)
        {
            TryingToUseEmptySpellTypeGroup?.Invoke(obj);
        }

        private void OnSelectedSpellTypeChanged(ISpellType obj)
        {
            SelectedSpellTypeChanged?.Invoke(obj);
        }
    }
}