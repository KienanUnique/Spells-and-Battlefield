using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;

namespace Spells
{
    public interface ICaster : IInteractable
    {
        public IReadonlyTransform UpperPointForSummonedEnemiesPositionCalculating { get; }
        public IInformationForSummon InformationForSummon { get; }
        public IToolsForSummon ToolsForSummon { get; }
    }
}