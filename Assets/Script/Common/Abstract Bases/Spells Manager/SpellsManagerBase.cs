using System;
using Common.Abstract_Bases.Disableable;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells;
using Spells;
using Spells.Controllers.Concrete_Types.Continuous;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Spell_Handlers;
using Spells.Spell_Handlers.Continuous;
using Spells.Spell_Handlers.Instant;
using UnityEngine;

namespace Common.Abstract_Bases.Spells_Manager
{
    public abstract class SpellsManagerBase : BaseWithDisabling, ISpellManager, ISpellHandler
    {
        protected SpellsManagerBase(IContinuousSpellHandlerImplementation continuousSpellHandler,
            IInstantSpellHandlerImplementation instantSpellHandler, ISpellSelectorFoSpellManager spellsSelector)
        {
            ContinuousSpellHandler = continuousSpellHandler;
            InstantSpellHandler = instantSpellHandler;
            SpellsSelector = spellsSelector;
        }

        public event Action<IAnimationData> NeedPlaySingleActionAnimation;
        public event Action<IContinuousActionAnimationData> NeedPlayContinuousActionAnimation;
        public event Action NeedCancelActionAnimations;

        protected IContinuousSpellHandlerImplementation ContinuousSpellHandler { get; }
        protected IInstantSpellHandlerImplementation InstantSpellHandler { get; }
        protected ISpellSelectorFoSpellManager SpellsSelector { get; }
        protected bool NeedCast { get; private set; }
        protected bool IsAnimatorReady { get; private set; } = true;
        protected bool IsCurrentSpellContinuous { get; private set; }
        protected ISpellsHandlerImplementationBase CurrentSpellsHandler { get; private set; }

        protected bool IsBusy => CurrentSpellsHandler != null && CurrentSpellsHandler.IsBusy;

        protected bool CanCastNextSpell
        {
            get
            {
                bool canCastNextSpell = NeedCast && !IsBusy && IsAnimatorReady;
                // Debug.Log(
                //     $"CanCastNextSpell: {canCastNextSpell}; NeedCast: {NeedCast};  IsAnimatorReady: {IsAnimatorReady}; CurrentSpellsHandler != null {CurrentSpellsHandler != null}; IsBusy: {IsBusy}");
                return canCastNextSpell;
            }
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

        public virtual void OnSpellCastPartOfAnimationFinished()
        {
            if (IsBusy)
            {
                CurrentSpellsHandler.OnSpellCastPartOfAnimationFinished();
            }
        }

        public virtual void OnAnimatorReadyForNextAnimation()
        {
            IsAnimatorReady = true;
            if (CanCastNextSpell)
            {
                CastSelectedSpell();
            }
        }

        public virtual void StopCasting()
        {
            NeedCast = false;
            CurrentSpellsHandler?.TryInterrupt();
        }

        public virtual void HandleSpell(IInformationAboutInstantSpell informationAboutInstantSpell)
        {
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(InstantSpellHandler);
            }

            //Debug.Log($"Handle InstantSpell, is null {informationAboutInstantSpell == null}");

            CurrentSpellsHandler = InstantSpellHandler;
            InstantSpellHandler.HandleSpell(informationAboutInstantSpell);
        }

        public virtual void HandleSpell(IInformationAboutContinuousSpell informationAboutContinuousSpell)
        {
            IsCurrentSpellContinuous = true;
            if (IsEnabled)
            {
                SubscribeOnSpellsHandler(ContinuousSpellHandler);
            }

            //Debug.Log($"Handle ContinuousSpell, is null {informationAboutContinuousSpell == null}");

            CurrentSpellsHandler = ContinuousSpellHandler;
            ContinuousSpellHandler.HandleSpell(informationAboutContinuousSpell);
        }

        protected override void SubscribeOnEvents()
        {
            ContinuousSpellHandler.Enable();
            InstantSpellHandler.Enable();
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
            //Debug.Log($"SubscribeOnSpellsHandler {IsCurrentSpellContinuous}");
            spellsHandler.SpellCanceled += OnSpellCanceled;
            spellsHandler.SpellHandled += OnSpellHandled;
            spellsHandler.NeedPlayContinuousActionAnimation += OnNeedPlayContinuousActionAnimation;
            spellsHandler.NeedPlaySingleActionAnimation += OnNeedPlaySingleActionAnimation;
            spellsHandler.SpellCasted += OnSpellCasted;
        }

        private void UnsubscribeFromSpellsHandler(ISpellsHandlerImplementationBase spellsHandler)
        {
            //Debug.Log($"UnsubscribeFromSpellsHandler {IsCurrentSpellContinuous}");
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
            IsAnimatorReady = true;
            UnsubscribeFromSpellsHandler(CurrentSpellsHandler);
            CurrentSpellsHandler = null;
            //Debug.Log($"OnSpellCanceled; is cont: {IsCurrentSpellContinuous} ");
            NeedCancelActionAnimations?.Invoke();
        }

        protected virtual void CastSelectedSpell()
        {
            if (!SpellsSelector.TryToRememberSelectedSpellInformation())
            {
                //Debug.Log($"Can not cast now");
                return;
            }

            if (IsBusy)
            {
                throw new InvalidOperationException("Trying to cast spell while manager is busy");
            }

            SpellsSelector.RememberedSpell.HandleSpell(this);
            IsAnimatorReady = false;
        }

        protected virtual void OnSpellHandled()
        {
            if (IsCurrentSpellContinuous)
            {
                NeedCancelActionAnimations?.Invoke();
            }

            //Debug.Log($"OnSpellHandled; is cont: {IsCurrentSpellContinuous} ");

            IsCurrentSpellContinuous = false;
            IsAnimatorReady = true;
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
    }
}