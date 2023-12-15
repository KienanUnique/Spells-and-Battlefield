using Common.Abstract_Bases;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Instant.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Instant Spell Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.InstantSpellDirectory + "Instant Spell Prefab Provider", order = 0)]
    public class InstantSpellPrefabProvider : PrefabProviderScriptableObjectBase, IInstantSpellPrefabProvider
    {
        [SerializeField] private InstantSpellObjectController _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}