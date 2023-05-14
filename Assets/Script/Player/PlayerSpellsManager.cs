using System.Collections.Generic;
using Spells;
using Spells.Factory;
using Spells.Spell;
using Spells.Spell.Interfaces;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerSpellsManager : MonoBehaviour
    {
        [SerializeField] private List<SpellScriptableObject> _startTestSpells;
        [SerializeField] private Transform _spellSpawnObject;
        [SerializeField] private PlayerController _player;
        private List<ISpell> _spellsStorage;
        private ISpellObjectsFactory _spellObjectsFactory;

        [Inject]
        private void Construct(ISpellObjectsFactory spellObjectsFactory)
        {
            _spellObjectsFactory = spellObjectsFactory;
        }

        public bool IsSpellSelected => _spellsStorage.Count > 0;
        private ISpell SelectedSpell => _spellsStorage[0];

        public ISpellAnimationInformation SelectedSpellAnimationInformation => SelectedSpell.SpellAnimationInformation;

        public void UseSelectedSpell(Quaternion direction)
        {
            _spellObjectsFactory.Create(SelectedSpell.SpellDataForSpellController,
                SelectedSpell.SpellGameObjectProvider, _player, _spellSpawnObject.position, direction);
            _spellsStorage.RemoveAt(0);
        }

        public void AddSpell(ISpell newSpell)
        {
            _spellsStorage.Add(newSpell);
        }

        private void Awake()
        {
            _spellsStorage = new List<ISpell>();
            _startTestSpells.ForEach(spell => _spellsStorage.Add(spell.GetImplementationObject()));
        }
    }
}