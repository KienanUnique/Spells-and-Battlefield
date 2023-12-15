using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Factory;

namespace Spells.Controllers.Concrete_Types.Instant
{
    public interface IInitializableInstantSpellController : IInstantSpellController
    {
        public void Initialize(IDataForInstantSpellController spellControllerData, ICaster caster,
            ISpellObjectsFactory spellObjectsFactory, IReadonlyTransform castPoint);
    }
}