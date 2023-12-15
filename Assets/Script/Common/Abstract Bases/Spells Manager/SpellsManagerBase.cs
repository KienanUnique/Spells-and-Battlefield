using System;
using Common.Abstract_Bases.Disableable;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
using Common.Animator_Status_Controller;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells;
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
        protected SpellsManagerBase(IContinuousSpellHandlerImplementation continuousSpellHandler,
            IInstantSpellHandlerImplementation instantSpellHandler, ISpellSelectorFoSpellManager spellsSelector,
            IReadonlyAnimatorStatusChecker animatorStatusChecker)
        {
            ContinuousSpellHandler = continuousSpellHandler;
            InstantSpellHandler = instantSpellHandler;
            SpellsSelector = spellsSelector;
            AnimatorStatusChecker = animatorStatusChecker;
        }

        public event Action<IAnimationData> NeedPlaySingleActionAnimation;
        public event Action<IContinuousActionAnimationData> NeedPlayContinuousActionAnimation;
        public event Action NeedCancelActionAnimations;
        public IReadonlyAnimatorStatusChecker AnimatorStatusChecker { get; }

        protected IContinuousSpellHandlerImplementation ContinuousSpellHandler { get; }
        protected IInstantSpellHandlerImplementation InstantSpellHandler { get; }
        protected ISpellSelectorFoSpellManager SpellsSelector { get; }
        protected bool NeedCast { get; private set; }
        protected bool IsAnimatorReady => AnimatorStatusChecker.IsReadyToPlayActionAnimations;
        protected bool IsCurrentSpellContinuous { get; private set; }
        protected ISpellsHandlerImplementationBase CurrentSpellsHandler { get; private set; }

        protected bool IsBusy => CurrentSpellsHandler != null && CurrentSpellsHandler.IsBusy;

        protected bool CanCastNextSpell => NeedCast && !IsBusy && IsAnimatorReady;

        public virtual void HandleSpell(IInformationAboutContinuousSpell informationAboutContinuousSpell)
        {
            IsCurrentSpellContinuous = true;
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(ContinuousSpellHandler);
            }

            CurrentSpellsHandler = ContinuousSpellHandler;
            ContinuousSpellHandler.HandleSpell(informationAboutContinuousSpell);
        }

        public virtual void HandleSpell(IInformationAboutInstantSpell informationAboutInstantSpell)
        {
            IsCurrentSpellContinuous = false;
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(InstantSpellHandler);
            }

            CurrentSpellsHandler = InstantSpellHandler;
            InstantSpellHandler.HandleSpell(informationAboutInstantSpell);
        }

        public virtual void StartCasting()
        {
            NeedCast = true;
            if (!CanCastNextSpell)
            {
                return;
            }

            CastSelectedSpell();
        }

        public virtual void StopCasting()
        {
            NeedCast = false;
            CurrentSpellsHandler?.TryInterrupt();
        }

        protected virtual void OnSpellCastPartOfAnimationFinished()
        {
            if (IsBusy)
            {
                CurrentSpellsHandler.OnSpellCastPartOfAnimationFinished();
            }
        }

        protected virtual void OnAnimatorReadyForNextAnimation()
        {
            if (CanCastNextSpell)
            {
                CastSelectedSpell();
            }
        }

        protected virtual void OnSpellCanceled()
        {
            UnsubscribeFromSpellsHandler(CurrentSpellsHandler);
            CurrentSpellsHandler = null;
            NeedCancelActionAnimations?.Invoke();
        }

        protected virtual void CastSelectedSpell()
        {
            if (!SpellsSelector.TryToRememberSelectedSpellInformation())
            {
                return;
            }

            if (IsBusy)
            {
                throw new InvalidOperationException("Trying to cast spell while manager is busy");
            }

            SpellsSelector.RememberedSpell.HandleSpell(this);
        }

        protected virtual void OnSpellHandled()
        {
            if (IsCurrentSpellContinuous)
            {
                NeedCancelActionAnimations?.Invoke();
            }

            IsCurrentSpellContinuous = false;
            UnsubscribeFromSpellsHandler(CurrentSpellsHandler);
            CurrentSpellsHandler = null;
            if (CanCastNextSpell)
            {
                CastSelectedSpell();
            }
        }

        protected virtual void OnSpellCasted()
        {
            SpellsSelector.RemoveRememberedSpell();
        }

        protected override void SubscribeOnEvents()
        {
            ContinuousSpellHandler.Enable();
            InstantSpellHandler.Enable();
            AnimatorStatusChecker.ActionAnimationKeyMomentTrigger += OnSpellCastPartOfAnimationFinished;
            AnimatorStatusChecker.AnimatorReadyToPlayActionsAnimations += OnAnimatorReadyForNextAnimation;
            if (CurrentSpellsHandler == null)
            {
                return;
            }

            SubscribeOnSpellsHandler(CurrentSpellsHandler);
        }

        protected override void UnsubscribeFromEvents()
        {
            ContinuousSpellHandler.Disable();
            InstantSpellHandler.Disable();
            if (CurrentSpellsHandler == null)
            {
                return;
            }

            UnsubscribeFromSpellsHandler(CurrentSpellsHandler);
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
    }
}