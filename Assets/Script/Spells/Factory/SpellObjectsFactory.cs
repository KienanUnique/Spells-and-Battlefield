using Common.Abstract_Bases.Factories;
using Spells.Controllers;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;
using Zenject;

namespace Spells.Factory
{
    public class SpellObjectsFactory : FactoryWithInstantiatorBase, ISpellObjectsFactory
    {
        public SpellObjectsFactory(IInstantiator instantiator, Transform defaultParentTransform) : base(instantiator,
            defaultParentTransform)
        {
        }

        public void Create(ISpellDataForSpellController spellData, ISpellPrefabProvider spellPrefabProvider,
            ICaster caster, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            var spellController =
                InstantiatePrefabForComponent<IInitializableSpellObjectController>(spellPrefabProvider, spawnPosition,
                    spawnRotation);
            spellController.Initialize(spellData, caster, this);
        }
    }
}