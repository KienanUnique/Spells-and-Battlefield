using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellsManager : MonoBehaviour
{
    public bool IsSpellSelected => _currentSpells.Count > 0;
    public AnimatorOverrideController SelectedSpellHandsAnimatorController => _currentSpells[0].HandsAnimatorController;
    [SerializeField] private List<Spell> _currentSpells;
    [SerializeField] private Transform _spellSpawnObject;

    public void UseSelectedSpell(Quaternion direction)
    {
        _currentSpells[0].Cast(_spellSpawnObject.position, direction, _spellSpawnObject);
        _currentSpells.RemoveAt(0);
    }
}