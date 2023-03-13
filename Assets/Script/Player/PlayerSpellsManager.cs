using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerSpellsManager
{
    [HideInInspector] public bool IsSpellSelected => _currentSpells.Count > 0;
    [HideInInspector] public AnimatorOverrideController SelectedSpellHandsAnimatorController => _currentSpells[0].HandsAnimatorController;
    [SerializeField] private List<SingleSpell> _currentSpells;
    [SerializeField] private Transform _spellSpawnObject;

    public void UseSelectedSpell(ICharacter _playerCharacter, Transform playerTransform, Quaternion direction)
    {
        _currentSpells[0].Cast(_spellSpawnObject.position, direction, playerTransform, _playerCharacter);
        _currentSpells.RemoveAt(0);
    }
}