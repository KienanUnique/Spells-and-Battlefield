using System;
using Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information;

namespace Common.Abstract_Bases.Character
{
    public interface ICharacterInformationProvider
    {
        public event Action<CharacterState> CharacterStateChanged;
        public event Action<IHitPointsCharacterChangeInformation> HitPointsCountChanged;

        public CharacterState CurrentCharacterState { get; }
        public float HitPointCountRatio { get; }
    }
}