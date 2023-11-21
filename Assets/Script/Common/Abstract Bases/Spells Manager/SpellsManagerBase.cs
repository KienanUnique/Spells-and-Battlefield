using System;
using Common.Abstract_Bases.Disableable;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
using Spells;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Spell_Handlers;
using Spells.Spell_Handlers.Continuous;
using Spells.Spell_Handlers.Instant;

namespace Common.Abstract_Bases.Spells_Manager
{
    public abstract class SpellsManagerBase : BaseWithDisabling, ISpellManager, ISpellHandler
    {
        private readonly IContinuousSpellHandlerImplementation _continuousSpellHandler;
        private readonly IInstantSpellHandlerImplementation _instantSpellHandler;
        private readonly ISpellSelectorFoSpellManager _spellsSelector;
        private bool _needCast;
        private bool _isAnimatorReady = true;
        private bool _isCurrentSpellContinuous;
        private ISpellsHandlerImplementationBase _currentSpellsHandler;

        protected SpellsManagerBase(IContinuousSpellHandlerImplementation continuousSpellHandler,
            IInstantSpellHandlerImplementation instantSpellHandler, ISpellSelectorFoSpellManager spellsSelector)
        {
            _continuousSpellHandler = continuousSpellHandler;
            _instantSpellHandler = instantSpellHandler;
            _spellsSelector = spellsSelector;
        }

        public event Action<IAnimationData> NeedPlaySingleActionAnimation;
        public event Action<IContinuousActionAnimationData> NeedPlayContinuousActionAnimation;
        public event Action NeedCancelActionAnimations;

        protected IContinuousSpellHandlerImplementation ContinuousSpellHandler => _continuousSpellHandler;
        protected IInstantSpellHandlerImplementation InstantSpellHandler => _instantSpellHandler;
        protected ISpellSelectorFoSpellManager SpellsSelector => _spellsSelector;
        protected bool NeedCast => _needCast;
        protected bool IsAnimatorReady => _isAnimatorReady;
        protected bool IsCurrentSpellContinuous => _isCurrentSpellContinuous;
        protected ISpellsHandlerImplementationBase CurrentSpellsHandler => _currentSpellsHandler;
        protected bool IsBusy => _currentSpellsHandler is {IsBusy: true};

        public virtual void StartCasting()
        {
            _needCast = true;
            if (IsBusy)
            {
                return;
            }

            CastSelectedSpell();
        }

        public virtual void OnSpellCastPartOfAnimationFinished()
        {
            _currentSpellsHandler?.OnSpellCastPartOfAnimationFinished();
        }

        public virtual void OnAnimatorReadyForNextAnimation()
        {
            _isAnimatorReady = true;
            if (_needCast && !IsBusy)
            {
                CastSelectedSpell();
            }
        }

        public virtual void StopCasting()
        {
            _needCast = false;
            _currentSpellsHandler?.TryInterrupt();
        }

        public virtual void HandleSpell(IInformationAboutInstantSpell informationAboutInstantSpell)
        {
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(_instantSpellHandler);
            }

            _currentSpellsHandler = _instantSpellHandler;
            _instantSpellHandler.HandleSpell(informationAboutInstantSpell);
        }

        public virtual void HandleSpell(IInformationAboutContinuousSpell informationAboutContinuousSpell)
        {
            _isCurrentSpellContinuous = true;
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(_continuousSpellHandler);
            }

            _currentSpellsHandler = _continuousSpellHandler;
            _continuousSpellHandler.HandleSpell(informationAboutContinuousSpell);
        }

        protected override void SubscribeOnEvents()
        {
            if (_currentSpellsHandler == null)
            {
                return;
            }

            SubscribeOnSpellsHandler(_currentSpellsHandler);
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_currentSpellsHandler == null)
            {
                return;
            }

            UnsubscribeFromSpellsHandler(_currentSpellsHandler);
        }

        private void SubscribeOnSpellsHandler(ISpellsHandlerImplementationBase spellsHandler)
        {
            spellsHandler.SpellCanceled += OnSpellCanceled;
            spellsHandler.SpellHandled += OnSpellHandled;
            spellsHandler.NeedPlayContinuousActionAnimation += OnNeedPlayContinuousActionAnimation;
            spellsHandler.NeedPlaySingleActionAnimation += OnNeedPlaySingleActionAnimation;
            spellsHandler.SpellCasted += OnSpellCasted;
        }

        private void UnsubscribeFromSpellsHandler(ISpellsHandlerImplementationBase spellsHandler)
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

        protected virtual void OnSpellCanceled()
        {
            NeedCancelActionAnimations?.Invoke();
            _isAnimatorReady = false;
        }

        protected virtual void CastSelectedSpell()
        {
            if (!_spellsSelector.TryToRememberSelectedSpellInformation())
            {
                return;
            }

            _spellsSelector.RememberedSpell.HandleSpell(this);
            _isAnimatorReady = false;
        }

        protected virtual void OnSpellHandled()
        {
            if (_isCurrentSpellContinuous)
            {
                NeedCancelActionAnimations?.Invoke();
            }

            _isCurrentSpellContinuous = false;
            UnsubscribeFromSpellsHandler(_currentSpellsHandler);
            _currentSpellsHandler = null;
            if (_needCast && _isAnimatorReady)
            {
                CastSelectedSpell();
            }
        }

        protected virtual void OnSpellCasted()
        {
            _spellsSelector.RemoveRememberedSpell();
        }
    }
}