using Common.Animation_Data;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Controllers.Concrete_Types.Instant.Prefab_Provider;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Instant
{
    [CreateAssetMenu(fileName = "Instant Spell",
        menuName = ScriptableObjectsMenuDirectories.InstantSpellDirectory + "Instant Spell", order = 0)]
    public class InstantSpellScriptableObject : SpellScriptableObjectBase, IInformationAboutInstantSpell
    {
        [SerializeField] private DataForInstantSpellControllerProvider _dataForController;
        [SerializeField] private AnimationData _animationData;
        [SerializeField] private InstantSpellPrefabProvider _prefabProvider;

        public IDataForInstantSpellController DataForController =>
            _dataForController.GetImplementationObject(SpellType, Movement, LookPointCalculator);

        public IAnimationData AnimationData => _animationData;
        public IInstantSpellPrefabProvider PrefabProvider => _prefabProvider;

        public override void HandleSpell(ISpellHandler handler)
        {
            handler.HandleSpell(this);
        }
    }
}