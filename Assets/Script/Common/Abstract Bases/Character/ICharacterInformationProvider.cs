using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information;
using Common.Mechanic_Effects.Continuous_Effect;

namespace Common.Abstract_Bases.Character
{
    public interface ICharacterInformationProvider
    {
        public event Action<CharacterState> CharacterStateChanged;
        public event Action<IHitPointsCharacterChangeInformation> HitPointsCountChanged;
        public event Action<IAppliedContinuousEffectInformation> ContinuousEffectAdded;

        public CharacterState CurrentCharacterState { get; }
        public float HitPointCountRatio { get; }
        public IReadOnlyList<IAppliedContinuousEffectInformation> CurrentContinuousEffects { get; }
    }
}