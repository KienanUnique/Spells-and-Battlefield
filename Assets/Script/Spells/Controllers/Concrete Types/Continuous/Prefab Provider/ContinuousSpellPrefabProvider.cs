using Common.Abstract_Bases;
using UnityEngine;

namespace Spells.Controllers.Concrete_Types.Continuous.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Continuous Spell Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.ContinuousSpellDirectory + "Continuous Spell Prefab Provider",
        order = 0)]
    public class ContinuousSpellPrefabProvider : PrefabProviderScriptableObjectBase, IContinuousSpellPrefabProvider
    {
        [SerializeField] private ContinuousSpellObjectController _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}