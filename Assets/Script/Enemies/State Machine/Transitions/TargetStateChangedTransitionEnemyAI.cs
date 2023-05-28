using Common.Abstract_Bases.Character;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.Transitions
{
    public class TargetStateChangedTransitionEnemyAI : TransitionEnemyAI
    {
        [SerializeField] private CharacterState _needState;
        private ICharacter _targetCharacter = null;
        private int _lastCashedTargetId;
        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetSelector.CurrentTarget;

        protected override void SpecialActionOnStartChecking()
        {
            TryUpdateCashedCharacter();
        }

        protected override void CheckConditions()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            if (_targetCharacter != null && _targetCharacter.Id == CurrentTarget.Id &&
                _targetCharacter.CurrentCharacterState.Value == _needState)
            {
                InvokeTransitionEvent();
            }
            else if (_lastCashedTargetId != CurrentTarget.Id)
            {
                TryUpdateCashedCharacter();
            }
        }

        private void TryUpdateCashedCharacter()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            if ((_targetCharacter == null || _targetCharacter.Id != CurrentTarget.Id) &&
                CurrentTarget is ICharacter targetCharacter)
            {
                _targetCharacter = targetCharacter;
            }

            _lastCashedTargetId = CurrentTarget.Id;
        }
    }
}