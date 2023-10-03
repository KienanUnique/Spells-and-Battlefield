using Common.Readonly_Transform;

namespace Common.Mechanic_Effects.Source
{
    public class EffectSourceInformation : IEffectSourceInformation
    {
        public EffectSourceInformation(EffectSourceType sourceType, IReadonlyTransform sourceTransform)
        {
            SourceType = sourceType;
            SourceTransform = sourceTransform;
        }

        public EffectSourceType SourceType { get; }
        public IReadonlyTransform SourceTransform { get; }
    }
}