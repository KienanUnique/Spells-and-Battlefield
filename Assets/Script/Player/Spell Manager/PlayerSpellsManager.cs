using System.Collections.Generic;
using Common.Readonly_Transform;
using Interfaces;
using Spells.Factory;
using Spells.Spell;
using Spells.Spell.Interfaces;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Player.Spell_Manager
{
    public class PlayerSpellsManager : IPlayerSpellsManager
    {
        private readonly IReadonlyTransform _spellSpawnObject;
        private readonly ICaster _player;
        private readonly ISpellObjectsFactory _spellObjectsFactory;

        private readonly List<ISpell> _spellsStorage;

        public PlayerSpellsManager(List<SpellScriptableObject> startTestSpells, IReadonlyTransform spellSpawnObject,
            ICaster player, ISpellObjectsFactory spellObjectsFactory)
        {
            _spellSpawnObject = spellSpawnObject;
            _player = player;
            _spellObjectsFactory = spellObjectsFactory;
            _spellsStorage = new List<ISpell>();
            startTestSpells.ForEach(spell => _spellsStorage.Add(spell));
        }

        public bool IsSpellSelected => _spellsStorage.Count > 0;
        public ISpell SelectedSpell => _spellsStorage[0];

        public ISpellAnimationInformation SelectedSpellAnimationInformation => SelectedSpell.SpellAnimationInformation;

        public void UseSelectedSpell(Quaternion direction)
        {
            _spellObjectsFactory.Create(SelectedSpell.SpellDataForSpellController,
                SelectedSpell.SpellPrefabProvider, _player, _spellSpawnObject.Position, direction);
            _spellsStorage.RemoveAt(0);
        }

        public void AddSpell(ISpell newSpell)
        {
            _spellsStorage.Add(newSpell);
        }
    }
}