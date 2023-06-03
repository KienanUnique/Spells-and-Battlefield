using Pickable_Items.Data_For_Creating.Scriptable_Object;
using Pickable_Items.Prefab_Provider;
using Pickable_Items.Prefab_Provider.Concrete_Types;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Spell.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Spell",
        menuName = ScriptableObjectsMenuDirectories.SpellSystemDirectory + "Spell", order = 0)]
    public class SpellScriptableObject : PickableCardScriptableObjectBase, ISpell
    {
        [SerializeField] private SpellAnimationInformation _animationInformation;
        [SerializeField] private SpellDataForSpellControllerProvider _dataForSpellController;
        [SerializeField] private SpellPrefabProviderScriptableObject _spellPrefabProvider;
        [SerializeField] private PickableCardPrefabProvider _cardPrefabProvider;

        public ISpellAnimationInformation SpellAnimationInformation => _animationInformation;
        public ISpellPrefabProvider SpellPrefabProvider => _spellPrefabProvider;

        public ISpellDataForSpellController SpellDataForSpellController =>
            _dataForSpellController.GetImplementationObject();

        public override IPickableItemPrefabProvider PickableItemPrefabProvider => _cardPrefabProvider;

        public override IStrategyForPickableController StrategyForController =>
            new StrategyForSpellsForPickableController(this);
    }
}