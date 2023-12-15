using System;
using Common.Abstract_Bases.Spells_Manager;
using Common.Animator_Status_Controller;
using Enemies.Look_Point_Calculator;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors;
using Spells.Spell_Handlers.Continuous;
using Spells.Spell_Handlers.Instant;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spells_Manager
{
    public class EnemySpellsManager : SpellsManagerBase, IEnemySpellsManager
    {
        private readonly IEnemySpellSelector _spellsSelector;
        private readonly ILookPointCalculator _defaultLookPointCalculator;

        public EnemySpellsManager(IContinuousSpellHandlerImplementation continuousSpellHandler,
            IInstantSpellHandlerImplementation instantSpellHandler, IEnemySpellSelector spellsSelector,
            IReadonlyAnimatorStatusChecker animatorStatusChecker, ILookPointCalculator defaultLookPointCalculator) :
            base(continuousSpellHandler, instantSpellHandler, spellsSelector, animatorStatusChecker)
        {
            _spellsSelector = spellsSelector;
            _defaultLookPointCalculator = defaultLookPointCalculator;
        }

        public event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public event Action StoppedCasting;
        public event Action FinishedCasting;
        public new bool IsBusy => base.IsBusy;
        public event Action CanUseSpellsAgain;
        public bool CanUseSpell => _spellsSelector.CanUseSpell;

        public override void StopCasting()
        {
            base.StopCasting();
            InformIfStoppedCasting();
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _spellsSelector.CanUseSpellsAgain += OnCanUseSpellsAgain;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _spellsSelector.CanUseSpellsAgain -= OnCanUseSpellsAgain;
        }

        protected override void OnSpellCanceled()
        {
            base.OnSpellCanceled();
            FinishedCasting?.Invoke();
            InformIfStoppedCasting();
        }

        protected override void CastSelectedSpell()
        {
            base.CastSelectedSpell();
            NeedChangeLookPointCalculator?.Invoke(_spellsSelector.RememberedSpell != null
                ? _spellsSelector.RememberedSpell.LookPointCalculator
                : _defaultLookPointCalculator);
        }

        protected override void OnSpellHandled()
        {
            base.OnSpellHandled();
            FinishedCasting?.Invoke();
            InformIfStoppedCasting();
        }

        private void InformIfStoppedCasting()
        {
            if (!NeedCast && !IsBusy)
            {
                StoppedCasting?.Invoke();
            }
        }

        private void OnCanUseSpellsAgain()
        {
            CanUseSpellsAgain?.Invoke();
            if (CanCastNextSpell)
            {
                CastSelectedSpell();
            }
        }
    }
}