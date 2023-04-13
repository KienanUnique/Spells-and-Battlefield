using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.Transitions
{
    public class TargetStateChangedTransition : Transition
    {
        [SerializeField] private CharacterState _needState;
        private ICharacter _targetCharacter = null;

        protected override void CheckConditions()
        {
            if ((_targetCharacter == null || _targetCharacter.Id != StateMachineControllable.Target.Id) &&
                StateMachineControllable.Target is ICharacter targetCharacter)
            {
                _targetCharacter = targetCharacter;
            }
            else if (_targetCharacter != null && _targetCharacter.Id == StateMachineControllable.Target.Id &&
                     _targetCharacter.CurrentCharacterState == _needState)
            {
                NeedTransit = true;
            }
        }
    }
}