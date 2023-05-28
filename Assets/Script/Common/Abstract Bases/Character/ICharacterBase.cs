using System;
using Spells.Continuous_Effect;

namespace Common.Abstract_Bases.Character
{
    public interface ICharacterBase
    {
        event Action<CharacterState> StateChanged;
        event Action<float> HitPointsCountChanged;
        float HitPointCountRatio { get; }
        ValueWithReactionOnChange<CharacterState> CurrentState { get; }
        void HandleHeal(int countOfHitPoints);
        void HandleDamage(int countOfHitPoints);
        void ApplyContinuousEffect(IAppliedContinuousEffect effect);
    }
}