using System;
using Common;
using Common.Abstract_Bases.Character;

namespace Interfaces
{
    public interface ICharacterInformationProvider
    {
        public delegate void OnHitPointsCountChanged(int hitPointsLeft, int hitPointsChangeValue, TypeOfHitPointsChange typeOfHitPointsChange);
        public event Action<CharacterState> CharacterStateChanged;
        public event OnHitPointsCountChanged HitPointsCountChanged;
        public CharacterState CurrentCharacterState { get; }
        public float HitPointCountRatio { get; }
    }
}