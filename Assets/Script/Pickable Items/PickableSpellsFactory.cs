using Common.Abstract_Bases;
using Spells.Spell;
using UnityEngine;
using Zenject;

namespace Pickable_Items
{
    public class PickableSpellsFactory : FactoryWithInstantiatorBase, IPickableSpellsFactory
    {
        private readonly PickableSpellController _pickableSpellPrefab;

        public PickableSpellsFactory(IInstantiator instantiator, Transform parentTransform,
            PickableSpellController pickableSpellPrefab) :
            base(instantiator, parentTransform)
        {
            _pickableSpellPrefab = pickableSpellPrefab;
        }

        public IPickableItem Create(ISpell spellToStore, Vector3 position)
        {
            var pickableSpellController =
                InstantiatePrefabForComponent<IPickableSpellController>(_pickableSpellPrefab.gameObject,
                    position, Quaternion.identity);
            pickableSpellController.SetStoredData(spellToStore);
            return pickableSpellController;
        }
    }
}