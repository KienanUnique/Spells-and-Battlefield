using Common.Animation_Data;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IInformationAboutContinuousSpell : IInformationAboutSpell<IDataForContinuousSpellController,
        IContinuousActionAnimationData, IContinuousSpellPrefabProvider>
    {
    }
}