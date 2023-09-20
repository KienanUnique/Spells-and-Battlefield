using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;

namespace Spells
{
    public interface ICaster
    {
        public IReadonlyTransform MainTransform { get; }
        public IReadonlyTransform UpperPointForSummonedEnemiesPositionCalculating { get; }
        public IInformationForSummon InformationForSummon { get; }
        public IToolsForSummon ToolsForSummon { get; }
    }
}