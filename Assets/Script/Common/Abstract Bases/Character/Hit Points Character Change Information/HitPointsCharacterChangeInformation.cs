using Common.Mechanic_Effects.Source;

namespace Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information
{
    public class HitPointsCharacterChangeInformation : IHitPointsCharacterChangeInformation
    {
        public HitPointsCharacterChangeInformation(int currentHitPointsCount, float currentHitPointCountRatio,
            int hitPointsChangeValue, TypeOfHitPointsChange typeOfHitPointsChange,
            IEffectSourceInformation sourceInformation)
        {
            CurrentHitPointsCount = currentHitPointsCount;
            CurrentHitPointCountRatio = currentHitPointCountRatio;
            HitPointsChangeValue = hitPointsChangeValue;
            TypeOfHitPointsChange = typeOfHitPointsChange;
            SourceInformation = sourceInformation;
        }

        public int CurrentHitPointsCount { get; }
        public float CurrentHitPointCountRatio { get; }
        public int HitPointsChangeValue { get; }
        public TypeOfHitPointsChange TypeOfHitPointsChange { get; }
        public IEffectSourceInformation SourceInformation { get; }
    }
}