using Common.Readonly_Transform;

namespace Common.Mechanic_Effects.Source
{
    public interface IEffectSourceInformation
    {
        public EffectSourceType SourceType { get; }
        public IReadonlyTransform SourceTransform { get; }
    }
}