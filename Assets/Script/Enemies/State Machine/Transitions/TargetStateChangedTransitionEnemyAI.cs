using Common.Abstract_Bases.Character;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.Transitions
{
    public class TargetStateChangedTransitionEnemyAI : TransitionEnemyAI
    {
        [SerializeField] private CharacterState _needState;
        private IInteractableCharacter _targetInteractableCharacter = null;
        private int _lastCashedTargetId;
        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;

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

            if (_targetInteractableCharacter != null && _targetInteractableCharacter.Id == CurrentTarget.Id &&
                _targetInteractableCharacter.CurrentCharacterState == _needState)
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

            if ((_targetInteractableCharacter == null || _targetInteractableCharacter.Id != CurrentTarget.Id) &&
                CurrentTarget is IInteractableCharacter targetCharacter)
            {
                _targetInteractableCharacter = targetCharacter;
            }

            _lastCashedTargetId = CurrentTarget.Id;
        }
    }
}