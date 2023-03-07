using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerSpellsManager
{
    [HideInInspector] public bool IsSpellSelected => _currentSpells.Count > 0;
    [HideInInspector] public AnimatorOverrideController SelectedSpellHandsAnimatorController => _currentSpells[0].HandsAnimatorController;
    [SerializeField] private List<Spell> _currentSpells;
    [SerializeField] private Transform _spellSpawnObject;

    public void UseSelectedSpell(Quaternion direction)
    {
        _currentSpells[0].Cast(_spellSpawnObject.position, direction, _spellSpawnObject);
        _currentSpells.RemoveAt(0);
    }
}