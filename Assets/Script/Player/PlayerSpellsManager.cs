using System.Collections.Generic;
using Spells;
using UnityEngine;

namespace Player
{
    public class PlayerSpellsManager : MonoBehaviour
    {
        [SerializeField] private List<SpellBase> _startTestSpells;
        [SerializeField] private Transform _spellSpawnObject;
        [SerializeField] private PlayerController _player;
        private Transform _playerTransform;
        private List<ISpell> _spellsStorage;

        public bool IsSpellSelected => _spellsStorage.Count > 0;

        public AnimatorOverrideController SelectedSpellHandsAnimatorController =>
            _spellsStorage[0].CastAnimationAnimatorOverrideController;

        public void UseSelectedSpell(Quaternion direction)
        {
            _spellsStorage[0].Cast(_spellSpawnObject.position, direction, _playerTransform, _player);
            _spellsStorage.RemoveAt(0);
        }

        public void AddSpell(ISpell newSpell)
        {
            _spellsStorage.Add(newSpell);
        }

        private void Awake()
        {
            _playerTransform = _player.transform;
            _spellsStorage = new List<ISpell>();
            _spellsStorage.AddRange(_startTestSpells);
        }
    }
}