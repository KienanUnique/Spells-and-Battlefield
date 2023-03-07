using UnityEngine;

[CreateAssetMenu(fileName = "Heal Mechanic", menuName = "Spells and Battlefield/Spell System/Mechanic/Heal Mechanic", order = 0)]
public class HealMechanic : SpellMechanicEffectScriptableObject
{
    [SerializeField] private int _healPoints;
    protected override void ApplyEffectToTarget(ICharacter target)
    {
        target.HandleHeal(_healPoints);
    }
}