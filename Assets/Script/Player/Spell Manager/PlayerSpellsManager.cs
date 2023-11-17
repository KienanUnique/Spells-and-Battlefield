using System;
using System.Collections.ObjectModel;
using Common.Abstract_Bases.Disableable;
using Common.Animation_Data;
using Common.Collection_With_Reaction_On_Change;
using Player.Spell_Manager.Spell_Handlers;
using Player.Spell_Manager.Spell_Handlers.Continuous;
using Player.Spell_Manager.Spell_Handlers.Instant;
using Player.Spell_Manager.Spells_Selector;
using Spells;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;

namespace Player.Spell_Manager
{
    public class PlayerSpellsManager : BaseWithDisabling, IPlayerSpellsManager, ISpellHandler
    {
        private readonly IPlayerContinuousSpellHandler _continuousSpellHandler;
        private readonly IPlayerInstantSpellHandler _instantSpellHandler;
        private readonly IPlayerSpellsSelectorForSpellManager _spellsSelector;
        private bool _needCast;
        private bool _isAnimatorReady = true;
        private IPlayerSpellsHandler _currentSpellsHandler;

        public PlayerSpellsManager(IPlayerContinuousSpellHandler continuousSpellHandler,
            IPlayerInstantSpellHandler instantSpellHandler, IPlayerSpellsSelectorForSpellManager spellsSelector)
        {
            _continuousSpellHandler = continuousSpellHandler;
            _instantSpellHandler = instantSpellHandler;
            _spellsSelector = spellsSelector;
        }

        public event Action<IAnimationData> NeedPlaySingleActionAnimation;
        public event Action<IContinuousActionAnimationData> NeedPlayContinuousActionAnimation;
        public event Action NeedCancelActionAnimations;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;

        public ISpellType SelectedSpellType => _spellsSelector.SelectedSpellType;

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells =>
            _spellsSelector.Spells;

        private bool IsBusy => _currentSpellsHandler is {IsBusy: true};

        public void StartCasting()
        {
            _needCast = true;
            if (IsBusy)
            {
                return;
            }

            CastSelectedSpell();
        }

        public void OnSpellCastPartOfAnimationFinished()
        {
            _currentSpellsHandler?.OnSpellCastPartOfAnimationFinished();
        }

        public void OnAnimatorReadyForNextAnimation()
        {
            _isAnimatorReady = true;
            if (_needCast && !IsBusy)
            {
                CastSelectedSpell();
            }
        }

        public void StopCasting()
        {
            _needCast = false;
            _currentSpellsHandler?.TryInterrupt();
        }

        public void AddSpell(ISpellType spellType, ISpell newSpell)
        {
            _spellsSelector.AddSpell(spellType, newSpell);
        }

        public void AddSpell(ISpell newSpell)
        {
            _spellsSelector.AddSpell(newSpell);
        }

        public void SelectNextSpellType()
        {
            _spellsSelector.SelectNextSpellType();
        }

        public void SelectPreviousSpellType()
        {
            _spellsSelector.SelectPreviousSpellType();
        }

        public void SelectSpellTypeWithIndex(int indexToSelect)
        {
            _spellsSelector.SelectSpellTypeWithIndex(indexToSelect);
        }

        public void HandleSpell(IInformationAboutInstantSpell informationAboutInstantSpell)
        {
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(_instantSpellHandler);
            }

            _currentSpellsHandler = _instantSpellHandler;
            _instantSpellHandler.HandleSpell(informationAboutInstantSpell);
        }

        public void HandleSpell(IInformationAboutContinuousSpell informationAboutContinuousSpell)
        {
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(_continuousSpellHandler);
            }

            _currentSpellsHandler = _continuousSpellHandler;
            _continuousSpellHandler.HandleSpell(informationAboutContinuousSpell);
        }

        protected sealed override void SubscribeOnEvents()
        {
            _spellsSelector.SelectedSpellTypeChanged += OnSelectedSpellTypeChanged;
            _spellsSelector.TryingToUseEmptySpellTypeGroup += OnTryingToUseEmptySpellTypeGroup;

            if (_currentSpellsHandler == null)
            {
                return;
            }

            SubscribeOnSpellsHandler(_currentSpellsHandler);
        }

        protected override void UnsubscribeFromEvents()
        {
            _spellsSelector.SelectedSpellTypeChanged -= OnSelectedSpellTypeChanged;
            _spellsSelector.TryingToUseEmptySpellTypeGroup -= OnTryingToUseEmptySpellTypeGroup;

            if (_currentSpellsHandler == null)
            {
                return;
            }

            UnsubscribeFromSpellsHandler(_currentSpellsHandler);
        }

        private void OnTryingToUseEmptySpellTypeGroup(ISpellType obj)
        {
            TryingToUseEmptySpellTypeGroup?.Invoke(obj);
        }

        private void OnSelectedSpellTypeChanged(ISpellType obj)
        {
            SelectedSpellTypeChanged?.Invoke(obj);
        }

        private void SubscribeOnSpellsHandler(IPlayerSpellsHandler spellsHandler)
        {
            spellsHandler.SpellCanceled += OnSpellCanceled;
            spellsHandler.SpellHandled += OnSpellHandled;
            spellsHandler.NeedPlayContinuousActionAnimation += OnNeedPlayContinuousActionAnimation;
            spellsHandler.NeedPlaySingleActionAnimation += OnNeedPlaySingleActionAnimation;
            spellsHandler.SpellCasted += OnSpellCasted;
        }

        private void UnsubscribeFromSpellsHandler(IPlayerSpellsHandler spellsHandler)
        {
            spellsHandler.SpellCanceled -= OnSpellCanceled;
            spellsHandler.SpellHandled -= OnSpellHandled;
            spellsHandler.NeedPlayContinuousActionAnimation -= OnNeedPlayContinuousActionAnimation;
            spellsHandler.NeedPlaySingleActionAnimation -= OnNeedPlaySingleActionAnimation;
            spellsHandler.SpellCasted -= OnSpellCasted;
        }

        private void OnNeedPlayContinuousActionAnimation(IContinuousActionAnimationData obj)
        {
            NeedPlayContinuousActionAnimation?.Invoke(obj);
        }

        private void OnNeedPlaySingleActionAnimation(IAnimationData obj)
        {
            NeedPlaySingleActionAnimation?.Invoke(obj);
        }

        private void OnSpellCanceled()
        {
            NeedCancelActionAnimations?.Invoke();
        }

        private void CastSelectedSpell()
        {
            if (!_spellsSelector.TryToRememberSelectedSpellInformation())
            {
                return;
            }

            _spellsSelector.RememberedSpell.HandleSpell(this);
            _isAnimatorReady = false;
        }

        private void OnSpellHandled()
        {
            UnsubscribeFromSpellsHandler(_currentSpellsHandler);
            _currentSpellsHandler = null;
            if (_needCast && _isAnimatorReady)
            {
                CastSelectedSpell();
            }
        }

        private void OnSpellCasted()
        {
            _spellsSelector.RemoveRememberedSpell();
        }
    }
}