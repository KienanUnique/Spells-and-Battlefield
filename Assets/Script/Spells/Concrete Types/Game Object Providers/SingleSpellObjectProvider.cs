using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Controllers;
using UnityEngine;

namespace Spells.Concrete_Types.Game_Object_Providers
{
    [CreateAssetMenu(fileName = "Single Spell Object Provider",
        menuName = ScriptableObjectsMenuDirectories.SpellObjectsProvidersDirectory + "Single Spell Object Provider",
        order = 0)]
    public class SingleSpellObjectProvider : SpellGameObjectProviderScriptableObject
    {
        [SerializeField] private SingleSpellObjectController _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}