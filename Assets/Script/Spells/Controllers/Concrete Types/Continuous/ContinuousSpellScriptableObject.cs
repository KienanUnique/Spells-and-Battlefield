using Common.Animation_Data.Continuous_Action;
using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Continuous.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    [CreateAssetMenu(fileName = "Continuous Spell",
        menuName = ScriptableObjectsMenuDirectories.ContinuousSpellDirectory + "Continuous Spell", order = 0)]
    public class ContinuousSpellScriptableObject : SpellScriptableObjectBase, IInformationAboutContinuousSpell
    {
        [SerializeField] private DataForContinuousSpellControllerProvider _dataForController;
        [SerializeField] private ContinuousActionAnimationData _animationData;
        [SerializeField] private ContinuousSpellPrefabProvider _prefabProvider;

        public IContinuousActionAnimationData AnimationData => _animationData;

        public IContinuousSpellPrefabProvider PrefabProvider => _prefabProvider;

        public override void HandleSpell(ISpellHandler handler)
        {
            handler.HandleSpell(this);
        }

        public IDataForActivationContinuousSpellObjectController GetActivationData(ICaster caster,
            IReadonlyTransform castPoint)
        {
            return _dataForController.GetImplementationObject(SpellType, Movement, LookPointCalculator);
        }
    }
}