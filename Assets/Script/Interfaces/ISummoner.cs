using Common.Readonly_Transform;

namespace Interfaces
{
    public interface ISummoner : ICharacterInformationProvider
    {
        public IReadonlyTransform MainTransform { get; }
    }
}