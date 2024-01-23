using Common.Animation_Data.Continuous_Action;
using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IInformationAboutContinuousSpell : IInformationAboutSpell<
        IDataForActivationContinuousSpellObjectController, IContinuousActionAnimationData,
        IContinuousSpellPrefabProvider>
    {
        public IDataForActivationContinuousSpellObjectController GetActivationData(ICaster caster,
            IReadonlyTransform castPoint);
    }
}