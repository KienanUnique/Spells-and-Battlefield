using Common.Readonly_Transform;

namespace Interfaces
{
    public interface ICaster
    {
        public IReadonlyTransform MainTransform { get; }
        public IReadonlyTransform UpperPointForSummonPointCalculating { get; }
        public IInformationForSummon InformationForSummon { get; }
        public IToolsForSummon ToolsForSummon { get; }
    }
}