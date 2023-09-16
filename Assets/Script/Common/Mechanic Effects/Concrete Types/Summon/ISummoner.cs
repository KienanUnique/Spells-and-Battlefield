using Common.Abstract_Bases.Character;
using Common.Readonly_Transform;

namespace Common.Mechanic_Effects.Concrete_Types.Summon
{
    public interface ISummoner : ICharacterInformationProvider
    {
        public IReadonlyTransform MainTransform { get; }
    }
}