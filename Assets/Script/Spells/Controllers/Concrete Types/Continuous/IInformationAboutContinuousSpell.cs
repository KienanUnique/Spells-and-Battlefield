using Common.Animation_Data.Continuous_Action;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IInformationAboutContinuousSpell : IInformationAboutSpell<IDataForContinuousSpellController,
        IContinuousActionAnimationData, IContinuousSpellPrefabProvider>
    {
    }
}