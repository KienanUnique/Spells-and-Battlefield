using Common.Abstract_Bases;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Instant.Prefab_Provider
{
    public class InstantSpellPrefabProvider : PrefabProviderScriptableObjectBase, IInstantSpellPrefabProvider
    {
        [SerializeField] private InstantSpellObjectController _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}