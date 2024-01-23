using Common.Animation_Data;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;

namespace Spells.Controllers.Concrete_Types.Instant
{
    public interface IInformationAboutInstantSpell : IInformationAboutSpell<
        IDataForActivationInstantSpellObjectController, IAnimationData, IInstantSpellPrefabProvider>
    {
        public IDataForActivationInstantSpellObjectController GetActivationData(ICaster caster);
    }
}