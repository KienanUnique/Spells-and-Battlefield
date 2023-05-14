using Interfaces;
using Spells.Controllers;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;
using Zenject;

namespace Spells.Factory
{
    public class SpellObjectsFactory : ISpellObjectsFactory
    {
        private readonly IInstantiator _instantiator;

        public SpellObjectsFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void Create(ISpellDataForSpellController spellData,
            ISpellGameObjectProvider spellGameObjectProvider, ICaster caster,
            Vector3 spawnPosition, Quaternion spawnRotation)
        {
            var spellController =
                _instantiator.InstantiatePrefabForComponent<ISpellObjectController>(spellGameObjectProvider.Prefab,
                    spawnPosition, spawnRotation, null);
            spellController.Initialize(spellData, caster, this);
        }
    }
}