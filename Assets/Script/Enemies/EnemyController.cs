using UnityEngine;

[RequireComponent(typeof(SpellGameObjectInterface))]
public class EnemyController : MonoBehaviour
{
    private SpellGameObjectInterface _spellGameObjectInterface;
    private Character _character;
    private void Awake()
    {
        _spellGameObjectInterface = GetComponent<SpellGameObjectInterface>();
        _character = new Character();
    }

    private void OnEnable()
    {
        _spellGameObjectInterface.HandleHealEvent += _character.HandleHeal;
        _spellGameObjectInterface.HandleDamageEvent += _character.HandleDamage;
    }

    private void OnDisable()
    {
        _spellGameObjectInterface.HandleHealEvent -= _character.HandleHeal;
        _spellGameObjectInterface.HandleDamageEvent -= _character.HandleDamage;
    }
}
