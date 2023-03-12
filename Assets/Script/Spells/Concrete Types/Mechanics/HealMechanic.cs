using UnityEngine;

[CreateAssetMenu(fileName = "Heal Mechanic", menuName = "Spells and Battlefield/Spell System/Mechanic/Heal Mechanic", order = 0)]
public class HealMechanic : SpellMechanicEffectScriptableObject
{
    [SerializeField] private int _healPoints;

    public override ISpellMechanicEffect GetImplementationObject() => new HealMechanicImplementation(_healPoints);

    private class HealMechanicImplementation : SpellMechanicEffectImplementationBase
    {
        private int _healPoints;

        public HealMechanicImplementation(int healPoints) => _healPoints = healPoints;

        protected override void ApplyEffectToTarget(ICharacter target)
        {
            target.HandleDamage(_healPoints);
        }
    }
}