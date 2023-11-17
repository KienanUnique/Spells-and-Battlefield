using Common.Animation_Data;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;

namespace Spells.Controllers.Concrete_Types.Instant
{
    public interface IInformationAboutInstantSpell : IInformationAboutSpell<IDataForInstantSpellController,
        IAnimationData, IInstantSpellPrefabProvider>
    {
    }
}