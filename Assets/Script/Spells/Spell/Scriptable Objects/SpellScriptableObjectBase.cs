using Enemies.Look_Point_Calculator;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Spell.Scriptable_Objects
{
    public abstract class SpellScriptableObjectBase : PickableCardScriptableObjectBase, ISpell
    {
        [SerializeField] private SpellMovementScriptableObject _movement;
        [SerializeField] private SpellTypeScriptableObject _type;

        public override IStrategyForPickableController StrategyForController =>
            new StrategyForSpellsForPickableController(this);

        public ISpellType SpellType => _type.GetImplementationObject();
        public ILookPointCalculator LookPointCalculator => _movement.GetImplementationObject().GetLookPointCalculator();
        protected ISpellMovement Movement => _movement.GetImplementationObject();

        public abstract void HandleSpell(ISpellHandler handler);

        public new bool Equals(object x, object y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}