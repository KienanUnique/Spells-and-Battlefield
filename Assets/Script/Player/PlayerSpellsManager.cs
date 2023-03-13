using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellsManager : MonoBehaviour
{
    [HideInInspector] public bool IsSpellSelected => _currentSpells.Count > 0;
    [HideInInspector] public AnimatorOverrideController SelectedSpellHandsAnimatorController => _currentSpells[0].CastAnimationAnimatorOverrideController;
    [SerializeField] private List<SingleSpell> _currentSpells;
    [SerializeField] private Transform _spellSpawnObject;
    [SerializeField] private PlayerController _player;
    [SerializeField] private SpellGameObjectInterface _playerSpellGameObjectInterface;
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = _player.transform;
    }

    public void UseSelectedSpell(Quaternion direction)
    {
        _currentSpells[0].Cast(_spellSpawnObject.position, direction, _playerTransform, _playerSpellGameObjectInterface);
        _currentSpells.RemoveAt(0);
    }
}