namespace Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information
{
    public interface IHitPointsCharacterChangeInformation
    {
        public int CurrentHitPointsCount { get; }
        public float CurrentHitPointCountRatio { get; }
        public int HitPointsChangeValue { get; }
        public TypeOfHitPointsChange TypeOfHitPointsChange { get; }
    }
}