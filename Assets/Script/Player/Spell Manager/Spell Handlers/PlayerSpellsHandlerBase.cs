﻿using System;
using Common.Abstract_Bases.Disableable;
using Common.Animation_Data;
using Common.Readonly_Transform;
using Player.Look;
using Spells;
using Spells.Factory;

namespace Player.Spell_Manager.Spell_Handlers
{
    public abstract class PlayerSpellsHandlerBase : BaseWithDisabling, IPlayerSpellsHandler
    {
        protected readonly ICaster _caster;
        protected readonly ISpellObjectsFactory _spellObjectsFactory;
        protected readonly IReadonlyTransform _spellSpawnObject;
        protected readonly IReadonlyPlayerLook _look;

        protected PlayerSpellsHandlerBase(ICaster caster, ISpellObjectsFactory spellObjectsFactory,
            IReadonlyTransform spellSpawnObject, IReadonlyPlayerLook look)
        {
            _caster = caster;
            _spellObjectsFactory = spellObjectsFactory;
            _spellSpawnObject = spellSpawnObject;
            _look = look;
        }

        public event Action SpellCasted;
        public event Action<IContinuousActionAnimationData> NeedPlayContinuousActionAnimation;
        public event Action<IAnimationData> NeedPlaySingleActionAnimation;
        public event Action SpellCanceled;
        public event Action SpellHandled;
        public bool IsBusy { get; private set; }
        public abstract bool TryInterrupt();
        public abstract void OnSpellCastPartOfAnimationFinished();

        protected virtual void HandleStart()
        {
            if (IsBusy)
            {
                throw new SpellHandlerAlreadyBusyException();
            }

            IsBusy = true;
        }

        protected virtual void HandleEndOfCast()
        {
            SpellCasted?.Invoke();
        }

        protected virtual void HandleEndOfSpell()
        {
            IsBusy = false;
            SpellHandled?.Invoke();
        }

        protected virtual void Cancel()
        {
            IsBusy = false;
            SpellCanceled?.Invoke();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected void PlayContinuousActionAnimation(IContinuousActionAnimationData animationData)
        {
            NeedPlayContinuousActionAnimation?.Invoke(animationData);
        }

        protected void PlaySingleActionAnimation(IAnimationData animationData)
        {
            NeedPlaySingleActionAnimation?.Invoke(animationData);
        }
    }
}