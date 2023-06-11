﻿using Pickable_Items.Strategies_For_Pickable_Controller;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using UnityEngine;

namespace Pickable_Items.Data_For_Creating.Scriptable_Object.Concrete_Types
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.PickableItemsDirectory + "Pickable Effect",
        fileName = "Pickable Effect", order = 0)]
    public class PickableEffectScriptableObject : Pickable3DObjectScriptableObject
    {
        [SerializeField] private SpellMechanicEffectScriptableObject _effect;

        public override IStrategyForPickableController StrategyForController =>
            new StrategyForEffectsForPickableController(_effect.GetImplementationObject());
    }
}