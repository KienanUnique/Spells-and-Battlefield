using System.Collections.Generic;
using Spells;
using UnityEngine;

namespace Player
{
    public class PlayerSpellsManager : MonoBehaviour
    {
        public bool IsSpellSelected => _currentSpells.Count > 0;

        public AnimatorOverrideController SelectedSpellHandsAnimatorController =>
            _currentSpells[0].CastAnimationAnimatorOverrideController;

        [SerializeField] private List<SingleSpell> _currentSpells;
        [SerializeField] private Transform _spellSpawnObject;
        [SerializeField] private PlayerController _player;
        private Transform _playerTransform;

        private void Awake()
        {
            _playerTransform = _player.transform;
        }

        public void UseSelectedSpell(Quaternion direction)
        {
            _currentSpells[0].Cast(_spellSpawnObject.position, direction, _playerTransform, _player);
            _currentSpells.RemoveAt(0);
        }
    }
}