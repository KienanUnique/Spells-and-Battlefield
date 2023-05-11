using System;
using Spells;
using UnityEngine;
using Zenject;

namespace Pickable_Items
{
    [Serializable]
    public class PickableSpellsFactory : IPickableSpellsFactory
    {
        [SerializeField] private PickableSpellController _pickableSpellPrefab;
        private IInstantiator _instantiator;

        public void SetInstantiator(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IPickableItem Create(ISpell spellToStore, Vector3 position)
        {
            var pickableSpellController =
                _instantiator.InstantiatePrefabForComponent<PickableSpellController>(_pickableSpellPrefab, position,
                    Quaternion.identity, null);
            pickableSpellController.SetStoredData(spellToStore);
            return pickableSpellController;
        }
    }
}